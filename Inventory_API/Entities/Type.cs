namespace Inventory_API.Models
{
    public class Type
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
    }
}