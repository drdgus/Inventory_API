using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private InventoryDbContext _context;
        public RoomsController(ILogger<RoomsController>? logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Получение всех помещений.
        /// </summary>
        /// <returns>IEnumerable: id, name</returns>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(_context.Rooms.ToList());
    }
}