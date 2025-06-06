using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Inventory;
using Infrastructure.Services.Inventory.Data;
using UI.Inventory.Data;

namespace UI.Inventory
{
    public class InventoryPresenter : BasePresenter<InventoryView>
    {
        protected override string ViewId => "InventoryView";

        private readonly IInventoryService _inventoryService;
        private readonly ICurrencyService _currencyService;
        
        public event Action OnBackToShopRequested;

        public InventoryPresenter(IInventoryService inventoryService, ICurrencyService currencyService)
        {
            _inventoryService = inventoryService;
            _currencyService = currencyService;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            View.OnBackToShopClicked += HandleBackClicked;
            
            _inventoryService.OnInventoryChanged += Refresh;
            
            _currencyService.OnGoldChanged += OnUpdateGold;
            
            Refresh();
        }

        private void Refresh()
        {
            List<InventoryItemData> items = _inventoryService.GetInventory();
            InventoryViewData viewData = new InventoryViewData(
                items.Select(i => new InventoryItemDataView
                {
                    ItemName = i.itemName,
                    Icon = i.icon,
                    Quantity = i.quantity
                }).ToList());

            View.SetContext(viewData);
        }
        
        private void OnUpdateGold(int gold)
        {
            View.UpdateGoldDisplay(gold);
        }
        
        private void HandleBackClicked()
        {
            OnBackToShopRequested?.Invoke();
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            
            View.OnBackToShopClicked -= HandleBackClicked;
            
            _inventoryService.OnInventoryChanged -= Refresh;  
            
            _currencyService.OnGoldChanged -= OnUpdateGold;
        }
    }
}