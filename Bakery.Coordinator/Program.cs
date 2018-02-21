using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using Bakery.Coordinator.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Coordinator
{
  public class Program
  {
    public static ManualResetEvent Shutdown = new ManualResetEvent(false);
    public static ManualResetEventSlim Complete = new ManualResetEventSlim();
    public static IBusControl BusControl;

    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Starting...");

        AssemblyLoadContext.Default.Unloading += DefaultUnloading;
        
        var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("CS_Environment")}.json", optional: true)
          .Build();
        
        var services = new ServiceCollection()
          .AddSingleton<IConfiguration>(configuration)
          .AddDbContext<BagelContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("CoffeeShopDb")))
          .AddTransient<IBagelRepository, EntityFrameworkBagelRepository>()
          .AddTransient<BagelsOrderedConsumer, BagelsOrderedConsumer>()
          .AddTransient<BagelCompletedConsumer, BagelCompletedConsumer>()
          .BuildServiceProvider();

        BusControl = Bus.Factory.CreateUsingRabbitMq(config => {
          var host = config.Host(new Uri(configuration["Rabbit:Url"]), rabbit => {
            rabbit.Username(configuration["Rabbit:User"]);
            rabbit.Password(configuration["Rabbit:Pass"]);
          });

          config.ReceiveEndpoint(host, "bagels_ordered", e => e.Consumer(() => services.GetService<BagelsOrderedConsumer>()));
          config.ReceiveEndpoint(host, "bagel_completed", e => e.Consumer(() => services.GetService<BagelCompletedConsumer>()));
        });

        BusControl.Start();

        Console.WriteLine("Waiting for events...");
        Shutdown.WaitOne();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        Console.Write("Cleaning up before shutdown...");
        BusControl.Stop();
      }
    }

    private static void DefaultUnloading(AssemblyLoadContext context)
    {
      Console.WriteLine("Shutting down in response to SIGTERM.");
      Shutdown.Set();
      Complete.Wait();
    }
  }
}
