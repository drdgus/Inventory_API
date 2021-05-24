using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Inventory_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.v2.Controllers
{
    [ApiController]
    [Route("v2/[controller]")]
    public class V2InventoryController : ControllerBase
    {
        private readonly ILogger<V2InventoryController> _logger;
        private InventoryDbContext _context;
        public V2InventoryController(ILogger<V2InventoryController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;

            if (_context.Equips.Count() > 5) return;
            var org = _context.Orgs.First();
            var rooms = _context.Rooms.ToList();
            var equips10K = new List<Equip>();
            var types = _context.Types.ToList();
            var status = _context.Statuses.First();
            var mols = _context.MOLs.ToList();
            var accountability = _context.Accountabilities.First();

            var rnd = new Random();
            for (var i = 5; i < 10000; i++)
            {
                var date = DateTime.Now.AddDays(rnd.Next(-110, 0));
                var room = rooms[rnd.Next(rooms.Count)];

                equips10K.Add(new Equip
                {
                    Id = i,
                    RegistrationDate = date,
                    Name = $"name{i}",
                    InvNum = i,
                    Org = org,
                    RoomId = room.Id,
                    Room = room,
                    Type = types[rnd.Next(types.Count)],
                    Status = status,
                    Accountability = accountability,
                    History = new List<History>()
                    {
                        new History
                        {
                            itemId = i,
                            Date = date,
                            Code = History.OperationCode.Created,
                            ChangedProperty = History.Property.None,
                            OldValue = "",
                            NewValue = ""
                        }
                    },
                    Note = "",
                    Count = 1,
                    IsDeleted = false,
                    MOL = mols[rnd.Next(mols.Count)],
                    ReleaseDate = default,
                    BasePrice = rnd.Next(3000, 50000),
                    DepreciationRate = 5,
                    DepreciationGroup = Equip.DepreciationGroups.I
                });
            }

            _context.Database.BeginTransaction();
            _context.Equips.AddRange(equips10K);
            _context.Database.CommitTransaction();
            _context.SaveChanges();
        }
        
        /// <summary>
        /// Получение имущества.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>если id = 0, получаем весь список имущества с id, name, invNum</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = _context.Equips
                .AsNoTracking()
                .Include(e => e.Org)
                .Include(e => e.Room)
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Include(e => e.Accountability)
                .Include(e => e.History)
                .ToList();

    

            return Ok(a);
        }
    }
}