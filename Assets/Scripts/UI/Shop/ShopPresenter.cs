using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Shop;
using StaticData;
using UI.Shop.Data;
using UnityEngine;

namespace UI.Shop
{
    public class ShopPresenter : BasePresenter<ShopView>
    {
        protected override string ViewId => "ShopView";
        
        public event Action<ShopItemDataView> OnItemPurchased;
        public event Action OnSwitchToInventory;

        private readonly IShopItemService _itemService;
        private readonly ICurrencyService _currencyService;

        public ShopPresenter(IShopItemService itemService, ICurrencyService currencyService)
        {
            _itemService = itemService;
            _currencyService = currencyService;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            View.OnBuyClicked += HandleBuyClicked;
            View.OnSwitchToInventoryClicked += HandleSwitchToInventory;

            _currencyService.OnGoldChanged += OnUpdateGold;

            LoadShopItems();
        }

        private void OnUpdateGold(int gold)
        {
            View.UpdateGoldDisplay(gold);
        }

        private void LoadShopItems()
        {
            List<ShopItemData> shopItems = _itemService.GetAllItems();

            List<ShopItemDataView> viewModels = shopItems
                .Select(i => new ShopItemDataView
                {
                    ItemName = i.itemName,
                    Icon = i.icon,
                    Description = i.description,
                    Price = i.price
                })
                .ToList();

            ShopViewData shopViewData = new ShopViewData(viewModels);

            View.SetContext(shopViewData);
            View.UpdateGoldDisplay(_currencyService.GetCurrentGold());
        }

        private void HandleBuyClicked(ShopItemDataView dataView)
        {
            bool success = _currencyService.CanAfford(dataView.Price);

            if (!success)
            {
                View.ShowPopupMessage();
                return;
            }
            
            _currencyService.TrySpend(dataView.Price);
            
            OnItemPurchased?.Invoke(dataView);
        }

        private void HandleSwitchToInventory()
        {
            OnSwitchToInventory?.Invoke();
        }

        protected override void OnFinish()
        {
            base.OnFinish();

            View.OnBuyClicked -= HandleBuyClicked;
            View.OnSwitchToInventoryClicked -= HandleSwitchToInventory;

            _currencyService.OnGoldChanged -= OnUpdateGold;
        }
    }
}