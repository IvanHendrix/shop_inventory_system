using Infrastructure.Services;
using Infrastructure.Services.Shop;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {

        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            EnterLoadLevel();
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState>();
        }
        
        private void RegisterServices()
        {
            _services.RegisterSingle<IShopItemService>(new ShopItemService());
            _services.Single<IShopItemService>().Init();
        }
    }
}