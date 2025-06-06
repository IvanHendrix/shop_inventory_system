using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Inventory.Data;
using StaticData;

namespace Infrastructure.Services.Inventory
{
    public interface IInventoryService : IService
    {
        event Action OnInventoryChanged;
        void Add(ShopItemData item);
        List<InventoryItemData> GetInventory();
    }

    public class SimpleInventoryService : IInventoryService
    {
        public event Action OnInventoryChanged;
        
        private readonly Dictionary<string, InventoryItemData> _items = new();

        public void Add(ShopItemData item)
        {
            if (_items.TryGetValue(item.itemName, out var existing))
            {
                existing.quantity++;
            }
            else
            {
                _items[item.itemName] = new InventoryItemData
                {
                    itemName = item.itemName,
                    icon = item.icon,
                    quantity = 1
                };
            }

            OnInventoryChanged?.Invoke();
        }

        public List<InventoryItemData> GetInventory()
        {
            return _items.Values.ToList();
        }
    }
}