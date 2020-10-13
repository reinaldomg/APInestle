using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HBSIS.Padawan.Produtos.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(op => op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddScoped<IFornecedorRepository,FornecedorRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddCors();

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                 {
                        Title = "ProjetoNestle",
                        Version = "v1",
                });
             });
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetRequiredService<MainContext>();

                context
                    .Database
                    .Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 ProjetoNestle");
                c.DocExpansion(DocExpansion.None);
            });


        }

        private void AddDbContextCollection(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}