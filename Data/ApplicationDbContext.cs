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
        public DbSet<RestViewHistoryModel> RestViewHistory { get; set; }
        public DbSet<SearchHistoryModel> SearchHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite keys
            modelBuilder.Entity<RestCategoryModel>()
                .HasKey(rc => new { rc.RestID, rc.CatID });

            modelBuilder.Entity<FoodListCategoryModel>()
                .HasKey(fc => new { fc.FoodListID, fc.CatID });

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasKey(fe => new { fe.FoodListID, fe.RestID });

            modelBuilder.Entity<ForumVotesModel>()
                .HasKey(fv => new { fv.ForumID, fv.UserID });

            modelBuilder.Entity<PostLikesRestModel>()
                .HasKey(plr => new { plr.PostID, plr.RestID });

            modelBuilder.Entity<PostLikesUserModel>()
                .HasKey(plu => new { plu.PostID, plu.UserID });

            modelBuilder.Entity<FoodListLikesModel>()
                .HasKey(fll => new { fll.FoodListID, fll.UserID });

            modelBuilder.Entity<RestaurantFollowingsModel>()
                .HasKey(rf => new { rf.UserID, rf.FollowedRestID });

            modelBuilder.Entity<UserFollowingsModel>()
                .HasKey(uf => new { uf.UserID, uf.FollowedUserID });

            // Define relationships
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

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasOne(fe => fe.FoodList)
                .WithMany(f => f.FoodListEntry)
                .HasForeignKey(fe => fe.FoodListID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListEntriesModel>()
                .HasOne(fe => fe.Restaurant)
                .WithMany(r => r.FoodEntry)
                .HasForeignKey(fe => fe.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumVotesModel>()
                .HasOne(fv => fv.Forum)
                .WithMany(f => f.ForumVote)
                .HasForeignKey(fv => fv.ForumID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumVotesModel>()
                .HasOne(fv => fv.User)
                .WithMany(u => u.ForumVote)
                .HasForeignKey(fv => fv.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikesRestModel>()
                .HasOne(plr => plr.Post)
                .WithMany(p => p.PostLikeRest)
                .HasForeignKey(plr => plr.PostID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikesRestModel>()
                .HasOne(plr => plr.Restaurant)
                .WithMany(r => r.PostLikesRest)
                .HasForeignKey(plr => plr.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikesUserModel>()
                .HasOne(plu => plu.Post)
                .WithMany(p => p.PostLikeUser)
                .HasForeignKey(plu => plu.PostID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikesUserModel>()
                .HasOne(plu => plu.User)
                .WithMany(u => u.PostLikeUser)
                .HasForeignKey(plu => plu.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListLikesModel>()
                .HasOne(fll => fll.FoodList)
                .WithMany(f => f.FoodListLike)
                .HasForeignKey(fll => fll.FoodListID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListLikesModel>()
                .HasOne(fll => fll.User)
                .WithMany(u => u.FoodListLike)
                .HasForeignKey(fll => fll.UserID)
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

            modelBuilder.Entity<PostsModel>()
                .HasOne(p => p.TaggedRestaurant)
                .WithMany(r => r.TaggedRest)
                .HasForeignKey(p => p.TaggedRest)
                .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<ForumCommentsModel>()
                .HasOne(fc => fc.Restaurant)
                .WithMany(r => r.ForumComment)
                .HasForeignKey(fc => fc.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comment)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comment)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Restaurant)
                .WithMany(r => r.Comment)
                .HasForeignKey(c => c.RestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodListsModel>()
                .HasOne(f => f.User)
                .WithMany(u => u.FoodList)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
