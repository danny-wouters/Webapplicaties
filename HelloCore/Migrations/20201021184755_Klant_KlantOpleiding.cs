using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloCore.Migrations
{
    public partial class Klant_KlantOpleiding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opleiding",
                columns: table => new
                {
                    OpleidingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AantalLesuren = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opleiding", x => x.OpleidingID);
                });

            migrationBuilder.CreateTable(
                name: "KlantOpleiding",
                columns: table => new
                {
                    KlantOpleidingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantID = table.Column<int>(nullable: false),
                    OpleidingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlantOpleiding", x => x.KlantOpleidingID);
                    table.ForeignKey(
                        name: "FK_KlantOpleiding_Klant_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klant",
                        principalColumn: "KlantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlantOpleiding_Opleiding_OpleidingID",
                        column: x => x.OpleidingID,
                        principalTable: "Opleiding",
                        principalColumn: "OpleidingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KlantOpleiding_KlantID",
                table: "KlantOpleiding",
                column: "KlantID");

            migrationBuilder.CreateIndex(
                name: "IX_KlantOpleiding_OpleidingID",
                table: "KlantOpleiding",
                column: "OpleidingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlantOpleiding");

            migrationBuilder.DropTable(
                name: "Opleiding");
        }
    }
}
