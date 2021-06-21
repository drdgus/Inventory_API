using Inventory_API.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Inventory_API.Entities;

namespace Inventory_API.Models
{
    public class Equip
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Name { get; set; }
        public int InvNum { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int AccountabilityId { get; set; }
        public Accountability Accountability { get; set; }
        public List<History> History { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
        [DefaultValue(false)] public bool IsWriteOff { get; set; }
        public int MOLId { get; set; }
        /// <summary>
        /// Материально ответственное лицо.
        /// </summary>
        public MOL MOL { get; set; }
        /// <summary>
        /// Дата выпуска оборудования
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Базовая цена
        /// </summary>
        public Decimal BasePrice { get; set; }
        /// <summary>
        /// Амортизационные начисления в процентах.
        /// </summary>
        public int DepreciationRate { get; set; }
        /// <summary>
        /// Группа амортизационных начислений
        /// </summary>
        public InvEnums.DepreciationGroups DepreciationGroup { get; set; }
        public string BaseInvNum { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
