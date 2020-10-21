﻿using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HBSIS.Padawan.Produtos.Application
{
    public static class DependencyInjection
    {
        public static void Injetar(this IServiceCollection services)
        {
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();

            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddScoped<ICategoriaProdutoService, CategoriaProdutoService>();
            services.AddScoped<ICategoriaProdutoValidator, CategoriaProdutoValidator>();

            services.AddScoped<ICSVService, CSVService>();
        }
    }
}
