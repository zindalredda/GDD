using UnityEngine;

namespace GDD.Item
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Pirate/Item Data")]
    public class ItemData : ScriptableObject
    {
        [Header("기본 정보")]
        public string itemID;
        public string itemName;
        [TextArea(2, 5)] 
        public string description;
        public Sprite icon;
        public ItemCategory category;

        [Header("경제 & 중량 수치")]
        public int baseValue;
        public int maxStackSize = 99;
        public float weight = 1.0f;

        [Header("상태 정보")]
        public bool isDroppable = true;
        public bool isTradable = true;
    }
}