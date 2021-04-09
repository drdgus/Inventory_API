using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models
{
    public class Equip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InvNum { get; set; }
        public Org Org { get; set; }
        public Room Room { get; set; }
        public Type Type { get; set; }
        public Status Status { get; set; }
        public Accountability Accountability { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
    }
}
