using System;
using GDD.Common;
using GDD.Item;
using GDD.Item.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDD.Player.Inventory
{
    public class SlotDisplay : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private Image image;
        [ReadOnly] [SerializeField] private TMP_Text text;

        private void Awake()
        {
            image = this.transform.GetChild(0).GetComponent<Image>();
            text = this.transform.GetChild(1).GetComponent<TMP_Text>();
        }

        private void Start()
        {
            image.sprite = null;
            text.text = "";
        }

        public void ChangeData(ItemStack itemStack)
        {
            image.sprite = itemStack.itemData.icon;
            image.preserveAspect = true;
            
            text.text = itemStack.amount.ToString();
        }

        public void ChangeData(Weapon weapon)
        {
            image.sprite = weapon.icon;
            image.preserveAspect = true;
            text.text = "";
        }
    }
}