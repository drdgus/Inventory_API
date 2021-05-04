using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Inventory_API.Services;
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
                    Name = "Каб. 101",
                    MOL = "Иванов Иван Иванович"
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
                Count = 1, 
                History = new List<History>
                {
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Created,
                        ChangedProperty = History.Property.None,
                        OldValue = "",
                        NewValue = "scx-4100"
                    },
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Room,
                        OldValue = "Каб. 101",
                        NewValue = "Каб. 999"
                    },
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Type,
                        OldValue = "МФУ",
                        NewValue = "МФУУУУ"
                    },new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Status,
                        OldValue = "На балансе",
                        NewValue = "Списано"
                    }, new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Accountability,
                        OldValue = "Основной баланс",
                        NewValue = "з/б"
                    }, new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Note,
                        OldValue = "",
                        NewValue = "Ut aut doloremque nihil provident est et numquam. Quia sit earum eos voluptatem fugiat nulla earum est. Odit natus qui veritatis aut eaque consectetur voluptatem. Odit rerum qui"
                    },
                }
            });

            _context.Rooms.AddRange(new List<Room>
            {
                new Room()
                {
                    Name = "Каб. 102",
                    MOL = "Петров П. П."
                },new Room()
                {
                    Name = "Каб. 103",
                    MOL = "МОЛ ФИО3"
                },new Room()
                {
                    Name = "Каб. 201",
                    MOL = "МОЛ ФИО4"
                },new Room()
                {
                    Name = "Каб. 202",
                    MOL = "МОЛ ФИО5"
                },new Room()
                {
                    Name = "Каб. 301",
                    MOL = "МОЛ ФИО6"
                },
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
                return Ok(_context.Equips.Select(e => new {Id = e.Id, Name = e.Name, InvNum = e.InvNum, Location = e.Room.Name}).ToList());
            }

            var history = _context.History.AsNoTracking()
                .Where(h => h.Code == History.OperationCode.Edited && h.itemId == id)
                .ToList();

            history = history.OrderByDescending(x => x.Date).Take(5).ToList();

            var customEquip = _context.Equips.AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Status)
                .Include(e => e.Accountability)
                .Include(e => e.History)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    InvNum = e.InvNum,
                    Location = e.Room.Name,
                    Mol = e.Room.MOL,
                    Type = e.Type.Name,
                    Status = e.Status.Name,
                    Accountability = e.Accountability.Name,
                    Note = e.Note,
                    Count = e.Count,
                    History = history
                }).Single();

            return Ok(customEquip);

        }
    }
}