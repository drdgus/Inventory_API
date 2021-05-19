using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.DAL
{
    public class InventoryDbContext :  DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Equip> Equips { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Org> Orgs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Room> Rooms { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Status> Statuses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Type> Types { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Accountability> Accountabilities { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<History> History { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Password> Passwords { get; set; }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(log => _logger.LogInformation(log));
    }
}