using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkFlowData.Migrations
{
    public partial class MenusItensSegurancaConfigTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbMenu",
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
                    DescricaoMenu = table.Column<string>(type: "varchar(200)", nullable: false),
                    IdMenuPai = table.Column<Guid>(nullable: true),
                    Ordem = table.Column<int>(nullable: false),
                    Modulo = table.Column<int>(nullable: true),
                    Emodulo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbMenu_TbMenu_IdMenuPai",
                        column: x => x.IdMenuPai,
                        principalTable: "TbMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbModuloAcao",
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
                    Modulo = table.Column<int>(nullable: false),
                    Acao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbModuloAcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbUsuarios",
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
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    SobreNome = table.Column<string>(type: "varchar(150)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    SenhaHash = table.Column<byte[]>(nullable: false),
                    SenhaSalt = table.Column<byte[]>(nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbUsuarioModuloAcao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false),
                    IdModuloAcao = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    IdUsuarioCadastro = table.Column<Guid>(nullable: false),
                    IdUsuarioAlteracao = table.Column<Guid>(nullable: true),
                    DataDelecao = table.Column<DateTime>(nullable: true),
                    IdUsuarioDelecao = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUsuarioModuloAcao", x => new { x.Id, x.IdUsuario, x.IdModuloAcao });
                    table.ForeignKey(
                        name: "FK_TbUsuarioModuloAcao_TbModuloAcao_IdModuloAcao",
                        column: x => x.IdModuloAcao,
                        principalTable: "TbModuloAcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbUsuarioModuloAcao_TbUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "TbUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbMenu_IdMenuPai",
                table: "TbMenu",
                column: "IdMenuPai",
                unique: true,
                filter: "[IdMenuPai] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuarioModuloAcao_IdModuloAcao",
                table: "TbUsuarioModuloAcao",
                column: "IdModuloAcao");

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuarioModuloAcao_IdUsuario",
                table: "TbUsuarioModuloAcao",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbMenu");

            migrationBuilder.DropTable(
                name: "TbUsuarioModuloAcao");

            migrationBuilder.DropTable(
                name: "TbModuloAcao");

            migrationBuilder.DropTable(
                name: "TbUsuarios");
        }
    }
}
