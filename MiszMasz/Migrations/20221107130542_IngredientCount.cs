using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiszMasz.Migrations
{
    public partial class IngredientCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "RecipeIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "RecipeIngredients");
        }
    }
}
