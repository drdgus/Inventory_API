using Inventory_API.Entities;
using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.DAL
{
    public class InventoryDbContext :  DbContext
    {
        public DbSet<Equip> Equips { get; set; }
        public DbSet<Org> Orgs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Accountability> Accountabilities { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<MOL> MOLs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UnappliedChange> UnappliedChanges { get; set; }
        public DbSet<InvDocument> InvDocuments { get; set; }
        public DbSet<MOLPosition> MolPositions { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CheckEquip> CheckEquips { get; set; }

        private ILogger<InventoryDbContext> _logger;

        public InventoryDbContext(ILogger<InventoryDbContext> logger, DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
            _logger = logger;
            Database.EnsureCreated();
            new DbInitializer(this);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Equip>().HasIndex(e => e.InvNum).IsUnique();
            //modelBuilder.Entity<Room>().HasIndex(r => r.Name).IsUnique();
            //modelBuilder.Entity<Room>().HasIndex(r => r.MOL).IsUnique();
            //modelBuilder.Entity<Type>().HasIndex(r => r.Name).IsUnique();
            //modelBuilder.Entity<Status>().HasIndex(r => r.Name).IsUnique();
            //modelBuilder.Entity<Accountability>().HasIndex(r => r.Name).IsUnique();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.LogTo(log => _logger.LogInformation(log));
    }
}