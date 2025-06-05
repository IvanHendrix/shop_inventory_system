using UI.Shop;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private ShopPresenter _shopPresenter;
        
        public void Enter()
        {
            ViewManager.Instance.Initialize();
            
            _shopPresenter = new ShopPresenter();
            _shopPresenter.Start();
        }

        public void Exit()
        {
            _shopPresenter.Finish();
            _shopPresenter = null;
        }
    }
}