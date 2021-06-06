using Inventory_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Inventory_API.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Type = Inventory_API.Models.Type;

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
            if (_context.Orgs.Any()) return;

            _context.Orgs.Add(new Org()
            {
                Name = "МКОУ Таежнинская школа №20"
            });

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
                Username = "PC",
                Password = new Password
                {
                    EncryptedPassword = BCrypt.Net.BCrypt.HashPassword("3cec0a")
                }
            });

            _context.Categories.AddRange(new List<Category>
            {
                new Category{ Name = "Компьютеры и периферийное оборудование", Class = "320.26.2"},
                new Category{ Name = "Оборудование для измерения, испытаний и навигации", Class = "330.26.51"},
                new Category{ Name = "Изделия текстильные готовые прочие", Class = "330.13.92.2"},
                new Category{ Name = "Бутылки стеклянные", Class = "330.23.13.11.110"},
                new Category{ Name = "Банки стеклянные", Class = "330.23.13.11.120"},
                new Category{ Name = "Тара прочая из стекла, кроме ампул", Class = "330.23.13.11.140"},
                new Category{ Name = "Посуда стеклянная для лабораторных, гигиенических или фармацевтических целей; ампулы из стекла", Class = "330.23.19.23"},
                new Category{ Name = "Изделия керамические лабораторного, химического или прочего технического назначения, кроме фарфоровых", Class = "330.23.44.12"},
                new Category{ Name = "Сейфы и контейнеры упрочненные металлические бронированные или армированные, специально предназначенные для хранения денег и документов", Class = "330.25.99.21.110"},
                new Category{ Name = "Часы всех видов", Class = "330.26.52"},
                new Category{ Name = "Приборы оптические и фотографическое оборудование", Class = "330.26.70"},
                new Category{ Name = "Носители информации магнитные и оптические", Class = "330.26.8"},
                new Category{ Name = "Мебель", Class = "330.31.01.12"},
                new Category{ Name = "Снаряды, инвентарь и оборудование для занятий физкультурой, гимнастикой и атлетикой, занятий в спортзалах, фитнес-центрах", Class = "330.32.30.14"},
                new Category{ Name = "Снаряды, инвентарь и оборудование прочие для занятий спортом или для игр на открытом воздухе; плавательные бассейны и бассейны для гребли", Class = "330.32.30.15"},
                new Category{ Name = "Мебель медицинская, включая хирургическую, стоматологическую или ветеринарную, и ее части", Class = "330.32.50.30.110"},
                new Category{ Name = "Метлы и щетки", Class = "	330.32.91.1"},
                new Category{ Name = "Приборы, аппаратура и модели, предназначенные для демонстрационных целей", Class = "330.32.99.53"}
            });
            _context.SaveChanges();

            _context.Types.AddRange(new List<Type>
            {
                new Type(){ Name = "Интерактивная доска", CategoryId = _context.Categories.Single(i => i.Name.Contains("Приборы, аппаратура и модели, предназначенные для демонстрационных целей")).Id},
                new Type(){ Name = "Колонки", CategoryId = _context.Categories.Single(i => i.Name.Contains("Компьютеры и периферийное оборудование")).Id},
                new Type(){ Name = "Монитор", CategoryId = _context.Categories.Single(i => i.Name.Contains("Компьютеры и периферийное оборудование")).Id},
                new Type(){ Name = "Компьютерная мышь", CategoryId = _context.Categories.Single(i => i.Name.Contains("Компьютеры и периферийное оборудование")).Id},
                new Type(){ Name = "Клавиатура", CategoryId = _context.Categories.Single(i => i.Name.Contains("Компьютеры и периферийное оборудование")).Id}
            });

            _context.Accountabilities.Add(new Accountability() {Name = "Основной баланс"});
            _context.Accountabilities.Add(new Accountability() {Name = "З/б"});

            _context.Statuses.Add(new Status() {Name = "На балансе"});
            _context.Statuses.Add(new Status() {Name = "Списано"});
            _context.Statuses.Add(new Status() {Name = "Ремонт"});

            _context.MolPositions.Add(new MOLPosition {Name = "Учитель"});
            _context.MolPositions.Add(new MOLPosition {Name = "Зав. АХЧ"});
            _context.MolPositions.Add(new MOLPosition {Name = "Директор"});
            _context.MolPositions.Add(new MOLPosition {Name = "Инженер-программист"});

            _context.Rooms.Add(new Room
            {
                OrgId = 1,
                Name = "Склад",
                IsDeleted = false
            });

            

            _context.SaveChanges();

            _context.MOLs.Add(new MOL
            {
                FullName = "Беляков Алексей Артёмович",
                PositionId = 2,
                PersonnelNumber = 54
            });

            _context.SaveChanges();
        }
    }
}
