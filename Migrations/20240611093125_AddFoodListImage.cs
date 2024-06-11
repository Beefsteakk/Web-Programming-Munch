using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EffectiveWebProg.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodListImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodListImage",
                table: "FoodLists",
                type: "text",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodListImage",
                table: "FoodLists");
        }
    }
}
