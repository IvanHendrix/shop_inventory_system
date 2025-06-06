using System.Collections.Generic;
using Infrastructure.Data;

namespace UI.Inventory.Data
{
    public class InventoryViewData : ViewData
    {
        public List<InventoryItemDataView> Items;

        public InventoryViewData(List<InventoryItemDataView> items)
        {
            Items = items;
        }
    }
}