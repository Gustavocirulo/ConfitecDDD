using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noticias");

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PSA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PSA_NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PSA_SOBRENOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PSA_EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PSA_DATA_NASCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PSA_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PSA_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PSA_ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    PSA_ESCOLARIDADE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PSA_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.CreateTable(
                name: "Noticias",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NTC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NTC_ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    NTC_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NTC_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NTC_INFORMACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NTC_TITULO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.NTC_ID);
                    table.ForeignKey(
                        name: "FK_Noticias_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_UserId",
                table: "Noticias",
                column: "UserId");
        }
    }
}
