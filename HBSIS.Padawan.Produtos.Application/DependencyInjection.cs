using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services.Generic;
using HBSIS.Padawan.Produtos.Application.Services.Produtos;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators;
using HBSIS.Padawan.Produtos.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HBSIS.Padawan.Produtos.Application
{
    public static class DependencyInjection
    {
        public static void Injetar(this IServiceCollection services)
        {
            services.AddScoped(typeof(IExportarCSVService<>), typeof(ExportarCSVService<>));

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<ICamposFornecedorValidator, CamposFornecedorValidator>();

            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddScoped<ICategoriaProdutoService, CategoriaProdutoService>();

            services.AddScoped<ICamposCategoriaProdutoValidator, CamposCategoriaProdutoValidator>();
            services.AddScoped<IIdCategoriaProdutoValidator, IdCategoriaProdutoValidator>();
            services.AddScoped<IUpdateCategoriaProdutoValidator, UpdateCategoriaProdutoValidator>();
 
            services.AddScoped<IImportarCategoriaProdutoValidator, ImportarCategoriaProdutoValidator>();
            services.AddScoped<ICategoriaProdutoExportarCSVService, CategoriaProdutoExportarCSVService>();
            services.AddScoped<ICategoriaProdutoImportarCSVService, CategoriaProdutoImportarCSVService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<ICamposProdutoValidator, CamposProdutoValidator>();
            services.AddScoped<IIdProdutoValidator, IdProdutoValidator>();
            services.AddScoped<IUpdateProdutoValidator, UpdateProdutoValidator>();

            services.AddScoped<IProdutosExportarCSVService, ProdutosExportarCSVService>();
        }
    }
}
