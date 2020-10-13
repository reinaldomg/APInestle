using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.Padawan.Produtos.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RazaoSocial = table.Column<string>(maxLength: 500, nullable: false),
                    Cnpj = table.Column<string>(maxLength: 14, nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 500, nullable: true),
                    Endereco = table.Column<string>(maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
