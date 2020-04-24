using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkFlowData.Migrations
{
    public partial class InitialTabelaClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbClientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    IdUsuarioCadastro = table.Column<Guid>(nullable: false),
                    IdUsuarioAlteracao = table.Column<Guid>(nullable: true),
                    DataDelecao = table.Column<DateTime>(nullable: true),
                    IdUsuarioDelecao = table.Column<Guid>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    SobreNome = table.Column<string>(type: "varchar(200)", nullable: false),
                    SobreNome1 = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbClientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbClientes");
        }
    }
}
