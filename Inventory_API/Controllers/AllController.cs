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
                    .Include(i => i.Org)
                    .ToList(),
                Accountabilities = _context.Accountabilities.AsNoTracking().ToList(),
                History = _context.History.AsNoTracking().ToList(),
                MOLs = _context.MOLs.AsNoTracking().ToList(),
                Orgs = _context.Orgs.AsNoTracking().ToList(),
                Rooms = _context.Rooms.AsNoTracking().ToList(),
                Types = _context.Types.AsNoTracking().ToList(),
                Statuses = _context.Statuses.AsNoTracking().ToList()
            };

            return Ok(allTables);
        }
    }
}
