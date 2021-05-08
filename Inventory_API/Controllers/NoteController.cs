using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private InventoryDbContext _context;

        public NoteController(ILogger<NoteController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] int equipId, [FromForm] string note)
        {
            try
            {
                var equip = _context.Equips.SingleOrDefault(e => e.Id == equipId);

                if(equip is null) BadRequest($"Имущество с id = {equipId} не найдено.");

                await _context.History.AddAsync(new History
                {
                    itemId = equipId,
                    Date = DateTime.Now,
                    Code = History.OperationCode.Edited,
                    ChangedProperty = History.Property.Note,
                    OldValue = equip.Note,
                    NewValue = note
                }); 

                equip.Note = note;
                _context.Equips.Update(equip);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return Ok();
        }   
    }
}
