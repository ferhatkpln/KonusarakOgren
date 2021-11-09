using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgren.Migrations
{
    public partial class modelss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sınavlar",
                columns: table => new
                {
                    SınavID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TextID = table.Column<string>(type: "TEXT", nullable: true),
                    Baslık = table.Column<string>(type: "TEXT", nullable: false),
                    İcerik = table.Column<string>(type: "TEXT", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sınavlar", x => x.SınavID);
                });

            migrationBuilder.CreateTable(
                name: "Sorular",
                columns: table => new
                {
                    SınavCevapID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SoruNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Soru = table.Column<string>(type: "TEXT", nullable: false),
                    CevapA = table.Column<string>(type: "TEXT", nullable: false),
                    CevapB = table.Column<string>(type: "TEXT", nullable: false),
                    CevapC = table.Column<string>(type: "TEXT", nullable: false),
                    CevapD = table.Column<string>(type: "TEXT", nullable: false),
                    DoğruCevap = table.Column<string>(type: "TEXT", nullable: false),
                    SınavID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorular", x => x.SınavCevapID);
                    table.ForeignKey(
                        name: "FK_Sorular_Sınavlar_SınavID",
                        column: x => x.SınavID,
                        principalTable: "Sınavlar",
                        principalColumn: "SınavID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_SınavID",
                table: "Sorular",
                column: "SınavID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sorular");

            migrationBuilder.DropTable(
                name: "Sınavlar");
        }
    }
}
