using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private InventoryDbContext _context;
        public InventoryController(ILogger<InventoryController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
            
            if (_context.Equips.Any()) return;
            _context.Equips.Add(new Equip
            {
                Name = "samsung scx-4100",
                InvNum = "T-0000001",
                Org = new Org
                {
                    Name = "МКОУ Таежнинская школа №20"
                },
                Room = new Room
                {
                    Name = "Каб. 101"
                },
                Type = new Models.Type()
                {
                    Name = "МФУ"
                } ,
                Status = new Status
                {
                    Name = "На балансе"
                },
                Accountability = new Accountability
                {
                    Name = "Основной баланс"
                },
                Note = "Ut aut doloremque nihil provident est et numquam. Quia sit earum eos voluptatem fugiat nulla earum est. Odit natus qui veritatis aut eaque consectetur voluptatem. Odit rerum qui",
                Count = 1
            });
            _context.SaveChanges();
        }
        
        /// <summary>
        /// Получение имущества.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>если id = 0, получаем весь список имущества с id, name, invNum</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null)
            {
                return Ok(_context.Equips.Select(e => new {Id = e.Id, Name = e.Name, InvNum = e.InvNum}).ToList());
            }
            return Ok(_context.Equips.AsNoTracking()
                .Include(e => e.Org)
                .Include(e => e.Room)
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Include(e => e.Accountability)
                .Single(e => e.Id == id));
        }
    }
}