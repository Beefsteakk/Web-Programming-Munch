using EffectiveWebProg.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EffectiveWebProg.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<PostsModel> Posts { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<PostLikesModel> PostLikes { get; set; }
        public DbSet<FoodListsModel> FoodLists { get; set; }
        public DbSet<FoodListEntriesModel> FoodListEntries { get; set; }
        public DbSet<FoodListLikesModel> FoodListLikes { get; set; }
        public DbSet<RestaurantsModel> Restaurants { get; set; }
        public DbSet<ReservationsModel> Reservations { get; set; }
        public DbSet<RestaurantRatingsModel> RestaurantRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationship between Users and Posts
            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.Post)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring the relationship between Posts and Comments
            modelBuilder.Entity<PostsModel>()
                .HasMany(p => p.Comment)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring the relationship for PostLikes
            modelBuilder.Entity<PostLikesModel>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLike)
                .HasForeignKey(pl => pl.PostID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostLikesModel>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.PostLike)
                .HasForeignKey(pl => pl.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring FoodLists and FoodListEntries
            modelBuilder.Entity<FoodListsModel>()
                .HasOne(fl => fl.Owner)
                .WithMany(u => u.FoodList)
                .HasForeignKey(fl => fl.OwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasOne(fle => fle.FoodList)
                .WithMany(fl => fl.FoodListEntry)
                .HasForeignKey(fle => fle.FoodListID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasOne(fle => fle.Restaurant)
                .WithMany(r => r.FoodEntry)
                .HasForeignKey(fle => fle.RestaurantID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring FoodListLikes
            modelBuilder.Entity<FoodListLikesModel>()
                .HasOne(fll => fll.FoodList)
                .WithMany(fl => fl.FoodListLike)
                .HasForeignKey(fll => fll.FoodListID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FoodListLikesModel>()
                .HasOne(fll => fll.User)
                .WithMany(u => u.FoodListLike)
                .HasForeignKey(fll => fll.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring Restaurants and Reservations
            modelBuilder.Entity<RestaurantsModel>()
                .HasOne(r => r.Owner)
                .WithMany(u => u.Restaurant)
                .HasForeignKey(r => r.OwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationsModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservation)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationsModel>()
                .HasOne(r => r.Restaurant)
                .WithMany(res => res.Reservation)
                .HasForeignKey(r => r.RestID)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuring Restaurants Rating
            modelBuilder.Entity<RestaurantRatingsModel>()
                .HasOne(rr => rr.User)
                .WithMany(u => u.RestaurantRating)
                .HasForeignKey(rr => rr.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RestaurantRatingsModel>()
                .HasOne(rr => rr.Restaurant)
                .WithMany(res => res.RestaurantRating)
                .HasForeignKey(rr => rr.RestID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}