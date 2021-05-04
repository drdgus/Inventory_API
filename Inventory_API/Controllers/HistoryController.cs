using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Inventory_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        private InventoryDbContext _context;

        public HistoryController(ILogger<HistoryController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null) BadRequest();

            var history = _context.History.OrderByDescending(h => h.Id)
                .Where(h => h.itemId == id && h.Code == History.OperationCode.Edited)
                .Select(h => new
                {
                    Date = h.Date.ToString("dd.MM.yy"),
                    ChangedProperty = h.ChangedProperty.GetStringValue(),
                    OldValue = h.OldValue,
                    NewValue = h.NewValue
                }).ToList();

            return Ok(history);
        }
    }
}
