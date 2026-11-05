using GDD.Common;
using GDD.Connector;
using GDD.Player;
using UnityEngine;

namespace GDD.Item
{
    public class InteractableItem : MonoBehaviour
    {
        [SerializeField] private ItemStack _itemInfo;
        [ReadOnly] [SerializeField] private PlayerInventory _inventory;

        private void Start()
        {
            _inventory = ServiceLocator.Get<PlayerInventory>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var player = collision.gameObject;
            var distance = Vector3.Distance(player.transform.position, transform.position);
            
            _inventory.SetInteractable(_itemInfo, distance);
        }
    }
}