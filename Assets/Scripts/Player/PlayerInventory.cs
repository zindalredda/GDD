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
        [ReadOnly] [SerializeField] private GameObject obj;

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
            SetInventory(_inventory);
        }

        public void SetInteractable(ItemStack item, float distance, GameObject _obj = null, bool reset = false)
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
                obj = _obj;
                var isDifferentItem = (_interactableItem != item);

                _interactableItem = item;
                _interactableItemDistance = distance;
                
                if (isDifferentItem || _interactableItem != null)
                {
                    _interactableDisplayController?.ChangeText(_interactableItem, obj, _inventory.maxInventory == _inventory.slots.Count);
                }
            }
        }

        public void AddItem(ItemStack item, bool isDropItem = false)
        {
            var _item = isDropItem ? _interactableItem : item;

            if (_item == null)
            {
                Debug.Log("상호작용할 수 있는 아이템이 없습니다.");
                return;
            }
            
            var currentInv = _inventory;
            var exist = currentInv.slots.FindIndex(slot => slot.itemData == _item.itemData);
            
            if (exist != -1)
            {
                currentInv.slots[exist].amount += _item.amount;
            }
            else
            {
                if (currentInv.slots.Count >= currentInv.maxInventory)
                {
                    Debug.LogWarning("인벤토리가 가득 차서 아이템을 주울 수 없습니다!");
                    // TODO: UI 안내 텍스트 출력
                    return;
                }

                currentInv.slots.Add(_item);
            }
            
            _interactableDisplayController.ChangeText(_interactableItem, null);
            SetInventory(currentInv);
            Destroy(obj);
        }
    }
}