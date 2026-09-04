using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SushiMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePromotionsAndNewsLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleKey",
                table: "Promotions",
                newName: "TitleKeyUa");

            migrationBuilder.RenameColumn(
                name: "DescriptionKey",
                table: "Promotions",
                newName: "TitleKeyEn");

            migrationBuilder.RenameColumn(
                name: "DateKey",
                table: "Promotions",
                newName: "DescriptionKeyUa");

            migrationBuilder.RenameColumn(
                name: "TitleKey",
                table: "News",
                newName: "TitleKeyUa");

            migrationBuilder.RenameColumn(
                name: "DescriptionKey",
                table: "News",
                newName: "TitleKeyEn");

            migrationBuilder.AddColumn<string>(
                name: "DateKeyEn",
                table: "Promotions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateKeyUa",
                table: "Promotions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionKeyEn",
                table: "Promotions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionKeyEn",
                table: "News",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionKeyUa",
                table: "News",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateKeyEn",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DateKeyUa",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DescriptionKeyEn",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DescriptionKeyEn",
                table: "News");

            migrationBuilder.DropColumn(
                name: "DescriptionKeyUa",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "TitleKeyUa",
                table: "Promotions",
                newName: "TitleKey");

            migrationBuilder.RenameColumn(
                name: "TitleKeyEn",
                table: "Promotions",
                newName: "DescriptionKey");

            migrationBuilder.RenameColumn(
                name: "DescriptionKeyUa",
                table: "Promotions",
                newName: "DateKey");

            migrationBuilder.RenameColumn(
                name: "TitleKeyUa",
                table: "News",
                newName: "TitleKey");

            migrationBuilder.RenameColumn(
                name: "TitleKeyEn",
                table: "News",
                newName: "DescriptionKey");
        }
    }
}
