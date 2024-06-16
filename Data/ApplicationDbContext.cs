using System;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<RestaurantsModel> Restaurants { get; set; }
        public DbSet<PostsModel> Posts { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<PostLikesUserModel> PostLikesUser { get; set; }
        public DbSet<PostLikesRestModel> PostLikesRest { get; set; }
        public DbSet<UserFollowingsModel> UserFollowings { get; set; }
        public DbSet<RestaurantFollowingsModel> RestaurantFollowings { get; set; }
        public DbSet<ReviewsModel> Reviews { get; set; }
        public DbSet<ReservationsModel> Reservations { get; set; }
        public DbSet<FoodListsModel> FoodLists { get; set; }
        public DbSet<FoodListLikesModel> FoodListLikes { get; set; }
        public DbSet<FoodListEntriesModel> FoodListEntries { get; set; }
        public DbSet<ForumsModel> Forums { get; set; }
        public DbSet<ForumVotesModel> ForumVotes { get; set; }
        public DbSet<ForumCommentsModel> ForumComments { get; set; }
        public DbSet<PostPicsModel> PostPics { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<RestCategoryModel> RestCategory { get; set; }
        public DbSet<FoodListCategoryModel> FoodListCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Keys
            modelBuilder.Entity<PostLikesUserModel>()
                .HasKey(plu => new { plu.PostID, plu.UserID });

            modelBuilder.Entity<PostLikesRestModel>()
                .HasKey(plr => new { plr.PostID, plr.RestID });

            modelBuilder.Entity<UserFollowingsModel>()
                .HasKey(uf => new { uf.UserID, uf.FollowedUserID });

            modelBuilder.Entity<RestaurantFollowingsModel>()
                .HasKey(rf => new { rf.UserID, rf.FollowedRestID });

            modelBuilder.Entity<FoodListLikesModel>()
                .HasKey(fll => new { fll.FoodListID, fll.UserID });

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasKey(fle => new { fle.FoodListID, fle.RestID });

            modelBuilder.Entity<ForumVotesModel>()
                .HasKey(fv => new { fv.ForumID, fv.UserID });

            modelBuilder.Entity<RestCategoryModel>()
                .HasKey(rc => new { rc.RestID, rc.CatID });

            modelBuilder.Entity<FoodListCategoryModel>()
                .HasKey(fc => new { fc.FoodListID, fc.CatID });

            // Foreign Key Constraints
            modelBuilder.Entity<ForumCommentsModel>()
                .HasOne(fc => fc.Forum)
                .WithMany(f => f.ForumComment)
                .HasForeignKey(fc => fc.ForumID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumCommentsModel>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.ForumComment)
                .HasForeignKey(fc => fc.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationsModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservation)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationsModel>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservation)
                .HasForeignKey(r => r.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comment)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostsModel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Post)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostsModel>()
                .HasOne(p => p.Restaurant)
                .WithMany(r => r.Post)
                .HasForeignKey(p => p.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFollowingsModel>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFollowingsModel>()
                .HasOne(uf => uf.FollowedUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowedUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantFollowingsModel>()
                .HasOne(rf => rf.User)
                .WithMany(u => u.RestaurantFollowings)
                .HasForeignKey(rf => rf.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantFollowingsModel>()
                .HasOne(rf => rf.FollowedRest)
                .WithMany(r => r.FollowedBy)
                .HasForeignKey(rf => rf.FollowedRestID)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<PostPicsModel>()
                .HasOne(p => p.Post)
                .WithMany(p => p.PostPic)
                .HasForeignKey(p => p.PostID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestCategoryModel>()
                .HasOne(rc => rc.Restaurant)
                .WithMany(r => r.RestCat)
                .HasForeignKey(rc => rc.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestCategoryModel>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RestCat)
                .HasForeignKey(rc => rc.CatID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListCategoryModel>()
                .HasOne(fc => fc.FoodList)
                .WithMany(f => f.FoodListCat)
                .HasForeignKey(fc => fc.FoodListID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListCategoryModel>()
                .HasOne(fc => fc.Category)
                .WithMany(c => c.FoodListCat)
                .HasForeignKey(fc => fc.CatID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchHistoryModel>()
                .HasOne(s => s.User)
                .WithMany(u => u.SearchHistory)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestViewHistoryModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.RestViewHistory)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestViewHistoryModel>()
                .HasOne(r => r.Restaurant)
                .WithMany(res => res.RestViewHistory)
                .HasForeignKey(r => r.RestID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
