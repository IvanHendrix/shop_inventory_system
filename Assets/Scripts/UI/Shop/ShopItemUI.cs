﻿using System;
using TMPro;
using UI.Shop.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopItemUI : ButtonView<ShopItemDataView>
    {
        public event Action<ShopItemDataView> OnClick;
        public event Action<string> OnDescriptionSendClick;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _descriptionButton;

        private void Start()
        {
            _buyButton.onClick.AddListener(OnButtonClick);
            _descriptionButton.onClick.AddListener(OnDescriptionSendButtonClick);
        }

        protected override void OnContextUpdate(ShopItemDataView data)
        {
            base.OnContextUpdate(data);

            UpdateButton();
        }

        private void UpdateButton()
        {
            _icon.sprite = Data.Icon;
            _nameText.text = Data.ItemName;
            _priceText.text = "$" + Data.Price;
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(Data);
        }
        
        private void OnDescriptionSendButtonClick()
        {
            OnDescriptionSendClick?.Invoke(Data.Description);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnButtonClick);
            _descriptionButton.onClick.RemoveListener(OnDescriptionSendButtonClick);
        }
    }
}