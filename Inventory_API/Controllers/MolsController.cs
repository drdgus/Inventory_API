using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Inventory_API.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MolsController : ControllerBase
    {
        private readonly ILogger<MolsController> _logger;
        private InventoryDbContext _context;

        public MolsController(ILogger<MolsController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _context.MOLs.AsNoTracking().Select(i => new
            {
                i.Id,
                Name = i.ShortFullName
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MOL mol)
        {
            await _context.MOLs.AddAsync(mol);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(MOL mol)
        {
            var mo = _context.MOLs.Single(i => i.Id == mol.Id);
            mo = mol;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
