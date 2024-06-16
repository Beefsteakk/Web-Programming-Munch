using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EffectiveWebProg.Migrations
{
    /// <inheritdoc />
    public partial class postAndcatupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_Users_UserID",
                table: "ForumComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Restaurants_TaggedRest",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_Users_UserID",
                table: "ForumComments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Restaurants_TaggedRest",
                table: "Posts",
                column: "TaggedRest",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_Users_UserID",
                table: "ForumComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Restaurants_TaggedRest",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_Users_UserID",
                table: "ForumComments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Restaurants_TaggedRest",
                table: "Posts",
                column: "TaggedRest",
                principalTable: "Restaurants",
                principalColumn: "RestID");
        }
    }
}
