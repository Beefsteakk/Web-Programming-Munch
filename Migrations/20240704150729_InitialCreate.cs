using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EffectiveWebProg.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Items_ItemID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Restaurants_RestID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Restaurants_RestID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Restaurants_RestID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Restaurants_RestID",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_UserID",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Restaurants_RestID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Inventory_InventoryID",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Items_ItemID",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCat_CatID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostID",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Users_UserID",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostPics_Posts_PostID",
                table: "PostPics");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Restaurants_RestID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Restaurants_RestID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RestCards_CreditCard_CardID",
                table: "RestCards");

            migrationBuilder.DropForeignKey(
                name: "FK_RestCards_Restaurants_RestID",
                table: "RestCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeID",
                table: "TimeSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_CreditCard_CardID",
                table: "UserCards");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Users_UserID",
                table: "UserCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards");

            migrationBuilder.DropIndex(
                name: "IX_UserCards_UserID",
                table: "UserCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestCards",
                table: "RestCards");

            migrationBuilder.DropIndex(
                name: "IX_RestCards_RestID",
                table: "RestCards");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                newName: "AuthorID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Users",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "uuid()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RestPic",
                table: "Restaurants",
                type: "longblob",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestID",
                table: "Restaurants",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "uuid()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservationID",
                table: "Reservations",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "uuid()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostID",
                table: "Posts",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "uuid()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LikeCreatedAt",
                table: "PostLikes",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowCreatedAt",
                table: "Followings",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentID",
                table: "Comments",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "uuid()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards",
                columns: new[] { "UserID", "CardID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestCards",
                table: "RestCards",
                columns: new[] { "RestID", "CardID" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_CardID",
                table: "UserCards",
                column: "CardID");

            migrationBuilder.CreateIndex(
                name: "IX_RestCards_CardID",
                table: "RestCards",
                column: "CardID");

            migrationBuilder.AddForeignKey(
                name: "CartItems_ibfk_1",
                table: "CartItems",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "CartItems_ibfk_2",
                table: "CartItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Cart_ibfk_1",
                table: "Carts",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Comments_ibfk_1",
                table: "Comments",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Comments_ibfk_2",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "Comments_ibfk_3",
                table: "Comments",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID");

            migrationBuilder.AddForeignKey(
                name: "Employees_ibfk_1",
                table: "Employees",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Followings_ibfk_1",
                table: "Followings",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Followings_ibfk_2",
                table: "Followings",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Inventory_ibfk_1",
                table: "Inventory",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "InventoryItems_ibfk_1",
                table: "InventoryItems",
                column: "InventoryID",
                principalTable: "Inventory",
                principalColumn: "InventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "InventoryItems_ibfk_2",
                table: "InventoryItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Items_ibfk_1",
                table: "Items",
                column: "CatID",
                principalTable: "ItemCat",
                principalColumn: "CatID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "PostLikes_ibfk_1",
                table: "PostLikes",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "PostLikes_ibfk_2",
                table: "PostLikes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "PostPics_ibfk_1",
                table: "PostPics",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Posts_ibfk_2",
                table: "Posts",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID");

            migrationBuilder.AddForeignKey(
                name: "Reservations_ibfk_1",
                table: "Reservations",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Reservations_ibfk_2",
                table: "Reservations",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "RestCard_ibfk_1",
                table: "RestCards",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "RestCard_ibfk_2",
                table: "RestCards",
                column: "CardID",
                principalTable: "CreditCard",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "TimeSheet_ibfk_1",
                table: "TimeSheets",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserCard_ibfk_1",
                table: "UserCards",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserCard_ibfk_2",
                table: "UserCards",
                column: "CardID",
                principalTable: "CreditCard",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CartItems_ibfk_1",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "CartItems_ibfk_2",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "Cart_ibfk_1",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "Comments_ibfk_1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "Comments_ibfk_2",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "Comments_ibfk_3",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "Employees_ibfk_1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "Followings_ibfk_1",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "Followings_ibfk_2",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "Inventory_ibfk_1",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "InventoryItems_ibfk_1",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "InventoryItems_ibfk_2",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "Items_ibfk_1",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "PostLikes_ibfk_1",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "PostLikes_ibfk_2",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "PostPics_ibfk_1",
                table: "PostPics");

            migrationBuilder.DropForeignKey(
                name: "Posts_ibfk_2",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "Reservations_ibfk_1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "Reservations_ibfk_2",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "RestCard_ibfk_1",
                table: "RestCards");

            migrationBuilder.DropForeignKey(
                name: "RestCard_ibfk_2",
                table: "RestCards");

            migrationBuilder.DropForeignKey(
                name: "TimeSheet_ibfk_1",
                table: "TimeSheets");

            migrationBuilder.DropForeignKey(
                name: "UserCard_ibfk_1",
                table: "UserCards");

            migrationBuilder.DropForeignKey(
                name: "UserCard_ibfk_2",
                table: "UserCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards");

            migrationBuilder.DropIndex(
                name: "IX_UserCards_CardID",
                table: "UserCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestCards",
                table: "RestCards");

            migrationBuilder.DropIndex(
                name: "IX_RestCards_CardID",
                table: "RestCards");

            migrationBuilder.RenameIndex(
                name: "AuthorID",
                table: "Comments",
                newName: "IX_Comments_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Users",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "uuid()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "RestPic",
                table: "Restaurants",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestID",
                table: "Restaurants",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "uuid()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservationID",
                table: "Reservations",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "uuid()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostID",
                table: "Posts",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "uuid()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LikeCreatedAt",
                table: "PostLikes",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowCreatedAt",
                table: "Followings",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentID",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "uuid()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards",
                columns: new[] { "CardID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestCards",
                table: "RestCards",
                columns: new[] { "CardID", "RestID" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_UserID",
                table: "UserCards",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RestCards_RestID",
                table: "RestCards",
                column: "RestID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartID",
                table: "CartItems",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Items_ItemID",
                table: "CartItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Restaurants_RestID",
                table: "Carts",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Restaurants_RestID",
                table: "Comments",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Restaurants_RestID",
                table: "Employees",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Restaurants_RestID",
                table: "Followings",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_UserID",
                table: "Followings",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Restaurants_RestID",
                table: "Inventory",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Inventory_InventoryID",
                table: "InventoryItems",
                column: "InventoryID",
                principalTable: "Inventory",
                principalColumn: "InventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Items_ItemID",
                table: "InventoryItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCat_CatID",
                table: "Items",
                column: "CatID",
                principalTable: "ItemCat",
                principalColumn: "CatID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostID",
                table: "PostLikes",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Users_UserID",
                table: "PostLikes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostPics_Posts_PostID",
                table: "PostPics",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Restaurants_RestID",
                table: "Posts",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Restaurants_RestID",
                table: "Reservations",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserID",
                table: "Reservations",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestCards_CreditCard_CardID",
                table: "RestCards",
                column: "CardID",
                principalTable: "CreditCard",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestCards_Restaurants_RestID",
                table: "RestCards",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeID",
                table: "TimeSheets",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_CreditCard_CardID",
                table: "UserCards",
                column: "CardID",
                principalTable: "CreditCard",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Users_UserID",
                table: "UserCards",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
