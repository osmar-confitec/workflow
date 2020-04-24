using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkFlowData.Migrations.EventStoreContextoMigrations
{
    public partial class StoredUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbUsuariosStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StoredEventAction = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "varchar(max)", nullable: false),
                    User = table.Column<Guid>(nullable: false),
                    AggregatedId = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    MessageType = table.Column<string>(type: "varchar(300)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUsuariosStorage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbUsuariosStorage");
        }
    }
}
