using GDD.Common;
using GDD.Connector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDD.Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private PlayerController playerController;

        private void Awake()
        {
            playerController = ServiceLocator.Get<PlayerController>();
        }
        
        public void OnMove(InputValue value)
        {
            playerController.targetVector = value.Get<Vector2>();
        }
    }
}