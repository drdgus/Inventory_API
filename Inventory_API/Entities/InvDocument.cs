using Inventory_API.Models;
using Inventory_API.Tools;
using System;
using System.Collections.Generic;

namespace Inventory_API.Entities
{
    public class InvDocument
    {
        public int Id { get; set; }
        public List<Equip> Equip { get; set; }
        public InvEnums.DocumentType DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string URL { get; set; }
    }
}
