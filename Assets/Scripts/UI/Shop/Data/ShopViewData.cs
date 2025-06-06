using System.Collections.Generic;
using Infrastructure.Data;

namespace UI.Shop.Data
{
    public class ShopViewData : ViewData
    {
        public List<ShopItemDataView> ShopItemDataViews;

        public ShopViewData(List<ShopItemDataView> shopItemDataViews)
        {
            ShopItemDataViews = shopItemDataViews;
        }
    }
}