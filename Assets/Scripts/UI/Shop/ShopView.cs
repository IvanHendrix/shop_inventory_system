using System;
using System.Collections.Generic;
using TMPro;
using UI.Shop.Data;
using UnityEngine;

namespace UI.Shop
{
    public class ShopView : MonoBehaviour
    {
        public event Action<ShopItemDataView> OnBuyClicked;
        
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private ShopItemUI _itemUIPrefab;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private List<ShopItemUI> _items = new List<ShopItemUI>();

        public void UpdateView(List<ShopItemDataView> views)
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
        }

        public void UpdateGoldDisplay(int gold)
        {
            _goldText.text = $"Gold: ${gold}";
        }

        private void OnBuyItemClick(ShopItemDataView data)
        {
            
        }

        private void OnSetDescriptionText(string description)
        {
            _descriptionText.text = description;
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}