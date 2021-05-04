﻿namespace Inventory_API.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Материально ответственное лицо.
        /// </summary>
        public string MOL { get; set; }
    }
}