using HBSIS.Padawan.Produtos.Infra.Context;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Padawan.Produtos.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private ServiceProvider _serviceProvider;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MainContext>));

                services.Remove(descriptor);

                var connectionString = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
                var connection = new SqliteConnection(connectionString.ToString());

                services.AddDbContext<MainContext>(options =>
                {                 
                    options.UseSqlite(connection);
                });

                _serviceProvider = services.BuildServiceProvider();

                using var scope = _serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<MainContext>();

                if (db.Database.GetDbConnection().State == System.Data.ConnectionState.Closed)
                {
                    db.Database.OpenConnection();
                }

                db.Database.Migrate();               
            });
        }

        public MainContext GetContext()
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<MainContext>();
        }

        public void SeedData(params object[] data)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MainContext>();

            db.AddRange(data);
            db.SaveChanges();
        }
    }
}
