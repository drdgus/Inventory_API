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
        public int OrgId { get; set; }
        public int RoomId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int AccountabilityId { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
    }
}
