using System.Collections.Generic;
using GDD.Common;
using GDD.Connector;
using GDD.Item;
using GDD.Item.Items;
using GDD.Player.Inventory;
using GDD.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private InventoryData _inventory;
        [SerializeField] private Weapon weapon;
        [ReadOnly] [SerializeField] private InteractableDisplayController _interactableDisplayController;

        [ReadOnly] [SerializeField] private ItemStack interactableItem;
        [ReadOnly] [SerializeField] private GameObject interactableItemObj;
        [ReadOnly] [SerializeField] private int itemsCount;
        [ReadOnly] [SerializeField] private List<(ItemStack, GameObject, float)> _interactableItems;

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
            interactableItem = null;
            _interactableItems = new List<(ItemStack, GameObject, float)>();
            _interactableDisplayController = ServiceLocator.Get<InteractableDisplayController>();
            SetInventory(_inventory);
        }

        private void FixedUpdate()
        {
            if (itemsCount == 0)
            {
                interactableItem = null;
                interactableItemObj = null;
                _interactableDisplayController.ChangeText(null, null);
                return;
            }

            for (int i = 0; i < _interactableItems.Count; i++)
            {
                var itemTuple = _interactableItems[i];
                
                var dist = Vector3.Distance(itemTuple.Item2.transform.position, transform.position);
                _interactableItems[i] = (itemTuple.Item1, itemTuple.Item2, dist);
            }
            
            _interactableItems.Sort((a,b) => a.Item3.CompareTo(b.Item3));

            interactableItem = _interactableItems[0].Item1;
            interactableItemObj = _interactableItems[0].Item2;
            
            _interactableDisplayController.ChangeText(interactableItem, interactableItemObj, _inventory.slots.Count >= _inventory.maxInventory);
        }

        public void AddToInteractable(ItemStack item, GameObject newObj, bool enter = true)
        {
            if (enter)
            {
                var alreadyExists = _interactableItems.Exists(x => x.Item2 == newObj);
                if (!alreadyExists)
                {
                    _interactableItems.Add((item, newObj, 0f));
                }
            }
            else
            {
                _interactableItems.RemoveAll(x => x.Item2 == newObj);
            }

            itemsCount = _interactableItems.Count;
        }

        public void AddItem(ItemStack item, bool isDropItem = false)
        {
            var _item = isDropItem ? interactableItem : item;

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
            
            _interactableItems.RemoveAll(x => x.Item2 == interactableItemObj);
            
            _interactableDisplayController.ChangeText(interactableItem, null);
            SetInventory(currentInv);
            Destroy(interactableItemObj);
        }
    }
}