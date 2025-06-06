using System;
using System.Collections.Generic;
using TMPro;
using UI.Shop.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopView : BaseView<ShopViewData>
    {
        public event Action<ShopItemDataView> OnBuyClicked;
        public event Action OnSwitchToInventoryClicked;

        [SerializeField] private Transform _itemContainer;
        [SerializeField] private ShopItemUI _itemUIPrefab;
        
        [Space]
        [SerializeField] private Button _navigatedButton;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private List<ShopItemUI> _items = new List<ShopItemUI>();

        private void Start()
        {
            _navigatedButton.onClick.AddListener(OnGoToInventoryClick);
        }

        protected override void OnContextUpdate(ShopViewData context)
        {
            base.OnContextUpdate(context);

            if (Data.ShopItemDataViews == null || Data.ShopItemDataViews.Count == 0)
            {
                return;
            }
            
            UpdateView(Data.ShopItemDataViews);
        }

        public void ShowPopupMessage()
        {
            Debug.LogError("Not enough money");
        }

        public void UpdateGoldDisplay(int gold)
        {
            _goldText.text = $"Gold: ${gold}";
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

        private void OnGoToInventoryClick()
        {
            OnSwitchToInventoryClicked?.Invoke();
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
            
            _navigatedButton.onClick.RemoveListener(OnGoToInventoryClick);
        }
    }
}