using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services.Produtos;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators;
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
            services.AddScoped<IFornecedorValidator, FornecedorValidator>();

            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddScoped<ICategoriaProdutoService, CategoriaProdutoService>();

            services.AddScoped<ICamposCategoriaProdutoValidator, CamposCategoriaProdutoValidator>();
            services.AddScoped<IIdCategoriaProdutoValidator, IdCategoriaProdutoValidator>();
            services.AddScoped<IUpdateCategoriaProdutoValidator, UpdateCategoriaProdutoValidator>();
 
            services.AddScoped<ICategoriaProdutoCSVService, CategoriaProdutoCSVService>();
            services.AddScoped<IImportarCategoriaProdutoValidator, ImportarCategoriaProdutoValidator>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<ICamposProdutoValidator, CamposProdutoValidator>();
            services.AddScoped<IIdProdutoValidator, IdProdutoValidator>();
            services.AddScoped<IUpdateProdutoValidator, UpdateProdutoValidator>();

            services.AddScoped<IProdutoCSVService, ProdutoCSVService>();
        }
    }
}
