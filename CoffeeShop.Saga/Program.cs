using System;
using System.Runtime.Loader;
using System.Threading;
using MassTransit;

namespace CoffeeShop.Saga
{
  public static class Program
  {
    public static ManualResetEvent Shutdown = new ManualResetEvent(false);
    public static ManualResetEventSlim Complete = new ManualResetEventSlim();
    public static IBusControl BusControl;
    
    static void Main(string[] args)
    {
      try
      {
        Console.Write("Starting...");

        AssemblyLoadContext.Default.Unloading += DefaultUnloading;

        BusControl = Bus.Factory.CreateUsingRabbitMq(config => {
          var host = config.Host(new Uri("rabbitmq://localhost"), rabbit => {
            rabbit.Username("phil");
            rabbit.Password("likesBagels");
          });

          config.ReceiveEndpoint(host, "order_created_queue", e => e.Consumer<OrderCreatedConsumer>());
        });

        BusControl.Start();

        Shutdown.WaitOne();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        Console.Write("Cleaning up before shutdown...");

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
