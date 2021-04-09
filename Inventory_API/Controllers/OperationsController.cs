using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private InventoryDbContext _context;
        
        public OperationsController(ILogger<InventoryController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Перемещение имущества в другое помещение.
        /// </summary>
        /// <param name="equipId"></param>
        /// <param name="roomId">Новое помещение</param>
        /// <returns>При успешном выполнении операции OK, иначе null</returns>
        [HttpPost]
        public async Task<IActionResult> Post(int equipId, int roomId)
        {
            try
            {
                _context.Equips.Single(e => e.Id == equipId).Room.Id = roomId;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}