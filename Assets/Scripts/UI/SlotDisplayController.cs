using System.Collections.Generic;
using System.Linq;
using GDD.Common;
using GDD.Connector;
using GDD.Player.Inventory;
using UnityEngine;

namespace GDD.UI
{
    public class SlotDisplayController : MonoBehaviour
    {
        [SerializeField] private SlotDisplay mainWeapon;
        [ReadOnly] [SerializeField] private List<SlotDisplay> slots;

        private void Awake()
        {
            ServiceLocator.Register<SlotDisplayController>(this);
        }

        private void Start()
        {
            slots = FindObjectsByType<SlotDisplay>(FindObjectsSortMode.None).ToList();
            slots.Remove(mainWeapon);
            slots.Sort((a, b) => a.num.CompareTo(b.num));
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<SlotDisplayController>();
        }

        public void UpdateDisplay(InventoryData data)
        {
            mainWeapon.ChangeData(data.weapon);

            var count = Mathf.Min(data.slots.Count, slots.Count);
            for (var i = 0; i < count; i++)
            {
                slots[i].ChangeData(data.slots[i]);
            }
        }
    }
}
