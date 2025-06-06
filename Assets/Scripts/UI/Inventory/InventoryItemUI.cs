using TMPro;
using UI.Inventory.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryItemUI : ButtonView<InventoryItemDataView>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _quantityText;

        protected override void OnContextUpdate(InventoryItemDataView data)
        {
            _icon.sprite = data.Icon;
            _nameText.text = data.ItemName;
            _quantityText.text = $"x{data.Quantity}";
        }
    }
}