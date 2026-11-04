using GDD.Common;
using GDD.Connector;
using GDD.Item.Items;
using GDD.Player.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public Weapon currentWeapon;
        private InventoryData _inventory;

        public void Awake()
        {
            ServiceLocator.Register<PlayerInventory>(this);
        }

        public void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerInventory>();
        }
    }
}