using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models
{
    public class UnappliedGODModel
    {
        public int Id { get; set; }
        public List<CheckEquip> CheckInfo { get; set; }
        public Equip Equip { get; set; }
    }
}
