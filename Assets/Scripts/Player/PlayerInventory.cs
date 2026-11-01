using GDD.Connector;
using GDD.Item.Items;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public Weapon currentWeapon;

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