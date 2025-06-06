using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "CurrencyConfig", menuName = "Game/Currency Config")]
    public class CurrencyConfig : ScriptableObject
    {
        public int startingGold;
    }
}