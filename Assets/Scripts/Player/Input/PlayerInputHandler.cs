using GDD.Common;
using GDD.Connector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDD.Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private PlayerController playerController;

        private void Start()
        {
            playerController = ServiceLocator.Get<PlayerController>();
        }
        
        public void OnMove(InputValue value)
        {
            playerController.targetVector = value.Get<Vector2>();
        }

        public void OnAttack(InputValue value, float delay)
        {
            playerController.Attack();
        }
    }
}