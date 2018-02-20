using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Api.Eventing;
using CoffeeShop.Api.Repository;
using CoffeeShop.Api.Service;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoffeeShop.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<OrderContext>(opt => {
        opt.UseSqlServer(Configuration.GetConnectionString("CoffeeShopDb"));
      });

      services.AddMvc();

      var bus = Bus.Factory.CreateUsingRabbitMq(config =>
        config.Host(new Uri(Configuration["Rabbit:Url"]), rabbit =>
        {
          rabbit.Username(Configuration["Rabbit:User"]);
          rabbit.Password(Configuration["Rabbit:Pass"]);
        })
      );

      services.AddSingleton<IPublishEndpoint>(bus);
      services.AddScoped<IOrderEventPublisher, OrderEventPublisher>();
      services.AddScoped<IOrderRepository, EntityFrameworkOrderRepository>();
      services.AddTransient<IOrderService, OrderService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
