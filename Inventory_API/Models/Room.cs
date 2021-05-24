using System.ComponentModel;

namespace Inventory_API.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)] public bool IsDeleted { get; set; }
    }
}