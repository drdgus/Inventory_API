﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models
{
    public class CheckInfo
    {
        public DateTime Date { get; set; }
        public List<CheckEquip> Equip { get; set; }
    }
}
