using System;
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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"트리거 감지: {other.gameObject.name} / {other.gameObject.tag}");

            if (other.CompareTag("PlayerInteractableRange"))
            {
                float distance = Vector2.Distance(transform.position, other.transform.position);
                _inventory.SetInteractable(_itemInfo, distance, this.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("PlayerInteractableRange"))
            {
                _inventory.SetInteractable(_itemInfo, 0, this.gameObject,true);
            }
        }
    }
}