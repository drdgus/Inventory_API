using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Inventory_API.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private InventoryDbContext _context;

        public SettingsController(ILogger<SettingsController> logger, InventoryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string InvSymbol)
        {
            //if (!System.IO.File.Exists("settings.xml"))
            //{
            //    XDocument xdoc = new XDocument(new XElement("settings",
            //        new XElement("Password", ""),
            //        new XElement("LoggingOfSentPackets", "false")));
            //    xdoc.Save("settings.xml");
            //}


            //XDocument settings = XDocument.Load("settings.xml");
            //SendInterval = Convert.ToInt32(settings.Element("settings").Element("InvSymbol").Value);
            //settings.Save("settings.xml");

            return Ok();
        }
    }
}
