﻿using Microsoft.AspNetCore.Mvc;
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
                var liteEquips = _context.Equips.Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    InvNum = e.InvNum,
                    Location = e.Room.Name
                }).ToList();

                return Ok(liteEquips);
            }

            var history = _context.History.AsNoTracking()
                .Where(h => h.Code == History.OperationCode.Edited && h.itemId == id)
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