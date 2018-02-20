using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using Barista.Coordinator.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barista.Coordinator
{
  public class Program
  {
    public static ManualResetEvent Shutdown = new ManualResetEvent(false);
    public static ManualResetEventSlim Complete = new ManualResetEventSlim();
    public static IBusControl BusControl;
    
    public static void Main(string[] args)
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
          .AddDbContext<CoffeeContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("CoffeeShopDb")))
          .AddTransient<ICoffeeRepository, EntityFrameworkCoffeeRepository>()
          .AddTransient<CoffeesOrderConsumer, CoffeesOrderConsumer>()
          .AddTransient<CoffeeCompletedConsumer, CoffeeCompletedConsumer>()
          .BuildServiceProvider();

        BusControl = Bus.Factory.CreateUsingRabbitMq(config => {
          var host = config.Host(new Uri(configuration["Rabbit:Url"]), rabbit => {
            rabbit.Username(configuration["Rabbit:User"]);
            rabbit.Password(configuration["Rabbit:Pass"]);
          });

          config.ReceiveEndpoint(host, "coffees_requested", e => e.Consumer(() => services.GetService<CoffeesOrderConsumer>()));
          config.ReceiveEndpoint(host, "coffee_completed", e => e.Consumer(() => services.GetService<CoffeeCompletedConsumer>()));
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

      Console.Write("Goodbye!");
      Complete.Set();
    }

    private static void DefaultUnloading(AssemblyLoadContext context)
    {
      Console.WriteLine("Shutting down in response to SIGTERM.");
      Shutdown.Set();
      Complete.Wait();
    }
  }
}
