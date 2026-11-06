using System;
using GDD.Connector;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Register<PlayerAnimationController>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerAnimationController>();
        }

        public void ChangeAnimation(Vector3 transformEulerAngles, float linearVelocityMagnitude)
        {
            // TODO:속도에 따라 애니메이션 실행
            throw new NotImplementedException();
        }
    }
}
