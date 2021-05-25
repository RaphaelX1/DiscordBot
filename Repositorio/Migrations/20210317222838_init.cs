using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arcanos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    TipoArcano = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arcanos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atributos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atributos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Efeitos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    TipoEfeito = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Efeitos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pericias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pericias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religioes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religioes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vantagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vantagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EfeitoCaracteristicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EfeitoId = table.Column<Guid>(nullable: false),
                    TipoCaracteristica = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Fortuna = table.Column<int>(nullable: false),
                    Ferimentos = table.Column<int>(nullable: false),
                    FerimentosDramaticos = table.Column<int>(nullable: false),
                    Bio = table.Column<string>(nullable: true),
                    ReligiaoId = table.Column<Guid>(nullable: false),
                    NacaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personagens_Nacoes_NacaoId",
                        column: x => x.NacaoId,
                        principalTable: "Nacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personagens_Religioes_ReligiaoId",
                        column: x => x.ReligiaoId,
                        principalTable: "Religioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemArcanos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    ArcanoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemArcanos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemArcanos_Arcanos_ArcanoId",
                        column: x => x.ArcanoId,
                        principalTable: "Arcanos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemArcanos_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemAtributos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<int>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    AtributoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemAtributos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemAtributos_Atributos_AtributoId",
                        column: x => x.AtributoId,
                        principalTable: "Atributos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemAtributos_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemEfeitos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    TempoAtividade = table.Column<int>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    EfeitoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemEfeitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemEfeitos_Efeitos_EfeitoId",
                        column: x => x.EfeitoId,
                        principalTable: "Efeitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemEfeitos_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemFormacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    FormacaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemFormacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemFormacoes_Formacoes_FormacaoId",
                        column: x => x.FormacaoId,
                        principalTable: "Formacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemFormacoes_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemPericias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<int>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    PericiaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemPericias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemPericias_Pericias_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemPericias_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemVantagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<int>(nullable: false),
                    PersonagemId = table.Column<Guid>(nullable: false),
                    VantagemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemVantagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonagemVantagens_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemVantagens_Vantagens_VantagemId",
                        column: x => x.VantagemId,
                        principalTable: "Vantagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EfeitoCaracteristicas_EfeitoId",
                table: "EfeitoCaracteristicas",
                column: "EfeitoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemArcanos_ArcanoId",
                table: "PersonagemArcanos",
                column: "ArcanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemArcanos_PersonagemId",
                table: "PersonagemArcanos",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemAtributos_AtributoId",
                table: "PersonagemAtributos",
                column: "AtributoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemAtributos_PersonagemId",
                table: "PersonagemAtributos",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemEfeitos_EfeitoId",
                table: "PersonagemEfeitos",
                column: "EfeitoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemEfeitos_PersonagemId",
                table: "PersonagemEfeitos",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemFormacoes_FormacaoId",
                table: "PersonagemFormacoes",
                column: "FormacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemFormacoes_PersonagemId",
                table: "PersonagemFormacoes",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemPericias_PericiaId",
                table: "PersonagemPericias",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemPericias_PersonagemId",
                table: "PersonagemPericias",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemVantagens_PersonagemId",
                table: "PersonagemVantagens",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemVantagens_VantagemId",
                table: "PersonagemVantagens",
                column: "VantagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_NacaoId",
                table: "Personagens",
                column: "NacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_ReligiaoId",
                table: "Personagens",
                column: "ReligiaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EfeitoCaracteristicas");

            migrationBuilder.DropTable(
                name: "PersonagemArcanos");

            migrationBuilder.DropTable(
                name: "PersonagemAtributos");

            migrationBuilder.DropTable(
                name: "PersonagemEfeitos");

            migrationBuilder.DropTable(
                name: "PersonagemFormacoes");

            migrationBuilder.DropTable(
                name: "PersonagemPericias");

            migrationBuilder.DropTable(
                name: "PersonagemVantagens");

            migrationBuilder.DropTable(
                name: "Arcanos");

            migrationBuilder.DropTable(
                name: "Atributos");

            migrationBuilder.DropTable(
                name: "Efeitos");

            migrationBuilder.DropTable(
                name: "Formacoes");

            migrationBuilder.DropTable(
                name: "Pericias");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Vantagens");

            migrationBuilder.DropTable(
                name: "Nacoes");

            migrationBuilder.DropTable(
                name: "Religioes");
        }
    }
}
