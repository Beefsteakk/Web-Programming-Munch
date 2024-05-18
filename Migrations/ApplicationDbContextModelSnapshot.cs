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
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EffectiveWebProg.Models.CommentsModel", b =>
                {
                    b.Property<Guid>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CommentCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PostID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("PostID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesModel", b =>
                {
                    b.Property<Guid>("PostLikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LikeCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PostID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PostLikeID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.Property<Guid>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("PostCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostImageURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PostLocation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostUpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PostID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.RestaurantsModel", b =>
                {
                    b.Property<Guid>("RestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RestBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("RestLat")
                        .HasColumnType("float");

                    b.Property<double>("RestLong")
                        .HasColumnType("float");

                    b.Property<string>("RestName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RestPic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RestWebsite")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("RestID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("FoodListEntriesModel", b =>
                {
                    b.Property<Guid>("FoodListEntriesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RestaurantID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodListEntriesID");

                    b.HasIndex("FoodListID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("FoodListEntries");
                });

            modelBuilder.Entity("FoodListLikesModel", b =>
                {
                    b.Property<Guid>("FoodListLikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodListLikeID");

                    b.HasIndex("FoodListID");

                    b.HasIndex("UserID");

                    b.ToTable("FoodListLikes");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.Property<Guid>("FoodListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FoodListCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FoodListDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FoodListTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FoodListID");

                    b.HasIndex("OwnerID");

                    b.ToTable("FoodLists");
                });

            modelBuilder.Entity("ReservationModel", b =>
                {
                    b.Property<Guid>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumOfGuests")
                        .HasColumnType("int");

                    b.Property<string>("ReservationStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ReservationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RestID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpecialRequest")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReservationID");

                    b.HasIndex("RestID");

                    b.HasIndex("UserID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("UsersModel", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAccountType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserBio")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UserCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserProfilePic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserUsername")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.CommentsModel", b =>
                {
                    b.HasOne("UsersModel", "Author")
                        .WithMany("Comment")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostLikesModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.PostsModel", "Post")
                        .WithMany("PostLike")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UsersModel", "User")
                        .WithMany("PostLike")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.HasOne("UsersModel", "Author")
                        .WithMany("Post")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.RestaurantsModel", b =>
                {
                    b.HasOne("UsersModel", "Owner")
                        .WithMany("Restaurant")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FoodListEntriesModel", b =>
                {
                    b.HasOne("FoodListsModel", "FoodList")
                        .WithMany("FoodListEntry")
                        .HasForeignKey("FoodListID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("FoodEntry")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FoodList");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("FoodListLikesModel", b =>
                {
                    b.HasOne("FoodListsModel", "FoodList")
                        .WithMany("FoodListLike")
                        .HasForeignKey("FoodListID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UsersModel", "User")
                        .WithMany("FoodListLike")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FoodList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.HasOne("UsersModel", "Owner")
                        .WithMany("FoodList")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ReservationModel", b =>
                {
                    b.HasOne("EffectiveWebProg.Models.RestaurantsModel", "Restaurant")
                        .WithMany("Reservation")
                        .HasForeignKey("RestID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UsersModel", "User")
                        .WithMany("Reservation")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.PostsModel", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("PostLike");
                });

            modelBuilder.Entity("EffectiveWebProg.Models.RestaurantsModel", b =>
                {
                    b.Navigation("FoodEntry");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("FoodListsModel", b =>
                {
                    b.Navigation("FoodListEntry");

                    b.Navigation("FoodListLike");
                });

            modelBuilder.Entity("UsersModel", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("FoodList");

                    b.Navigation("FoodListLike");

                    b.Navigation("Post");

                    b.Navigation("PostLike");

                    b.Navigation("Reservation");

                    b.Navigation("Restaurant");
                });
#pragma warning restore 612, 618
        }
    }
}
