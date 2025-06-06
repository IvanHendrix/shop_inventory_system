using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Inventory;
using Infrastructure.Services.Shop;
using UI.Inventory;
using UI.Shop;
using UI.Shop.Data;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly AllServices _services;
        private ShopPresenter _shopPresenter;
        private InventoryPresenter _inventoryPresenter;

        public GameLoopState(AllServices services)
        {
            _services = services;
        }

        public void Enter()
        {
            ViewManager.Instance.Initialize();

            _shopPresenter =
                new ShopPresenter(_services.Single<IShopItemService>(), _services.Single<ICurrencyService>());
            _shopPresenter.OnItemPurchased += HandleItemPurchased;
            _shopPresenter.OnSwitchToInventory += ShowInventory;
            _shopPresenter.Start();

            _inventoryPresenter = new InventoryPresenter(_services.Single<IInventoryService>(), _services.Single<ICurrencyService>());
            _inventoryPresenter.OnBackToShopRequested += ShowShop;
            _inventoryPresenter.Start();

            ShowShop();
        }
        
        private void HandleItemPurchased(ShopItemDataView item)
        {
            _services.Single<IInventoryService>().Add(item);
        }

        private void ShowShop()
        {
            _inventoryPresenter.SetVisibleView(false);
            _shopPresenter.SetVisibleView(true);
        }

        private void ShowInventory()
        {
            _shopPresenter.SetVisibleView(false);
            _inventoryPresenter.SetVisibleView(true);
        }

        public void Exit()
        {
            _shopPresenter.Finish();
            _shopPresenter.OnItemPurchased -= HandleItemPurchased;
            _shopPresenter.OnSwitchToInventory -= ShowInventory;
            _shopPresenter = null;

            _inventoryPresenter.Finish();
            _inventoryPresenter.OnBackToShopRequested -= ShowShop;
            _inventoryPresenter = null;
        }
    }
}