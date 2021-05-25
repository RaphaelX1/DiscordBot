using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class efeitoedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EfeitoCaracteristicas");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "PersonagemEfeitos");

            migrationBuilder.DropColumn(
                name: "TempoAtividade",
                table: "PersonagemEfeitos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "PersonagemEfeitos");

            migrationBuilder.DropColumn(
                name: "TipoEfeito",
                table: "Efeitos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "PersonagemEfeitos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TempoAtividade",
                table: "PersonagemEfeitos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Valor",
                table: "PersonagemEfeitos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoEfeito",
                table: "Efeitos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EfeitoCaracteristicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EfeitoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoCaracteristica = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfeitoCaracteristicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EfeitoCaracteristicas_Efeitos_EfeitoId",
                        column: x => x.EfeitoId,
                        principalTable: "Efeitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EfeitoCaracteristicas_EfeitoId",
                table: "EfeitoCaracteristicas",
                column: "EfeitoId");
        }
    }
}
