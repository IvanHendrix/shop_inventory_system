using System;
using System.Collections.Generic;
using UI.Inventory.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryView : BaseView<InventoryViewData>
    {
        public event Action OnBackToShopClicked;
        
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private InventoryItemUI _itemUIPrefab;
        [SerializeField] private Button _backButton;
        
        private  List<InventoryItemUI> _activeItems = new();
        private  Stack<InventoryItemUI> _pooledItems = new();

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        protected override void OnContextUpdate(InventoryViewData context)
        {
            base.OnContextUpdate(context);
            UpdateView(Data.Items);
        }

        private void UpdateView(List<InventoryItemDataView> items)
        {
            foreach (InventoryItemUI item in _activeItems)
            {
                item.gameObject.SetActive(false);
                _pooledItems.Push(item);
            }
            _activeItems.Clear();
            
            foreach (InventoryItemDataView data in items)
            {
                InventoryItemUI itemUI = _pooledItems.Count > 0
                    ? _pooledItems.Pop()
                    : Instantiate(_itemUIPrefab, _itemContainer);

                itemUI.gameObject.SetActive(true);
                itemUI.SetContext(data);
                _activeItems.Add(itemUI);
            }
        }

        private void OnBackButtonClick()
        {
            OnBackToShopClicked?.Invoke();
        }
        
        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            foreach (var item in _activeItems)
            {
                Destroy(item.gameObject); 
            }

            foreach (var item in _pooledItems)
            {
                Destroy(item.gameObject);
            }
        }
    }
}