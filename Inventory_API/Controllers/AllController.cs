using Inventory_API.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllController : ControllerBase
    {
        private readonly ILogger<AllController> _logger;
        private InventoryDbContext _context;

        public AllController(ILogger<AllController>? logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allTables = new
            {
                Equips = _context.Equips
                    .AsNoTracking()
                    .Include(i => i.Accountability)
                    .Include(i => i.History)
                    .Include(i => i.MOL)
                    .Include(i => i.Type)
                    .Include(i => i.Status)
                    .Include(i => i.Room)
                    .Include(i => i.Room.Org)
                    .Include(i => i.Manufacturer)
                    .ToListAsync().Result,
                Categories = _context.Categories.ToListAsync().Result,
                Accountabilities = _context.Accountabilities.AsNoTracking().ToListAsync().Result,
                History = _context.History.AsNoTracking().ToListAsync().Result,
                MOLs = _context.MOLs.AsNoTracking().ToListAsync().Result,
                Orgs = _context.Orgs.AsNoTracking().ToListAsync().Result,
                Rooms = _context.Rooms.AsNoTracking().ToListAsync().Result,
                Types = _context.Types.AsNoTracking().ToListAsync().Result,
                Statuses = _context.Statuses.AsNoTracking().ToListAsync().Result,
                Manufacturers = _context.Manufacturers.AsNoTracking().ToListAsync().Result,
                InvDocuments = _context.InvDocuments.AsNoTracking().ToListAsync().Result,
                MolPositions = _context.MolPositions.AsNoTracking().ToListAsync().Result
            };

            return Ok(allTables);
        }
    }
}
