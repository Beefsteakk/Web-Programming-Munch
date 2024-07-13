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
        public DbSet<CartModel> Cart { get; set; }
        public DbSet<CartItemsModel> CartItems { get; set; }

        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure CreditCard
            modelBuilder.Entity<CreditCardModel>(entity =>
            {
                entity.HasKey(e => e.CardID);
                entity.Property(e => e.CardID).IsRequired().HasMaxLength(36);
            });

            // Configure ItemCat
            modelBuilder.Entity<ItemCatModel>(entity =>
            {
                entity.HasKey(e => e.CatID);
                entity.Property(e => e.CatID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.CatName).HasMaxLength(255);
            });

            // Configure Items
            modelBuilder.Entity<ItemsModel>(entity =>
            {
                entity.HasKey(e => e.ItemID);
                entity.Property(e => e.ItemID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.CatID).HasMaxLength(36);
                entity.Property(e => e.ItemName).HasMaxLength(255);
                entity.Property(e => e.ItemPic).HasMaxLength(255);
                entity.HasIndex(e => e.CatID);
                entity.HasOne(e => e.ItemCat)
                    .WithMany(c => c.Item)
                    .HasForeignKey(e => e.CatID)
                    .HasConstraintName("Items_ibfk_1");
            });

            // Configure Restaurants
            modelBuilder.Entity<RestaurantsModel>(entity =>
            {
                entity.HasKey(e => e.RestID);
                entity.Property(e => e.RestID).IsRequired().HasMaxLength(36).HasDefaultValueSql("uuid()");
                entity.Property(e => e.RestName).HasMaxLength(255);
                entity.Property(e => e.RestEmail).HasMaxLength(50);
                entity.Property(e => e.RestPassword).HasMaxLength(255);
                entity.Property(e => e.RestContact).HasColumnType("int");
                entity.Property(e => e.RestBio).HasColumnType("text");
                entity.Property(e => e.RestPic).HasColumnType("longblob");
                entity.Property(e => e.RestWebsite).HasMaxLength(255);
                entity.Property(e => e.RestCoverPic).HasMaxLength(255);
            });

            // Configure Cart
            modelBuilder.Entity<CartModel>(entity =>
            {
                entity.HasKey(e => e.CartID);
                entity.Property(e => e.CartID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.RestID).HasMaxLength(36);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.CartName).HasMaxLength(255);
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.Rest)
                    .WithMany(r => r.Cart)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Cart_ibfk_1");
            });

            // Configure CartItems
            modelBuilder.Entity<CartItemsModel>(entity =>
            {
                entity.HasKey(e => new { e.CartID, e.ItemID });
                entity.Property(e => e.CartID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.ItemID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.Quantity).HasColumnType("int");
                entity.HasIndex(e => e.ItemID);
                entity.HasOne(e => e.Cart)
                    .WithMany(c => c.CartItem)
                    .HasForeignKey(e => e.CartID)
                    .HasConstraintName("CartItems_ibfk_1");
                entity.HasOne(e => e.Items)
                    .WithMany(i => i.CartItem)
                    .HasForeignKey(e => e.ItemID)
                    .HasConstraintName("CartItems_ibfk_2");
            });

            // Configure Employees
            modelBuilder.Entity<EmployeesModel>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);
                entity.Property(e => e.EmployeeID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.RestID).HasMaxLength(36);
                entity.Property(e => e.EmployeeName).HasMaxLength(255);
                entity.Property(e => e.EmployeePic).HasMaxLength(255);
                entity.Property(e => e.Role).HasMaxLength(20);
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.Restaurant)
                    .WithMany(r => r.Employee)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Employees_ibfk_1");
            });

            // Configure Inventory
            modelBuilder.Entity<InventoryModel>(entity =>
            {
                entity.HasKey(e => e.InventoryID);
                entity.Property(e => e.InventoryID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.RestID).HasMaxLength(36);
                entity.Property(e => e.InventoryName).HasMaxLength(255);
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.Rest)
                    .WithMany(r => r.Inventory)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Inventory_ibfk_1");
            });

            // Configure InventoryItems
            modelBuilder.Entity<InventoryItemsModel>(entity =>
            {
                entity.HasKey(e => new { e.InventoryID, e.ItemID });
                entity.Property(e => e.InventoryID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.ItemID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.StockCount).HasColumnType("int");
                entity.HasIndex(e => e.ItemID);
                entity.HasOne(e => e.Inventory)
                    .WithMany(i => i.InventoryItem)
                    .HasForeignKey(e => e.InventoryID)
                    .HasConstraintName("InventoryItems_ibfk_1");
                entity.HasOne(e => e.Items)
                    .WithMany(i => i.InventoryItem)
                    .HasForeignKey(e => e.ItemID)
                    .HasConstraintName("InventoryItems_ibfk_2");
            });

            // Configure Posts
            modelBuilder.Entity<PostsModel>(entity =>
            {
                entity.HasKey(e => e.PostID);
                entity.Property(e => e.PostID).IsRequired().HasMaxLength(36).HasDefaultValueSql("uuid()");
                entity.Property(e => e.RestID).HasMaxLength(36);
                entity.Property(e => e.PostContent).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.Restaurant)
                    .WithMany(r => r.Post)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Posts_ibfk_2");
            });

            // Configure PostPics
            modelBuilder.Entity<PostPicsModel>(entity =>
            {
                entity.HasKey(e => e.PicID);
                entity.Property(e => e.PicID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.PostID).HasMaxLength(36);
                entity.Property(e => e.ImageURL).HasMaxLength(255);
                entity.HasOne(e => e.Post)
                    .WithMany(p => p.PostPic)
                    .HasForeignKey(e => e.PostID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PostPics_ibfk_1");
            });

            // Configure RestCard
            modelBuilder.Entity<RestCardModel>(entity =>
            {
                entity.HasKey(e => new { e.RestID, e.CardID });
                entity.Property(e => e.RestID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.CardID).IsRequired().HasMaxLength(36);
                entity.HasIndex(e => e.CardID);
                entity.HasOne(e => e.Rest)
                    .WithMany(r => r.RestCard)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("RestCard_ibfk_1");
                entity.HasOne(e => e.Card)
                    .WithMany(c => c.RestCard)
                    .HasForeignKey(e => e.CardID)
                    .HasConstraintName("RestCard_ibfk_2");
            });

            // Configure TimeSheet
            modelBuilder.Entity<TimeSheetModel>(entity =>
            {
                entity.HasKey(e => e.SheetID);
                entity.Property(e => e.SheetID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.EmployeeID).HasMaxLength(36);
                entity.Property(e => e.Day).HasMaxLength(50);
                entity.HasIndex(e => e.EmployeeID);
                entity.HasOne(e => e.Employees)
                    .WithMany(e => e.TimeSheets)
                    .HasForeignKey(e => e.EmployeeID)
                    .HasConstraintName("TimeSheet_ibfk_1");
            });

            // Configure Users
            modelBuilder.Entity<UsersModel>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).IsRequired().HasMaxLength(36).HasDefaultValueSql("uuid()");
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(255);
                entity.Property(e => e.UserUsername).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserPassword).IsRequired().HasMaxLength(255);
                entity.Property(e => e.UserProfilePic).HasMaxLength(255);
            });

            // Configure Comments
            modelBuilder.Entity<CommentsModel>(entity =>
            {
                entity.HasKey(e => e.CommentID);
                entity.Property(e => e.CommentID).IsRequired().HasMaxLength(36).HasDefaultValueSql("uuid()");
                entity.Property(e => e.PostID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.UserID).HasMaxLength(36);
                entity.Property(e => e.RestID).HasMaxLength(36);
                entity.Property(e => e.CommentContent).IsRequired().HasColumnType("text");
                entity.HasIndex(e => e.UserID).HasName("AuthorID");
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(e => e.PostID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comments_ibfk_1");
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Comment)
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("Comments_ibfk_2");
                entity.HasOne(e => e.Restaurant)
                    .WithMany(r => r.Comment)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Comments_ibfk_3");
            });

            // Configure Followings
            modelBuilder.Entity<FollowingsModel>(entity =>
            {
                entity.HasKey(e => new { e.UserID, e.RestID });
                entity.Property(e => e.UserID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.RestID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.FollowCreatedAt).HasColumnType("datetime");
                entity.HasIndex(e => e.RestID);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Followings)
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("Followings_ibfk_1");
                entity.HasOne(e => e.Rest)
                    .WithMany(r => r.Followings)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Followings_ibfk_2");
            });

            // Configure PostLikes
            modelBuilder.Entity<PostLikesModel>(entity =>
            {
                entity.HasKey(e => new { e.PostID, e.UserID });
                entity.Property(e => e.PostID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.UserID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.LikeCreatedAt).HasColumnType("datetime");
                entity.HasIndex(e => e.UserID);
                entity.HasOne(e => e.Post)
                    .WithMany(p => p.PostLike)
                    .HasForeignKey(e => e.PostID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PostLikes_ibfk_1");
                entity.HasOne(e => e.User)
                    .WithMany(u => u.PostLike)
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("PostLikes_ibfk_2");
            });

            // Configure Reservations
            modelBuilder.Entity<ReservationsModel>(entity =>
            {
                entity.HasKey(e => e.ReservationID);
                entity.Property(e => e.ReservationID).IsRequired().HasMaxLength(36).HasDefaultValueSql("uuid()");
                entity.Property(e => e.UserID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.RestID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.SpecialRequest).HasMaxLength(255);
                entity.Property(e => e.ReservationStatus).HasMaxLength(50);
                entity.Property(e => e.ReservedName).HasMaxLength(200);
                entity.HasIndex(e => e.RestID);
                entity.HasIndex(e => e.UserID);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Reservation)
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("Reservations_ibfk_1");
                entity.HasOne(e => e.Restaurant)
                    .WithMany(r => r.Reservation)
                    .HasForeignKey(e => e.RestID)
                    .HasConstraintName("Reservations_ibfk_2");
            });

            // Configure UserCard
            modelBuilder.Entity<UserCardModel>(entity =>
            {
                entity.HasKey(e => new { e.UserID, e.CardID });
                entity.Property(e => e.UserID).IsRequired().HasMaxLength(36);
                entity.Property(e => e.CardID).IsRequired().HasMaxLength(36);
                entity.HasIndex(e => e.CardID);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserCard)
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("UserCard_ibfk_1");
                entity.HasOne(e => e.Card)
                    .WithMany(c => c.UserCard)
                    .HasForeignKey(e => e.CardID)
                    .HasConstraintName("UserCard_ibfk_2");
            });
        }
    }

}
