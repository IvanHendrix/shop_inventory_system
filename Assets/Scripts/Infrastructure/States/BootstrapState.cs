using System.ComponentModel.Design;
using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Inventory;
using Infrastructure.Services.Shop;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string ConfigsCurrencyPath = "Configs/CurrencyConfig";

        private readonly GameStateMachine _stateMachine;

        private AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
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
            
            CurrencyConfig config = Resources.Load<CurrencyConfig>(ConfigsCurrencyPath);
            _services.RegisterSingle<ICurrencyService>(new CurrencyService(config));
            
            _services.RegisterSingle<IInventoryService>(new SimpleInventoryService());
        }
    }
}