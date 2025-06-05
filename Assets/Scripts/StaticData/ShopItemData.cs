using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item", order = 0)]
    public class ShopItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [TextArea] public string description;
        public int price;
    }
}