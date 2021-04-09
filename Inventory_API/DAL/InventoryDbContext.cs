using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;

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

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}