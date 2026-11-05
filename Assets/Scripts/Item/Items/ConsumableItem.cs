using UnityEngine;

namespace GDD.Item.Items
{
    [CreateAssetMenu(fileName = "ConsumableItem", menuName = "Pirate/Item Data/ConsumableItem")]
    public class ConsumableItem : ItemData
    {
        [Header("소비 스텟 설정")]
        [SerializeField] private ConsumableType type;

        public ConsumableType Type
        {
            get => type;
            set => type = value;
        }
        
        public bool Use()
        {
            return true;
        }
    }
}