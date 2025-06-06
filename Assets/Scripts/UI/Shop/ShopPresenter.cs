using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Shop;
using StaticData;
using UI.Shop.Data;

namespace UI.Shop
{
    public class ShopPresenter : BasePresenter<ShopView>
    {
        protected override string ViewId => "ShopView";
        
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
            
            _currencyService.OnGoldChanged += OnUpdateGold;
            _currencyService.OnRefusedPurchase += OnShowPopupMessage;
            
            LoadShopItems();
        }

        private void OnShowPopupMessage()
        {
            View.ShowPopupMessage();
        }

        private void OnUpdateGold(int obj)
        {
            View.UpdateGoldDisplay(_currencyService.GetCurrentGold());
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
            _currencyService.TrySpend(dataView.Price);
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            
            View.OnBuyClicked -= HandleBuyClicked;
            
            _currencyService.OnGoldChanged -= OnUpdateGold;
            _currencyService.OnRefusedPurchase -= OnShowPopupMessage;
        }
    }
}