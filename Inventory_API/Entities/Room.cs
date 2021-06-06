using System.ComponentModel;

namespace Inventory_API.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public Org Org { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)] public bool IsDeleted { get; set; }
    }
}