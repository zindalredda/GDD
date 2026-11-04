using GDD.Common;
using GDD.Item;
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
        
        public void ChangeData(ItemStack itemStack)
        {
            image.sprite = itemStack.itemData.icon;
            image.preserveAspect = true;
            
            text.text = itemStack.amount.ToString();
        }
    }
}