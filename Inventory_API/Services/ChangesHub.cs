using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Inventory_API.Entities;
using Inventory_API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Inventory_API.Services
{
    public class ChangesHub : Hub
    {
        public ObservableCollection<UnappliedChange> Changes { get; set; }

        private readonly InventoryDbContext _context;
        public ChangesHub(InventoryDbContext context)
        {
            _context = context;

            Changes = new ObservableCollection<UnappliedChange>();
            Changes.CollectionChanged += ChangesOnCollectionChanged;
        }

        private void ChangesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            SendChanges(Changes);
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.AddAsync(new Room
            {
                OrgId = 1,
                Name = room.Name,
                IsDeleted = false
            });
            _context.SaveChanges();
        }

        public void AddMOL(MOL mol)
        {
            _context.MOLs.Add(mol);
            _context.SaveChanges();
        }

        public void AddEquip(Equip equip)
        {
            _context.Equips.Add(equip);
            _context.SaveChanges();
        }

        public void AddHistory(History history)
        {
            _context.History.Add(history);
            _context.SaveChanges();
        }

        public void AddType(Type type)
        {
            _context.Types.Add(type);
            _context.SaveChanges();
        }

        public void Relocate(int equipId, int newRoomId, int molId)
        {
            var equip = _context.Equips.Single(i => i.Id == equipId);
            equip.RoomId = newRoomId;
            equip.MOLId = molId;
        }

        public void RemoveType(int selectedItemId)
        {
            var type = _context.Types.Single(i => i.Id == selectedItemId);
            _context.Types.Remove(type);
            _context.SaveChanges();
        }

        public void RemoveMOL(int selectedItemId)
        {
            var mol = _context.MOLs.Single(i => i.Id == selectedItemId);
            _context.MOLs.Remove(mol);
            _context.SaveChanges();
        }

        public async void RemoveEquip(int id)
        { 
            var equip = _context.Equips.Single(i => i.Id == id);
            _context.Equips.Remove(equip);
            _context.SaveChanges();
        }

        public void RemoveRoom(int roomId)
        {
            var room = _context.Rooms.Single(i => i.Id == roomId);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public void UpdateType(Type selectedItem)
        {
            _context.Types.Update(selectedItem);
            _context.SaveChanges();
        }

        public void UpdateMOL(MOL selectedItem)
        {
            _context.MOLs.Update(selectedItem);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void UpdateEquip(Equip equip)
        {
            _context.Equips.Update(equip);
            _context.SaveChanges();
        }

        public async Task SendChanges(ObservableCollection<UnappliedChange> changes)
        {
            await this.Clients.All.SendAsync("ReceiveChanges", changes.ToList());
        }
    }
}
