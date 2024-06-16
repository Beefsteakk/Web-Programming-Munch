﻿// <auto-generated />
using System;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EffectiveWebProg.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EffectiveWebProg.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("CatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CatType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(255)");

                    b.HasKey("CatID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.CommentsModel", b =>
                {
                    b.Property<Guid>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CommentCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PostID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("RestID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.HasIndex("RestID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.ForumCommentsModel", b =>
                {
                    b.Property<Guid>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ForumID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("RestID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("CommentID");

                    b.HasIndex("ForumID");

                    b.HasIndex("RestID");

                    b.HasIndex("UserID");

                    b.ToTable("ForumComments");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesRestModel", b =>
                {
                    b.Property<Guid>("PostID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("RestID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("LikeCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PostID", "RestID");

                    b.HasIndex("RestID");

                    b.ToTable("PostLikesRest");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesUserModel", b =>
                {
                    b.Property<Guid>("PostID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("LikeCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PostID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("PostLikesUser");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.Property<Guid>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("PostCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("RestID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TaggedRest")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("PostID");

                    b.HasIndex("RestID");

                    b.HasIndex("TaggedRest");

                    b.HasIndex("UserID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.RestaurantsModel", b =>
                {
                    b.Property<Guid>("RestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("RestAddress")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RestBio")
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("RestCloseHr")
                        .HasColumnType("time(6)");

                    b.Property<int?>("RestContact")
                        .HasColumnType("int");

                    b.Property<string>("RestCoverPic")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RestEmail")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double?>("RestLat")
                        .HasColumnType("double");

                    b.Property<double?>("RestLong")
                        .HasColumnType("double");

                    b.Property<string>("RestName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<TimeSpan?>("RestOpenHr")
                        .HasColumnType("time(6)");

                    b.Property<string>("RestPassword")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RestPic")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<float?>("RestRatings")
                        .HasColumnType("float");

                    b.Property<string>("RestWebsite")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("RestID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.UsersModel", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("AccountToken")
                        .HasColumnType("int");

                    b.Property<bool?>("AccountVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserBio")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserContactNum")
                        .HasColumnType("int");

                    b.Property<string>("UserCoverPic")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UserCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserProfilePic")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserUsername")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoodListCategoryModel", b =>
                {
                    b.Property<Guid>("FoodListID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CatID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.HasKey("FoodListID", "CatID");

                    b.HasIndex("CatID");

                    b.ToTable("FoodListCategory");
                });

            modelBuilder.Entity("FoodListEntriesModel", b =>
                {
                    b.Property<Guid>("FoodListID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("RestID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.HasKey("FoodListID", "RestID");

                    b.HasIndex("RestID");

                    b.ToTable("FoodListEntries");
                });

            modelBuilder.Entity("FoodListLikesModel", b =>
                {
                    b.Property<Guid>("FoodListID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("LikeCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FoodListID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("FoodListLikes");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.Property<Guid>("FoodListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("FoodListCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FoodListDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FoodListImage")
                        .HasColumnType("text");

                    b.Property<string>("FoodListTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("FoodListID");

                    b.HasIndex("UserID");

                    b.ToTable("FoodLists");
                });

            modelBuilder.Entity("ForumVotesModel", b =>
                {
                    b.Property<Guid>("ForumID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<bool>("VoteType")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("ForumID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ForumVotes");
                });

            modelBuilder.Entity("ForumsModel", b =>
                {
                    b.Property<Guid>("ForumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ForumCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ForumDesc")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ForumName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("ForumID");

                    b.HasIndex("UserID");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("PostPicsModel", b =>
                {
                    b.Property<Guid>("PicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid?>("PostID")
                        .HasColumnType("char(36)");

                    b.HasKey("PicID");

                    b.HasIndex("PostID");

                    b.ToTable("PostPics");
                });

            modelBuilder.Entity("ReservationsModel", b =>
                {
                    b.Property<Guid>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("NumOfGuests")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ReservationDate")
                        .HasColumnType("date");

                    b.Property<string>("ReservationStatus")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<TimeSpan>("ReservationTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("ReservedName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("RestID")
                        .HasColumnType("char(36)");

                    b.Property<string>("SpecialRequest")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("ReservationID");

                    b.HasIndex("RestID");

                    b.HasIndex("UserID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RestCategoryModel", b =>
                {
                    b.Property<Guid>("RestID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CatID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.HasKey("RestID", "CatID");

                    b.HasIndex("CatID");

                    b.ToTable("RestCategory");
                });

            modelBuilder.Entity("RestViewHistoryModel", b =>
                {
                    b.Property<Guid>("ViewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RestID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ViewedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ViewID");

                    b.HasIndex("RestID");

                    b.HasIndex("UserID");

                    b.ToTable("RestViewHistory");
                });

            modelBuilder.Entity("RestaurantFollowingsModel", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("FollowedRestID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("FollowCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserID", "FollowedRestID");

                    b.HasIndex("FollowedRestID");

                    b.ToTable("RestaurantFollowings");
                });

            modelBuilder.Entity("ReviewsModel", b =>
                {
                    b.Property<Guid>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PostID")
                        .HasColumnType("char(36)");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<string>("ReviewComments")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReviewCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ReviewID");

                    b.HasIndex("PostID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SearchHistoryModel", b =>
                {
                    b.Property<Guid>("SearchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("SearchCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.Property<string>("UserSearch")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("SearchID");

                    b.HasIndex("UserID");

                    b.ToTable("SearchHistory");
                });

            modelBuilder.Entity("UserFollowingsModel", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("FollowedUserID")
                        .HasColumnType("char(36)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("FollowCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserID", "FollowedUserID");

                    b.HasIndex("FollowedUserID");

                    b.ToTable("UserFollowings");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.CommentsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("Comment")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("Comment")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.ForumCommentsModel", b =>
                {
                    b.HasOne("ForumsModel", "Forum")
                        .WithMany("ForumComment")
                        .HasForeignKey("ForumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("ForumComment")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("ForumComment")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Forum");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesRestModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("PostLikeRest")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("PostLikesRest")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesUserModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("PostLikeUser")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("PostLikeUser")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("Post")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "TaggedRestaurant")
                        .WithMany("TaggedRest")
                        .HasForeignKey("TaggedRest")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("Post")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Restaurant");

                    b.Navigation("TaggedRestaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodListCategoryModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.CategoryModel", "Category")
                        .WithMany("FoodListCat")
                        .HasForeignKey("CatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodListsModel", "FoodList")
                        .WithMany("FoodListCat")
                        .HasForeignKey("FoodListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("FoodList");
                });

            modelBuilder.Entity("FoodListEntriesModel", b =>
                {
                    b.HasOne("FoodListsModel", "FoodList")
                        .WithMany("FoodListEntry")
                        .HasForeignKey("FoodListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("FoodEntry")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodList");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("FoodListLikesModel", b =>
                {
                    b.HasOne("FoodListsModel", "FoodList")
                        .WithMany("FoodListLike")
                        .HasForeignKey("FoodListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("FoodListLike")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("FoodList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumVotesModel", b =>
                {
                    b.HasOne("ForumsModel", "Forum")
                        .WithMany("ForumVote")
                        .HasForeignKey("ForumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("ForumVote")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Forum");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("Forum")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PostPicsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("PostPic")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ReservationsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("Reservation")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("Reservation")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestCategoryModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.CategoryModel", "Category")
                        .WithMany("RestCat")
                        .HasForeignKey("CatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("RestCat")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestViewHistoryModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("RestViewHistory")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("RestViewHistory")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantFollowingsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "FollowedRest")
                        .WithMany("FollowedBy")
                        .HasForeignKey("FollowedRestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("RestaurantFollowings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FollowedRest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReviewsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("Review")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SearchHistoryModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("SearchHistory")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserFollowingsModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.UsersModel", "FollowedUser")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.UsersModel", "User")
                        .WithMany("Followings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FollowedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.CategoryModel", b =>
                {
                    b.Navigation("FoodListCat");

                    b.Navigation("RestCat");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("PostLikeRest");

                    b.Navigation("PostLikeUser");

                    b.Navigation("PostPic");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.RestaurantsModel", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("FollowedBy");

                    b.Navigation("FoodEntry");

                    b.Navigation("ForumComment");

                    b.Navigation("Post");

                    b.Navigation("PostLikesRest");

                    b.Navigation("Reservation");

                    b.Navigation("RestCat");

                    b.Navigation("RestViewHistory");

                    b.Navigation("TaggedRest");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.UsersModel", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("FoodList");

                    b.Navigation("FoodListLike");

                    b.Navigation("Forum");

                    b.Navigation("ForumComment");

                    b.Navigation("ForumVote");

                    b.Navigation("Post");

                    b.Navigation("PostLikeUser");

                    b.Navigation("Reservation");

                    b.Navigation("RestViewHistory");

                    b.Navigation("RestaurantFollowings");

                    b.Navigation("SearchHistory");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.Navigation("FoodListCat");

                    b.Navigation("FoodListEntry");

                    b.Navigation("FoodListLike");
                });

            modelBuilder.Entity("ForumsModel", b =>
                {
                    b.Navigation("ForumComment");

                    b.Navigation("ForumVote");
                });
#pragma warning restore 612, 618
        }
    }
}
