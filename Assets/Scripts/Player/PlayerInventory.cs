using System.Collections.Generic;
using GDD.Common;
using GDD.Connector;
using GDD.Item;
using GDD.Item.Items;
using GDD.Player.Inventory;
using GDD.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private InventoryData _inventory;
        [SerializeField] private Weapon weapon;
        [ReadOnly] [SerializeField] private ItemStack _interactableItem;
        [ReadOnly] [SerializeField] private float _interactableItemDistance;
        [ReadOnly] [SerializeField] private InteractableDisplayController _interactableDisplayController;

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
            _interactableItem = null;
            _interactableItemDistance = float.MaxValue;
            _interactableDisplayController = ServiceLocator.Get<InteractableDisplayController>();
        }

        public void SetInteractable(ItemStack item, float distance, GameObject obj = null, bool reset = false)
        {
            Debug.Log($"{item} {distance} {reset}\n{_interactableItem} {_interactableItemDistance}");
            if (reset)
            {
                _interactableItem = null;
                _interactableItemDistance = float.MaxValue;
                _interactableDisplayController.ChangeText(_interactableItem, obj);
                return;
            }

            if (_interactableItem == null || distance < _interactableItemDistance || _interactableItem == item)
            {
                var isDifferentItem = (_interactableItem != item);

                _interactableItem = item;
                _interactableItemDistance = distance;
                
                if (isDifferentItem || _interactableItem != null)
                {
                    _interactableDisplayController?.ChangeText(_interactableItem, obj, _inventory.maxInventory == _inventory.slots.Count);
                }
            }
        }

        public void AddItem(ItemStack item, bool isInteractable = false)
        {
            if (_inventory.maxInventory == _inventory.slots.Count)
            {
                //TODO: 꽉 차 있다는 메세지 보내기
                Debug.Log("Inventory is Full!!");
                return;
            }
            
            
        }
    }
}