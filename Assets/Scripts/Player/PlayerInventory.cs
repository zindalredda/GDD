using System.Collections.Generic;
using GDD.Common;
using GDD.Connector;
using GDD.Item;
using GDD.Item.Items;
using GDD.Player.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private InventoryData _inventory;
        [SerializeField] private Weapon weapon;
        private (ItemStack, float) _interactableItem = (new ItemStack(null, 1), 0);

        public void Awake()
        {
            ServiceLocator.Register<PlayerInventory>(this);
        }

        public void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerInventory>();
        }

        public InventoryData GetInventory()
        {
            return _inventory;
        }

        public void SetInventory(InventoryData inventory)
        {
            _inventory = inventory;
            ServiceLocator.Get<SlotDisplayController>().UpdateDisplay(_inventory);
        }

        private void Start()
        {
            _inventory.weapon = new ItemStack(weapon, 1);
        }

        public void SetInteractable(ItemStack item, float distance, bool reset = false)
        {
            if (reset == true)
            {
                _interactableItem = (new ItemStack(null, 1), 1000);
                return;
            }

            if (_interactableItem.Item1 == null || distance < _interactableItem.Item2)
            {
                _interactableItem = (item, distance);
            }
        }
    }
}