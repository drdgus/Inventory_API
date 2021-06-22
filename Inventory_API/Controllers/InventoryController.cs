using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Entities;
using Inventory_API.Models;
using Inventory_API.Services;
using Inventory_API.Tools;
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
                var liteEquips = _context.Equips.Where(i => i.IsWriteOff == false).OrderBy(i => i.InvNum).Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    InvNum = e.InvNum.ToString("Т-0000000"),
                    Location = e.Room.Name,
                    Type = e.Type.Name
                }).ToList();

                return Ok(liteEquips);
            }

            var history = _context.History.AsNoTracking()
                .Where(h => h.ObjectId == id && h.TableCode ==InvEnums.Table.Equip)
                .ToList()
                .OrderByDescending(x => x.Id)
                .Take(5)
                .Select(h => new
                {
                    Date = h.Date.ToString("dd.MM.yy"),
                    ChangedProperty = h.ChangedProperty.GetStringValue(),
                    OldValue = h.OldValue,
                    NewValue = h.NewValue
                }).ToList();

            var customEquip = _context.Equips
                .Where(i => i.IsWriteOff == false)
                .AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Include(e => e.Accountability)
                .Include(e => e.History)
                .Include(e => e.MOL)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    InvNum = e.InvNum.ToString("Т-0000000"),
                    Location = e.Room.Name,
                    Mol = e.MOL.ShortFullName,
                    Type = e.Type.Name,
                    Status = e.Status.Name,
                    Accountability = e.Accountability.Name,
                    Note = e.Note,
                    Count = e.Count,
                    History = history
                }).Single();

            return Ok(customEquip);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Equip equip)
        {
            await _context.Equips.AddAsync(equip);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(Equip equip)
        {
            var eq = _context.Equips.Single(i => i.Id == equip.Id);
            eq = equip;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}