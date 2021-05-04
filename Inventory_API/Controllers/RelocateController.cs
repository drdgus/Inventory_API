using System;
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
    public class RelocateController : ControllerBase
    {
        private readonly ILogger<RelocateController> _logger;
        private InventoryDbContext _context;
        
        public RelocateController(ILogger<RelocateController> logger, InventoryDbContext context)
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
        public async Task<IActionResult> Post([FromForm]int roomId, [FromForm]int equipId)
        {
            try
            {
                _context.History.Add(new History
                {
                    itemId = equipId,
                    Date = DateTime.Now,
                    Code = History.OperationCode.Edited,
                    ChangedProperty = History.Property.Room,
                    OldValue = _context.Equips.Include(e => e.Room).Single(e => e.Id == equipId).Room.Name,
                    NewValue = _context.Rooms.Single(r => r.Id == roomId).Name
                });

                _context.Equips.Single(e => e.Id == equipId).RoomId = roomId;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(roomId + "       " + equipId + "\n" + e.Message);
            }

            return Ok();
        }
    }
}