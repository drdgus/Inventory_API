using Inventory_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory_API.DAL
{
    public class DbInitializer
    {
        private InventoryDbContext _context;

        public DbInitializer(InventoryDbContext context)
        {
            _context = context;
            Init();
        }

        private void Init()
        {
            if (_context.Equips.Any()) return;
            _context.Equips.Add(new Equip
            {
                Name = "samsung scx-4100",
                InvNum = "T-0000001",
                Org = new Org
                {
                    Name = "МКОУ Таежнинская школа №20"
                },
                Room = new Room
                {
                    Name = "Каб. 101",
                    MOL = "Иванов Иван Иванович"
                },
                Type = new Models.Type()
                {
                    Name = "МФУ"
                },
                Status = new Status
                {
                    Name = "На балансе"
                },
                Accountability = new Accountability
                {
                    Name = "Основной баланс"
                },
                Note = "Ut aut doloremque nihil provident est et numquam. Quia sit earum eos voluptatem fugiat nulla earum est. Odit natus qui veritatis aut eaque consectetur voluptatem. Odit rerum qui",
                Count = 1,
                History = new List<History>
                {
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Created,
                        ChangedProperty = History.Property.None,
                        OldValue = "",
                        NewValue = "scx-4100"
                    },
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Room,
                        OldValue = "Каб. 101",
                        NewValue = "Каб. 999"
                    },
                    new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Type,
                        OldValue = "МФУ",
                        NewValue = "МФУУУУ"
                    },new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Status,
                        OldValue = "На балансе",
                        NewValue = "Списано"
                    }, new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Accountability,
                        OldValue = "Основной баланс",
                        NewValue = "з/б"
                    }, new()
                    {
                        itemId = 1,
                        Date = DateTime.Now,
                        Code = History.OperationCode.Edited,
                        ChangedProperty = History.Property.Note,
                        OldValue = "",
                        NewValue = "Ut aut doloremque nihil provident est et numquam. Quia sit earum eos voluptatem fugiat nulla earum est. Odit natus qui veritatis aut eaque consectetur voluptatem. Odit rerum qui"
                    },
                }
            });

            _context.Rooms.AddRange(new List<Room>
            {
                new()
                {
                    Name = "Каб. 102",
                    MOL = "Петров П. П."
                },new()
                {
                    Name = "Каб. 103",
                    MOL = "МОЛ ФИО3"
                },new()
                {
                    Name = "Каб. 201",
                    MOL = "МОЛ ФИО4"
                },new()
                {
                    Name = "Каб. 202",
                    MOL = "МОЛ ФИО5"
                },new()
                {
                    Name = "Каб. 301",
                    MOL = "МОЛ ФИО6"
                },
            });
            _context.SaveChanges();
        }
    }
}
