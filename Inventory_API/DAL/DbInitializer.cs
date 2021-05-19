﻿using Inventory_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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

            _context.Users.Add(new User
            {
                Username = "Android",
                Password = new Password
                {
                    EncryptedPassword = BCrypt.Net.BCrypt.HashPassword("Android")
                }
            });
            _context.Users.Add(new User
            {
                Username = "Андроид",
                Password = new Password
                {
                    EncryptedPassword = BCrypt.Net.BCrypt.HashPassword("123123")
                }
            });

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
                Note = "",
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
                        NewValue = "Новая заметка"
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
            _context.Equips.AddRange(new Equip[]
            {
                new Equip
                {
                    RegistrationDate = DateTime.Now.AddDays(-10),
                    Name = "Lenovo idea pad 330",
                    InvNum = "T-0000001",
                    Org = _context.Orgs.Local.First(),
                    Room = _context.Rooms.Local.Skip(1).First(),
                    Type = new Models.Type
                    {
                        Name = "Ноутбук"
                    },
                    Status = _context.Statuses.Local.First(),
                    Accountability = _context.Accountabilities.Local.First(),
                    History = new List<History>
                    {
                        new History
                        {
                            itemId = 2,
                            Date = DateTime.Now.AddDays(-10),
                            Code = History.OperationCode.Created,
                            ChangedProperty = History.Property.None,
                            OldValue = "",
                            NewValue = ""
                        }
                    },
                    Note = "",
                    Count = 1,
                    IsDeleted = false
                },
                new Equip
                {
                    RegistrationDate = DateTime.Now.AddDays(-8),
                    Name = "Стул",
                    InvNum = "T-0000001",
                    Org = _context.Orgs.Local.First(),
                    Room = _context.Rooms.Local.Skip(1).First(),
                    Type = new Models.Type
                    {
                        Name = "Мебель"
                    },
                    Status = _context.Statuses.Local.First(),
                    Accountability = _context.Accountabilities.Local.First(),
                    History = new List<History>
                    {
                        new History
                        {
                            itemId = 3,
                            Date = DateTime.Now.AddDays(-8),
                            Code = History.OperationCode.Created,
                            ChangedProperty = History.Property.None,
                            OldValue = "",
                            NewValue = ""
                        }
                    },
                    Note = "",
                    Count = 20,
                    IsDeleted = false
                },
                new Equip
                {
                    RegistrationDate = DateTime.Now.AddDays(-8),
                    Name = "Стол",
                    InvNum = "T-0000001",
                    Org = _context.Orgs.Local.First(),
                    Room = _context.Rooms.Local.Skip(1).First(),
                    Type = new Models.Type
                    {
                        Name = "Мебель"
                    },
                    Status = _context.Statuses.Local.First(),
                    Accountability = _context.Accountabilities.Local.First(),
                    History = new List<History>
                    {
                        new History
                        {
                            itemId = 4,
                            Date = DateTime.Now.AddDays(-8),
                            Code = History.OperationCode.Created,
                            ChangedProperty = History.Property.None,
                            OldValue = "",
                            NewValue = ""
                        }
                    },
                    Note = "",
                    Count = 10,
                    IsDeleted = false
                }
            });

            _context.SaveChanges();
        }
    }
}