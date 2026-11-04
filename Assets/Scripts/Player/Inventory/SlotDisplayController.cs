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
            mainWeapon.ChangeData(data.weapon);
            
            slot1.ChangeData(data.slot1);
            slot2.ChangeData(data.slot2);
            slot3.ChangeData(data.slot3);
            slot4.ChangeData(data.slot4);
            slot5.ChangeData(data.slot5);
            slot6.ChangeData(data.slot6);
            slot7.ChangeData(data.slot7);
            slot8.ChangeData(data.slot8);
            slot9.ChangeData(data.slot9);
            slot10.ChangeData(data.slot10);
        }
    }
}