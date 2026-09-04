using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SushiMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationsAndNewsLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    TitleKeyUa = table.Column<string>(type: "TEXT", nullable: false),
                    TitleKeyEn = table.Column<string>(type: "TEXT", nullable: false),
                    CityKeyUa = table.Column<string>(type: "TEXT", nullable: false),
                    CityKeyEn = table.Column<string>(type: "TEXT", nullable: false),
                    AddressKeyUa = table.Column<string>(type: "TEXT", nullable: false),
                    AddressKeyEn = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Lng = table.Column<double>(type: "REAL", nullable: false),
                    Hours = table.Column<string>(type: "TEXT", nullable: false),
                    ImageSrc = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
