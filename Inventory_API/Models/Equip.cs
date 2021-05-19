using System;
using System.Collections.Generic;
using System.ComponentModel;
using Inventory_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Models
{
    public class Equip
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Name { get; set; }
        public string InvNum { get; set; }
        public Org Org { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Type Type { get; set; }
        public Status Status { get; set; }
        public Accountability Accountability { get; set; }
        public List<History> History { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
        [DefaultValue(false)] public bool IsDeleted { get; set; }
        /// <summary>
        /// Материально ответственное лицо.
        /// </summary>
        public string MOL { get; set; }
        /// <summary>
        /// Дата выпуска оборудования
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Базовая цена
        /// </summary>
        public Decimal BasePrice { get; set; }
        /// <summary>
        /// В процентах
        /// </summary>
        public int DepreciationRate  { get; set; }
        public DepreciationGroups DepreciationGroup  { get; set; }

        public enum DepreciationGroups
        {
            [StringValue("Первая группа")] I,
            [StringValue("Вторая группа")] II,
            [StringValue("Третья группа")] III,
            [StringValue("Четвертая группа")] IV,
            [StringValue("Пятая группа")] V,
            [StringValue("Шестая группа")] VI,
            [StringValue("Седьмая группа")] VII,
            [StringValue("Восьмая группа")] VIII,
            [StringValue("Девятая группа")] IX,
            [StringValue("Десятая группа")] X,
        }
    }
}
