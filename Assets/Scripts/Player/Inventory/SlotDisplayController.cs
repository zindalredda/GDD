using System;
using GDD.Connector;
using GDD.Item;
using UnityEngine;

namespace GDD.Player.Inventory
{
    public class SlotDisplayController : MonoBehaviour
    {
        [SerializeField] private SlotDisplay mainWeapon;
        [SerializeField] private SlotDisplay slot1;
        [SerializeField] private SlotDisplay slot2;
        [SerializeField] private SlotDisplay slot3;
        [SerializeField] private SlotDisplay slot4;
        [SerializeField] private SlotDisplay slot5;
        [SerializeField] private SlotDisplay slot6;
        [SerializeField] private SlotDisplay slot7;
        [SerializeField] private SlotDisplay slot8;
        [SerializeField] private SlotDisplay slot9;
        [SerializeField] private SlotDisplay slot10;

        private void Awake()
        {
            ServiceLocator.Register<SlotDisplayController>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<SlotDisplayController>();
        }

        public void UpdateDisplay(InventoryData data)
        {
            
        }
    }
}