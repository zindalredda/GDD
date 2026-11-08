using System;
using GDD.Common;
using GDD.Connector;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private Animator _animator;
        [SerializeField] private float speed;
        
        private void Awake()
        {
            ServiceLocator.Register<PlayerAnimationController>(this);
            _animator = this.GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerAnimationController>();
        }

        private void Update()
        {
            _animator.SetFloat("Down Move", speed);
        }

        public void ChangeAnimation(Vector3 transformEulerAngles, float linearVelocityMagnitude)
        {
            // TODO:속도에 따라 애니메이션 실행
            throw new NotImplementedException();
        }
    }
}
