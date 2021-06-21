using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Entities;
using Inventory_API.Models;
using Inventory_API.Services;
using Inventory_API.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckListController : ControllerBase
    {
        private readonly ILogger<CheckListController> _logger;
        private InventoryDbContext _context;

        public CheckListController(ILogger<CheckListController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int roomId)
        {
            var checkList = _context.Equips
                .Where(e => e.Room.Id == roomId)
                .Select(e => new
            {
                Id = e.Id,
                Name = e.Name,
                InvNum = e.InvNum.ToString("Т-0000000"),
                Type = e.Type.Name,
                CountAcc = e.Count,
                Mol = e.MOL.ShortFullName
            }).ToList();

            return Ok(checkList);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            //_unappliedChangeController.AddUnappliedChange(new UnappliedChange
            //{
            //    CreatedTime = DateTime.Now,
            //    TableCode = InvEnums.Table.CheckInfo,
            //    ChangedObject = new UnappliedGODModel{ CheckInfo = checkInfo.ToList() },
            //    OperationType = InvEnums.OperationCode.Created
            //});

            //using (var reader = new StreamReader(Request.Body))
            //{
            //    var body = await reader.ReadToEndAsync();
            //    var a = JsonSerializer.Deserialize<CheckEquip[]>(body);
            //}




            //var check = new List<CheckEquip>();

            //await _context.CheckEquips.AddRangeAsync(check);
            //await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
