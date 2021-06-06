using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly UnappliedChangeController _unappliedChangeController;

        public CheckListController(ILogger<CheckListController> logger, InventoryDbContext context, UnappliedChangeController unappliedChangeController)
        {
            _logger = logger;
            _context = context;
            _unappliedChangeController = unappliedChangeController;
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
        public async Task<IActionResult> Post([FromBody] CheckEquip[] checkInfo)
        {
            _unappliedChangeController.AddUnappliedChange(new UnappliedChange
            {
                CreatedTime = DateTime.Now,
                TableCode = InvEnums.Table.CheckInfo,
                ChangedObject = new UnappliedGODModel{ CheckInfo = checkInfo.ToList() },
                OperationType = InvEnums.OperationCode.Created
            });

            return Ok();
        }
    }
}
