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
        [ReadOnly] [SerializeField] private GameObject obj;
        [ReadOnly] [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector3 offset;

        private void Awake()
        {
            ServiceLocator.Register<InteractableDisplayController>(this);
            text = this.transform.GetChild(0).GetComponent<TMP_Text>();
            _rectTransform = this.GetComponent<RectTransform>();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<InteractableDisplayController>();
        }

        private void Start()
        {
            text.text = "";
        }

        private void FixedUpdate()
        {
            if (obj == null)
                return;
            var pos = Camera.main.WorldToScreenPoint(obj.transform.position + offset);
            _rectTransform.position = pos;
        }

        public void ChangeText(ItemStack item, GameObject _obj, bool isFull = false)
        {
            Debug.Log($"{item} {_obj}");

            if (item == null || item.itemData == null)
            {
                text.text = "";
                obj = null;
                return;
            }

            obj = _obj;

            var itemName = item.itemData.itemName;
            var itemDescription = item.itemData.description;

            if (isFull)
            {
                text.text = $"<color=red>인벤토리 가득 참</color>\n{itemName}";
            }
            else
            {
                text.text = $"{itemName}\n{itemDescription}";
            }
        }
    }
}
