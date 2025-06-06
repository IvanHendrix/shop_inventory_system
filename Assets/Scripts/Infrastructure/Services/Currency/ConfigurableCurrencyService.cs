using System;
using StaticData;

namespace Infrastructure.Services.Currency
{
    public interface ICurrencyService : IService
    {
        event Action<int> OnGoldChanged;
        event Action OnRefusedPurchase;
        void TrySpend(int amount);
        void AddGold(int amount);
        bool CanAfford(int amount);
    }

    public class CurrencyService : ICurrencyService
    {
        public event Action<int> OnGoldChanged;
        public event Action OnRefusedPurchase;
        
        private int _gold;

        public CurrencyService(CurrencyConfig config)
        {
            _gold = config.startingGold;
        }

        public void TrySpend(int amount)
        {
            if (!CanAfford(amount))
            {
                OnRefusedPurchase?.Invoke();
                return;
            }
            
            _gold -= amount;
            OnGoldChanged?.Invoke(_gold);
        }

        public void AddGold(int amount)
        {
            _gold += amount;
            OnGoldChanged?.Invoke(_gold);
        }

        public bool CanAfford(int amount)
        {
            return _gold >= amount;
        }
    }
}