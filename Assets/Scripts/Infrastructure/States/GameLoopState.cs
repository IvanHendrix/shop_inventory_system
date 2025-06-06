using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Shop;
using UI.Shop;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly AllServices _services;
        private ShopPresenter _shopPresenter;

        public GameLoopState(AllServices services)
        {
            _services = services;
        }

        public void Enter()
        {
            ViewManager.Instance.Initialize();
            
            _shopPresenter = new ShopPresenter(_services.Single<IShopItemService>(),_services.Single<ICurrencyService>() );
            _shopPresenter.Start();
        }

        public void Exit()
        {
            _shopPresenter.Finish();
            _shopPresenter = null;
        }
    }
}