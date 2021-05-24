using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
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
            var checkList = _context.Equips.Where(e => e.Room.Id == roomId).Select(e => new
            {
                Id = e.Id,
                Name = e.Name,
                InvNum = e.InvNum.ToString("Т-0000000"),
                Type = e.Type.Name,
                CountAcc = e.Count,
                Mol = e.MOL
            }).ToList();

            return Ok(checkList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CheckEquip[] checkInfo)
        {
            return Ok("NotImplementedFunction");
        }
    }
}
