using System;
using GDD.Common;
using GDD.Connector;
using UnityEngine;

namespace GDD.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("조정용 변수")]
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;

        [Header("디버그용 변수들")]
        [ReadOnly] [SerializeField] private Rigidbody2D rb;

        public Vector2 targetVector;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            ServiceLocator.Register<PlayerController>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<PlayerController>();
        }

        private Vector2 ReturnCalculatedAcceleration()
        {
            var direction = targetVector.normalized;
            var targetVel = direction * maxSpeed;
            
            var targetAccel = targetVector.magnitude > 0 ? acceleration : deceleration;

            return Vector2.Lerp(rb.linearVelocity, targetVel, targetAccel * Time.deltaTime);
        }

        public void FixedUpdate()
        {
            rb.linearVelocity = ReturnCalculatedAcceleration();
        }
    }
}
