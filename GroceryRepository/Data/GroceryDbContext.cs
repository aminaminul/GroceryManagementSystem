using Microsoft.EntityFrameworkCore;
using GroceryModel;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GroceryRepository.Data
{
    public class GroceryDbContext : IdentityDbContext<User>
    {
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Decimal Precision
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            // Resolve Multiple Cascade Paths
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Cans", Description = "Contains the essential nutrients your kitty needs to support a strong immune system." },
                new Category { Id = 2, Name = "Treats", Description = "Your kitty will love the crunchy outside and soft and creamy inside." },
                new Category { Id = 3, Name = "Toys", Description = "Stimulates your kitty’s natural hunting instincts to spark up play and exercise." }
            );

            // Need to ensure Supplier exists for seeded Products
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Default Supplier", ContactPerson = "System", PhoneNumber = "000-000-0000" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1, SupplierId = 1, Name = "Applaws Mousse Tuna Grain-Free Wet Cat Food", StockQuantity = 100, UnitPrice = 1.99m },
                new Product { Id = 2, CategoryId = 1, SupplierId = 1, Name = "Applaws Chicken Breast with Pumpkin Canned Cat Food", StockQuantity = 200, UnitPrice = 1.99m },
                new Product { Id = 3, CategoryId = 2, SupplierId = 1, Name = "Temptations MixUps Surfers' Delight Flavor Soft & Crunchy Cat Treats", StockQuantity = 300, UnitPrice = 15.50m },
                new Product { Id = 4, CategoryId = 2, SupplierId = 1, Name = "Inaba Churu for Kittens Chicken Recipe Puree Grain-Free Lickable Cat Treats", StockQuantity = 100, UnitPrice = 3.99m },
                new Product { Id = 5, CategoryId = 3, SupplierId = 1, Name = "Frisco Bird with Feathers Teaser Wand Cat Toy with Catnip", StockQuantity = 100, UnitPrice = 4.08m },
                new Product { Id = 6, CategoryId = 3, SupplierId = 1, Name = "Quirky Kitty Cute Koi Wand Cat Toy", StockQuantity = 100, UnitPrice = 8.99m }
            );
        }
    }
}
