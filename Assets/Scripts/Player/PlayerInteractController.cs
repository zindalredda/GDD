using System;
using GDD.Common;
using GDD.Connector;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerInteractController : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private InteractableType type = InteractableType.Chat;
        
        private void Awake()
        {
            ServiceLocator.Register<PlayerInteractController>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerInteractController>();
        }

        public void Interact()
        {
            switch (type)
            {
                case InteractableType.Chat:
                    ServiceLocator.Get<PlayerInventory>().AddItem(null, true);
                    break;
                default:
                    return;
            }
        }
    }
}