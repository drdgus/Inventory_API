using Inventory_API.DAL;
using Inventory_API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    public class UnappliedChangeController
    {
        private readonly InventoryDbContext _context;

        public UnappliedChangeController(InventoryDbContext context)
        {
            _context = context;
        }
        public async void AddUnappliedChange(UnappliedChange unappliedChange)
        {
            await _context.UnappliedChanges.AddAsync(unappliedChange);
        }
    }
}
