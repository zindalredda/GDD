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
    }
}