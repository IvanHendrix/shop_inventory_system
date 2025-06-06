using System;
using System.Collections.Generic;
using Infrastructure;
using TMPro;
using UI.Shop.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopView : BaseView<ShopViewData>
    {
        public event Action<ShopItemDataView> OnBuyClicked;

        [SerializeField] private Transform _itemContainer;
        [SerializeField] private ShopItemUI _itemUIPrefab;
        
        [Space]
        [SerializeField] private Button _navigatedButton;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private List<ShopItemUI> _items = new List<ShopItemUI>();

        protected override void OnContextUpdate(ShopViewData context)
        {
            base.OnContextUpdate(context);

            if (Data.ShopItemDataViews == null || Data.ShopItemDataViews.Count == 0)
            {
                return;
            }
            
            UpdateView(Data.ShopItemDataViews);
        }

        private void UpdateView(List<ShopItemDataView> views)
        {
            Clear();
            
            foreach (var viewData in views)
            {
                var item = Instantiate(_itemUIPrefab, _itemContainer);
                item.SetContext(viewData);
                item.OnClick += OnBuyItemClick;
                item.OnDescriptionSendClick += OnSetDescriptionText;
                
                _items.Add(item);
            }
        }

        public void Clear()
        {
            foreach (ShopItemUI item in _items)
            {
                item.OnClick -= OnBuyItemClick;
                item.OnDescriptionSendClick -= OnSetDescriptionText;
                Destroy(item.gameObject);
            }
            
            _items.Clear();

            _descriptionText.text = string.Empty;
        }

        public void UpdateGoldDisplay(int gold)
        {
            _goldText.text = $"Gold: ${gold}";
        }

        private void OnBuyItemClick(ShopItemDataView data)
        {
            OnBuyClicked?.Invoke(data);
        }

        private void OnSetDescriptionText(string description)
        {
            _descriptionText.text = description;
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void ShowPopupMessage()
        {
        }
    }
}