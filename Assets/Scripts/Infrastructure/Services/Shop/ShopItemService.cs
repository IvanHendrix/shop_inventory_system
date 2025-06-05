using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Shop
{
    public interface IShopItemService : IService
    {
        void Init();
        List<ShopItemData> GetAllItems();
    }

    public class ShopItemService : IShopItemService
    {
        private const string StaticData = "Static data";
        
        private List<ShopItemData> _datas = new List<ShopItemData>();

        public void Init()
        {
            ShopItemData[] itemDatas = Resources.LoadAll<ShopItemData>(StaticData);

            foreach (var item in itemDatas)
            {
                _datas.Add(item);
            }
        }

        public List<ShopItemData> GetAllItems()
        {
            return _datas;
        }
    }
}