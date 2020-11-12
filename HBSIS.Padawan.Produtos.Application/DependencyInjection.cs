using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services.Produtos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators;
using HBSIS.Padawan.Produtos.Infra.Repository;
using HBSIS.Padawan.Produtos.Infra.Services.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Infra.Services.Generic;
using HBSIS.Padawan.Produtos.Infra.Services.Produtos;
using Microsoft.Extensions.DependencyInjection;

namespace HBSIS.Padawan.Produtos.Application
{
    public static class DependencyInjection
    {
        public static void Injetar(this IServiceCollection services)
        { 
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<ICriarFornecedorValidator, CriarFornecedorValidator>();

            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddScoped<ICategoriaProdutoService, CategoriaProdutoService>();

            services.AddScoped<ICriarCategoriaProdutoValidator, CriarCategoriaProdutoValidator>();
            services.AddScoped<IIdCategoriaProdutoValidator, IdCategoriaProdutoValidator>();
            services.AddScoped<IAtualizarCategoriaProdutoValidator, AtualizarCategoriaProdutoValidator>();

            services.AddScoped<IExportarCSVService<CategoriaProduto>, ExportarCSVService<CategoriaProduto>>();
            services.AddScoped<IImportarCategoriaProdutoValidator, ImportarCategoriaProdutoValidator>();
            services.AddScoped<ICategoriaProdutoImportarCSVService, CategoriaProdutoImportarCSVService>();
            services.AddScoped<ICategoriaProdutoCSVService, CategoriaProdutoCSVService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<ICriarProdutoValidator, CriarProdutoValidator>();
            services.AddScoped<IIdProdutoValidator, IdProdutoValidator>();
            services.AddScoped<IAtualizarProdutoValidator, UpdateProdutoValidator>();

            services.AddScoped<IExportarCSVService<Produto>, ExportarCSVService<Produto>>();
            services.AddScoped<IImportarProdutoValidator, ImportarProdutoValidator>();
            services.AddScoped<IProdutosImportarCSVService, ProdutosImportarCSVService>();
            services.AddScoped<IProdutoCSVService, ProdutoCSVService>();
        }
    }
}
