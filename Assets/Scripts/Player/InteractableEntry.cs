using System;
using GDD.Item;
using UnityEngine;

namespace GDD.Player
{
    /// <summary>
    /// 플레이어 주변의 상호작용 가능한 아이템 정보를 담는 구조체.
    /// ValueTuple 대신 사용하여 가독성과 Inspector 직렬화를 개선합니다.
    /// </summary>
    [Serializable]
    public struct InteractableEntry
    {
        public ItemStack item;
        public GameObject gameObject;

        public InteractableEntry(ItemStack item, GameObject gameObject)
        {
            this.item = item;
            this.gameObject = gameObject;
        }
    }
}
