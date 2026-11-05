using System;
using GDD.Common;
using GDD.Connector;
using GDD.Item;
using TMPro;
using UnityEngine;

namespace GDD.UI
{
    public class InteractableDisplayController : MonoBehaviour
    {
        [Header("절때 직접 지정 금지")]
        [ReadOnly] [SerializeField] private TMP_Text text;

        private void Awake()
        {
            ServiceLocator.Register<InteractableDisplayController>(this);
            text = this.transform.GetChild(0).GetComponent<TMP_Text>();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<InteractableDisplayController>();
        }

        private void Start()
        {
            text.text = "";
        }

        // TODO: 코드 더럽다....
        public void ChangeText(ItemStack item)
        {
            if (item == null)
                text.text = "";
            else
            {
                var itemName = item.itemData.itemName;
                var itemDescription = item.itemData.description;
                var _text = $"{itemName}\n{itemDescription}";

                text.text = _text;
            }
        }
    }
}