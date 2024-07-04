using EffectiveWebProg.Models;

namespace EffectiveWebProg.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<UserCardModel> UserCards { get; set; }
        public DbSet<TimeSheetModel> TimeSheets { get; set; }
        public DbSet<RestCardModel> RestCards { get; set; }
        public DbSet<RestaurantsModel> Restaurants { get; set; }
        public DbSet<ReservationsModel> Reservations { get; set; }
        public DbSet<PostsModel> Posts { get; set; }
        public DbSet<PostPicsModel> PostPics { get; set; }
        public DbSet<PostLikesModel> PostLikes { get; set; }
        public DbSet<ItemsModel> Items { get; set; }
        public DbSet<ItemCatModel> ItemCat { get; set; }
        public DbSet<InventoryModel> Inventory { get; set; }
        public DbSet<InventoryItemsModel> InventoryItems { get; set; }
        public DbSet<FollowingsModel> Followings { get; set; }
        public DbSet<EmployeesModel> Employees { get; set; }
        public DbSet<CreditCardModel> CreditCard { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartItemsModel> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UsersModel relationships
            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.Comment)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.PostLike)
                .WithOne(pl => pl.User)
                .HasForeignKey(pl => pl.UserID);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.Reservation)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.Followings)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserID);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.UserCard)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserID);

            // UserCardModel composite key
            modelBuilder.Entity<UserCardModel>()
                .HasKey(uc => new { uc.CardID, uc.UserID });

            // TimeSheetModel relationships
            modelBuilder.Entity<TimeSheetModel>()
                .HasOne(ts => ts.Employees)
                .WithMany(e => e.TimeSheets)
                .HasForeignKey(ts => ts.EmployeeID);

            // RestCardModel composite key
            modelBuilder.Entity<RestCardModel>()
                .HasKey(rc => new { rc.CardID, rc.RestID });

            // RestaurantsModel relationships
            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Reservation)
                .WithOne(rs => rs.Restaurant)
                .HasForeignKey(rs => rs.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Comment)
                .WithOne(c => c.Restaurant)
                .HasForeignKey(c => c.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Post)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.RestCard)
                .WithOne(rc => rc.Rest)
                .HasForeignKey(rc => rc.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Inventory)
                .WithOne(i => i.Rest)
                .HasForeignKey(i => i.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Cart)
                .WithOne(c => c.Rest)
                .HasForeignKey(c => c.RestID);

            modelBuilder.Entity<RestaurantsModel>()
                .HasMany(r => r.Employee)
                .WithOne(e => e.Restaurant)
                .HasForeignKey(e => e.RestID);

            // ReservationsModel relationships
            modelBuilder.Entity<ReservationsModel>()
                .HasOne(rs => rs.User)
                .WithMany(u => u.Reservation)
                .HasForeignKey(rs => rs.UserID);

            modelBuilder.Entity<ReservationsModel>()
                .HasOne(rs => rs.Restaurant)
                .WithMany(r => r.Reservation)
                .HasForeignKey(rs => rs.RestID);

            // PostsModel relationships
            modelBuilder.Entity<PostsModel>()
                .HasMany(p => p.Comment)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID);

            modelBuilder.Entity<PostsModel>()
                .HasMany(p => p.PostLike)
                .WithOne(pl => pl.Post)
                .HasForeignKey(pl => pl.PostID);

            modelBuilder.Entity<PostsModel>()
                .HasMany(p => p.PostPic)
                .WithOne(pp => pp.Post)
                .HasForeignKey(pp => pp.PostID);

            // PostLikesModel composite key
            modelBuilder.Entity<PostLikesModel>()
                .HasKey(pl => new { pl.PostID, pl.UserID });

            // ItemsModel relationships
            modelBuilder.Entity<ItemsModel>()
                .HasOne(i => i.ItemCat)
                .WithMany(ic => ic.Item)
                .HasForeignKey(i => i.CatID);

            modelBuilder.Entity<ItemsModel>()
                .HasMany(i => i.InventoryItem)
                .WithOne(ii => ii.Items)
                .HasForeignKey(ii => ii.ItemID);

            modelBuilder.Entity<ItemsModel>()
                .HasMany(i => i.CartItem)
                .WithOne(ci => ci.Items)
                .HasForeignKey(ci => ci.ItemID);

            // InventoryItemsModel composite key
            modelBuilder.Entity<InventoryItemsModel>()
                .HasKey(ii => new { ii.InventoryID, ii.ItemID });

            // InventoryModel relationships
            modelBuilder.Entity<InventoryModel>()
                .HasMany(i => i.InventoryItem)
                .WithOne(ii => ii.Inventory)
                .HasForeignKey(ii => ii.InventoryID);

            // FollowingsModel composite key
            modelBuilder.Entity<FollowingsModel>()
                .HasKey(f => new { f.UserID, f.RestID });

            // EmployeesModel relationships
            modelBuilder.Entity<EmployeesModel>()
                .HasMany(e => e.TimeSheets)
                .WithOne(ts => ts.Employees)
                .HasForeignKey(ts => ts.EmployeeID);

            modelBuilder.Entity<EmployeesModel>()
                .HasOne(e => e.Restaurant)
                .WithMany(r => r.Employee)
                .HasForeignKey(e => e.RestID);

            // CreditCardModel relationships
            modelBuilder.Entity<CreditCardModel>()
                .HasMany(cc => cc.UserCard)
                .WithOne(uc => uc.Card)
                .HasForeignKey(uc => uc.CardID);

            modelBuilder.Entity<CreditCardModel>()
                .HasMany(cc => cc.RestCard)
                .WithOne(rc => rc.Card)
                .HasForeignKey(rc => rc.CardID);

            // CommentsModel relationships
            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comment)
                .HasForeignKey(c => c.PostID);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comment)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Restaurant)
                .WithMany(r => r.Comment)
                .HasForeignKey(c => c.RestID);

            // CartModel relationships
            modelBuilder.Entity<CartModel>()
                .HasMany(c => c.CartItem)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartID);

            modelBuilder.Entity<CartModel>()
                .HasOne(c => c.Rest)
                .WithMany(r => r.Cart)
                .HasForeignKey(c => c.RestID);

            // CartItemsModel composite key
            modelBuilder.Entity<CartItemsModel>()
                .HasKey(ci => new { ci.CartID, ci.ItemID });

            base.OnModelCreating(modelBuilder);
        }
    }

}
