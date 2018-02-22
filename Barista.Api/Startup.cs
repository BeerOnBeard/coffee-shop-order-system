using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barista.Api.Eventing;
using Barista.Api.Repository;
using Barista.Api.Service;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Barista.Api
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
      services.AddDbContext<CoffeeContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("CoffeeShopDb")));
      
      services.AddCors(opt => {
        opt.AddPolicy("CorsPolicy", builder =>
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
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
      services.AddScoped<ICoffeeEventPublisher, CoffeeEventPublisher>();
      services.AddScoped<ICoffeeRepository, EntityFrameworkCoffeeRepository>();
      services.AddTransient<ICoffeeService, CoffeeService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors("CorsPolicy");
      app.UseMvc();
    }
  }
}
