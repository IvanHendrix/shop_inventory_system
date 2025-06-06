using System;
using System.Collections.Generic;
using TMPro;
using UI.Popup;
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
        [SerializeField] private PopupView _popupView;

        private List<ShopItemUI> _items = new List<ShopItemUI>();

        private void Start()
        {
            _navigatedButton.onClick.AddListener(OnGoToInventoryClick);
            
            _popupView.SetPopupText("Not enough money");
            _popupView.OnClick += OnPopupButtonClick;
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
            SetVisiblePopup(true);
        }

        public void UpdateGoldDisplay(int gold)
        {
            _goldText.text = $"Gold: ${gold}";
        }

        private void OnPopupButtonClick()
        {
            SetVisiblePopup(false);
        }

        private void SetVisiblePopup(bool visible)
        {
            _popupView.gameObject.SetActive(visible);
        }

        private void Clear()
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
            
            SetVisiblePopup(false);
            
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
            _descriptionText.text = string.Empty;
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