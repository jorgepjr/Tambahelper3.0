using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Migrations
{
    public partial class Genesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tecnico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Help",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoDeProblema = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Setor = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    TecnicoId = table.Column<int>(nullable: true),
                    InicioDoAtendimento = table.Column<DateTime>(nullable: false),
                    FimDoAtendimento = table.Column<DateTime>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Solucao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Help", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Help_Tecnico_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Help_TecnicoId",
                table: "Help",
                column: "TecnicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Help");

            migrationBuilder.DropTable(
                name: "Tecnico");
        }
    }
}
