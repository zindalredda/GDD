using System.Collections.Generic;
using GDD.Common;
using GDD.Connector;
using GDD.Item;
using GDD.Item.Items;
using GDD.Player.Inventory;
using GDD.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("설정")]
        [SerializeField] private Weapon weapon;

        [Header("디버그 (ReadOnly)")]
        [ReadOnly] [SerializeField] private InventoryData _inventory;
        [ReadOnly] [SerializeField] private InteractableDisplayController _interactableDisplayController;
        [ReadOnly] [SerializeField] private ItemStack interactableItem;
        [ReadOnly] [SerializeField] private GameObject interactableItemObj;
        [ReadOnly] [SerializeField] private List<InteractableEntry> _interactableItems;

        public bool IsInventoryFull => _inventory.slots.Count >= _inventory.maxInventory;

        #region Lifecycle

        public void Awake()
        {
            ServiceLocator.Register<PlayerInventory>(this);
        }

        public void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerInventory>();
        }

        private void Start()
        {
            _inventory.weapon = new ItemStack(weapon, 1);
            interactableItem = null;
            _interactableItems = new List<InteractableEntry>();
            _interactableDisplayController = ServiceLocator.Get<InteractableDisplayController>();
            SetInventory(_inventory);
        }

        private void FixedUpdate()
        {
            UpdateNearestInteractable();
        }

        #endregion

        #region Inventory

        public InventoryData GetInventory() => _inventory;

        public void SetInventory(InventoryData inventory)
        {
            _inventory = inventory;
            ServiceLocator.Get<SlotDisplayController>().UpdateDisplay(_inventory);
        }

        public void AddItem(ItemStack item, bool isDropItem = false)
        {
            var targetItem = isDropItem ? interactableItem : item;

            if (targetItem == null)
            {
                Debug.Log("상호작용할 수 있는 아이템이 없습니다.");
                return;
            }

            if (!TryAddToInventory(targetItem))
                return;

            RemoveInteractable(interactableItemObj);
            UpdateInteractableUI();
            SetInventory(_inventory);
            Destroy(interactableItemObj);
        }

        /// <summary>
        /// 인벤토리에 아이템 추가를 시도합니다.
        /// 이미 존재하는 아이템이면 수량을 합산하고, 새 아이템이면 슬롯에 추가합니다.
        /// </summary>
        /// <returns>추가 성공 여부</returns>
        private bool TryAddToInventory(ItemStack item)
        {
            var existIndex = _inventory.slots.FindIndex(slot => slot.itemData == item.itemData);

            if (existIndex != -1)
            {
                _inventory.slots[existIndex].amount += item.amount;
                return true;
            }

            if (IsInventoryFull)
            {
                Debug.LogWarning("인벤토리가 가득 차서 아이템을 주울 수 없습니다!");
                // TODO: UI 안내 텍스트 출력
                return false;
            }

            _inventory.slots.Add(item);
            return true;
        }

        #endregion

        #region Interactable Tracking

        public void AddToInteractable(ItemStack item, GameObject newObj, bool enter = true)
        {
            if (enter)
            {
                if (!_interactableItems.Exists(x => x.gameObject == newObj))
                {
                    _interactableItems.Add(new InteractableEntry(item, newObj));
                }
            }
            else
            {
                RemoveInteractable(newObj);
            }
        }

        private void RemoveInteractable(GameObject obj)
        {
            _interactableItems.RemoveAll(x => x.gameObject == obj);
        }

        /// <summary>
        /// 가장 가까운 상호작용 가능 아이템을 찾아 UI를 갱신합니다.
        /// </summary>
        private void UpdateNearestInteractable()
        {
            if (_interactableItems == null || _interactableItems.Count == 0)
            {
                ClearInteractable();
                return;
            }

            FindNearestInteractable(out var nearestItem, out var nearestObj);
            interactableItem = nearestItem;
            interactableItemObj = nearestObj;

            _interactableDisplayController.ChangeText(interactableItem, interactableItemObj, IsInventoryFull);
        }

        /// <summary>
        /// 리스트를 한 번 순회하여 플레이어와 가장 가까운 아이템을 찾습니다.
        /// </summary>
        private void FindNearestInteractable(out ItemStack nearestItem, out GameObject nearestObj)
        {
            float nearestDist = float.MaxValue;
            nearestItem = null;
            nearestObj = null;

            foreach (var entry in _interactableItems)
            {
                if (entry.gameObject == null) continue;

                float dist = Vector3.Distance(entry.gameObject.transform.position, transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestItem = entry.item;
                    nearestObj = entry.gameObject;
                }
            }
        }

        private void ClearInteractable()
        {
            interactableItem = null;
            interactableItemObj = null;
            _interactableDisplayController.ChangeText(null, null);
        }

        /// <summary>
        /// 아이템 획득 후 인터랙션 UI를 갱신합니다.
        /// </summary>
        private void UpdateInteractableUI()
        {
            if (interactableItem?.itemData != null)
            {
                _interactableDisplayController.ChangeText(interactableItem, null);
            }
            else
            {
                _interactableDisplayController.ChangeText(null, null);
            }
        }

        #endregion
    }
}
