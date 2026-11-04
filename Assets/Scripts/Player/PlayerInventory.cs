using GDD.Connector;
using GDD.Item.Items;
using GDD.Player.Inventory;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public Weapon currentWeapon;
        public InventoryData inventory;

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