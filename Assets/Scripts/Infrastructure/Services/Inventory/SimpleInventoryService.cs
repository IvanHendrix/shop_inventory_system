using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Inventory.Data;
using UI.Shop.Data;

namespace Infrastructure.Services.Inventory
{
    public interface IInventoryService : IService
    {
        event Action OnInventoryChanged;
        void Add(ShopItemDataView item);
        List<InventoryItemData> GetInventory();
    }

    public class SimpleInventoryService : IInventoryService
    {
        public event Action OnInventoryChanged;
        
        private readonly Dictionary<string, InventoryItemData> _items = new();

        public void Add(ShopItemDataView item)
        {
            if (_items.TryGetValue(item.ItemName, out var existing))
            {
                existing.quantity++;
            }
            else
            {
                _items[item.ItemName] = new InventoryItemData
                {
                    itemName = item.ItemName,
                    icon = item.Icon,
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