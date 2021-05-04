using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models
{
    public class CheckInfo
    {
        public DateTime Date { get; set; }
        public List<CheckEquip> Equip { get; set; }

        public override string ToString()
        {
            return $"{Date}\n\t{Equip.ToString()}";
        }
    }

    public class CheckEquip
    {
        public int Id { get; set; }
        public int CountFact { get; set; }

        public override string ToString()
        {
            return $"{Id} - {CountFact}\n\t";
        }
    }
}
