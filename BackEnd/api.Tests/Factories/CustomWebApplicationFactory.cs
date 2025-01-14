using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace api.Tests.Factories
{
   public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDBContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}