using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using CoffeeShop.Coordinator.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Coordinator
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
          .AddDbContext<OrderContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("CoffeeShopDb")))
          .AddTransient<IOrderRepository, EntityFrameworkOrderRepository>()
          .AddTransient<OrderRequestedConsumer, OrderRequestedConsumer>()
          .AddTransient<OrderCompletedConsumer, OrderCompletedConsumer>()
          .AddTransient<OrderFulfilledConsumer, OrderFulfilledConsumer>()
          .BuildServiceProvider();
        
        BusControl = Bus.Factory.CreateUsingRabbitMq(config => {
          var host = config.Host(new Uri(configuration["Rabbit:Url"]), rabbit => {
            rabbit.Username(configuration["Rabbit:User"]);
            rabbit.Password(configuration["Rabbit:Pass"]);
          });

          config.ReceiveEndpoint(host, "order_requested", e => e.Consumer(() => services.GetService<OrderRequestedConsumer>()));
          config.ReceiveEndpoint(host, "order_completed", e => e.Consumer(() => services.GetService<OrderCompletedConsumer>()));
          config.ReceiveEndpoint(host, "order_fulfilled", e => e.Consumer(() => services.GetService<OrderFulfilledConsumer>()));
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
