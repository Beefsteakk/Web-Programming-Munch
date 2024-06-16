using System;
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
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CatID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CatType = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CatID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RestName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestLat = table.Column<double>(type: "double", nullable: true),
                    RestLong = table.Column<double>(type: "double", nullable: true),
                    RestAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestPassword = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestContact = table.Column<int>(type: "int", nullable: true),
                    RestBio = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestPic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestWebsite = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestRatings = table.Column<float>(type: "float", nullable: true),
                    RestOpenHr = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    RestCloseHr = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    RestCoverPic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserContactNum = table.Column<int>(type: "int", nullable: false),
                    UserUsername = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPassword = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserProfilePic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserBio = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountToken = table.Column<int>(type: "int", nullable: true),
                    AccountVerified = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    UserCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserCoverPic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestCategory",
                columns: table => new
                {
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CatID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestCategory", x => new { x.RestID, x.CatID });
                    table.ForeignKey(
                        name: "FK_RestCategory_Category_CatID",
                        column: x => x.CatID,
                        principalTable: "Category",
                        principalColumn: "CatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestCategory_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodLists",
                columns: table => new
                {
                    FoodListID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FoodListTitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FoodListDescription = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FoodListCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FoodListImage = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLists", x => x.FoodListID);
                    table.ForeignKey(
                        name: "FK_FoodLists_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    ForumID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ForumName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForumDesc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForumCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.ForumID);
                    table.ForeignKey(
                        name: "FK_Forums_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PostContent = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TaggedRest = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Restaurants_TaggedRest",
                        column: x => x.TaggedRest,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NumOfGuests = table.Column<int>(type: "int", nullable: false),
                    SpecialRequest = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReservationStatus = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReservationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReservationTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ReservedName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestaurantFollowings",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FollowedRestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FollowCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantFollowings", x => new { x.UserID, x.FollowedRestID });
                    table.ForeignKey(
                        name: "FK_RestaurantFollowings_Restaurants_FollowedRestID",
                        column: x => x.FollowedRestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantFollowings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestViewHistory",
                columns: table => new
                {
                    ViewID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ViewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestViewHistory", x => x.ViewID);
                    table.ForeignKey(
                        name: "FK_RestViewHistory_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestViewHistory_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SearchHistory",
                columns: table => new
                {
                    SearchID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserSearch = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SearchCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistory", x => x.SearchID);
                    table.ForeignKey(
                        name: "FK_SearchHistory_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserFollowings",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FollowedUserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FollowCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowings", x => new { x.UserID, x.FollowedUserID });
                    table.ForeignKey(
                        name: "FK_UserFollowings_Users_FollowedUserID",
                        column: x => x.FollowedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFollowings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodListCategory",
                columns: table => new
                {
                    FoodListID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CatID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodListCategory", x => new { x.FoodListID, x.CatID });
                    table.ForeignKey(
                        name: "FK_FoodListCategory_Category_CatID",
                        column: x => x.CatID,
                        principalTable: "Category",
                        principalColumn: "CatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodListCategory_FoodLists_FoodListID",
                        column: x => x.FoodListID,
                        principalTable: "FoodLists",
                        principalColumn: "FoodListID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodListEntries",
                columns: table => new
                {
                    FoodListID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodListEntries", x => new { x.FoodListID, x.RestID });
                    table.ForeignKey(
                        name: "FK_FoodListEntries_FoodLists_FoodListID",
                        column: x => x.FoodListID,
                        principalTable: "FoodLists",
                        principalColumn: "FoodListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodListEntries_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodListLikes",
                columns: table => new
                {
                    FoodListID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LikeCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodListLikes", x => new { x.FoodListID, x.UserID });
                    table.ForeignKey(
                        name: "FK_FoodListLikes_FoodLists_FoodListID",
                        column: x => x.FoodListID,
                        principalTable: "FoodLists",
                        principalColumn: "FoodListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodListLikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ForumComments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ForumID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Comments = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumComments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_ForumComments_Forums_ForumID",
                        column: x => x.ForumID,
                        principalTable: "Forums",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumComments_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumComments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ForumVotes",
                columns: table => new
                {
                    ForumID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VoteType = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumVotes", x => new { x.ForumID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ForumVotes_Forums_ForumID",
                        column: x => x.ForumID,
                        principalTable: "Forums",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumVotes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CommentContent = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommentCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostLikesRest",
                columns: table => new
                {
                    PostID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LikeCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikesRest", x => new { x.PostID, x.RestID });
                    table.ForeignKey(
                        name: "FK_PostLikesRest_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikesRest_Restaurants_RestID",
                        column: x => x.RestID,
                        principalTable: "Restaurants",
                        principalColumn: "RestID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostLikesUser",
                columns: table => new
                {
                    PostID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LikeCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikesUser", x => new { x.PostID, x.UserID });
                    table.ForeignKey(
                        name: "FK_PostLikesUser_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikesUser_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostPics",
                columns: table => new
                {
                    PicID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ImageURL = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPics", x => x.PicID);
                    table.ForeignKey(
                        name: "FK_PostPics_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    ReviewComments = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReviewCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RestID",
                table: "Comments",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodListCategory_CatID",
                table: "FoodListCategory",
                column: "CatID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodListEntries_RestID",
                table: "FoodListEntries",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodListLikes_UserID",
                table: "FoodListLikes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodLists_UserID",
                table: "FoodLists",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ForumComments_ForumID",
                table: "ForumComments",
                column: "ForumID");

            migrationBuilder.CreateIndex(
                name: "IX_ForumComments_RestID",
                table: "ForumComments",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_ForumComments_UserID",
                table: "ForumComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_UserID",
                table: "Forums",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ForumVotes_UserID",
                table: "ForumVotes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikesRest_RestID",
                table: "PostLikesRest",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikesUser_UserID",
                table: "PostLikesUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostPics_PostID",
                table: "PostPics",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RestID",
                table: "Posts",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TaggedRest",
                table: "Posts",
                column: "TaggedRest");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestID",
                table: "Reservations",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantFollowings_FollowedRestID",
                table: "RestaurantFollowings",
                column: "FollowedRestID");

            migrationBuilder.CreateIndex(
                name: "IX_RestCategory_CatID",
                table: "RestCategory",
                column: "CatID");

            migrationBuilder.CreateIndex(
                name: "IX_RestViewHistory_RestID",
                table: "RestViewHistory",
                column: "RestID");

            migrationBuilder.CreateIndex(
                name: "IX_RestViewHistory_UserID",
                table: "RestViewHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PostID",
                table: "Reviews",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_UserID",
                table: "SearchHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowings_FollowedUserID",
                table: "UserFollowings",
                column: "FollowedUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FoodListCategory");

            migrationBuilder.DropTable(
                name: "FoodListEntries");

            migrationBuilder.DropTable(
                name: "FoodListLikes");

            migrationBuilder.DropTable(
                name: "ForumComments");

            migrationBuilder.DropTable(
                name: "ForumVotes");

            migrationBuilder.DropTable(
                name: "PostLikesRest");

            migrationBuilder.DropTable(
                name: "PostLikesUser");

            migrationBuilder.DropTable(
                name: "PostPics");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RestaurantFollowings");

            migrationBuilder.DropTable(
                name: "RestCategory");

            migrationBuilder.DropTable(
                name: "RestViewHistory");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SearchHistory");

            migrationBuilder.DropTable(
                name: "UserFollowings");

            migrationBuilder.DropTable(
                name: "FoodLists");

            migrationBuilder.DropTable(
                name: "Forums");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
