using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Inventory_API.Services;
using Inventory_API.Tools;
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

        /// <summary>
        /// История по эквипу.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var history = _context.History
                .OrderByDescending(h => h.Id)
                .Where(h => h.ObjectId == id && h.TableCode == InvEnums.Table.Equip)
                .Select(h => new
                {
                    Date = h.Date.ToString("dd.MM.yy"),
                    ChangedProperty = h.ChangedProperty.GetStringValue(),
                    OldValue = h.OldValue,
                    NewValue = h.NewValue
                }).ToList();

            return Ok(history);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(int id, InvEnums.Table tableCode)
        //{
            

        //    var history = _context.History
        //        .OrderByDescending(h => h.Id)
        //        .Where(h => h.ObjectId == id && h.TableCode ==InvEnums.Table.Equip)
        //        .Select(h => new
        //        {
        //            Date = h.Date.ToString("dd.MM.yy"),
        //            ChangedProperty = h.ChangedProperty.GetStringValue(),
        //            OldValue = h.OldValue,
        //            NewValue = h.NewValue
        //        }).ToList();

        //    return Ok(history);
        //}
    }
}
