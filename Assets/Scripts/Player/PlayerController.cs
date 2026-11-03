using System;
using System.Collections.Generic;
using GDD.Common;
using GDD.Connector;
using GDD.Item.Items;
using UnityEngine;
using UnityEngine.UIElements;

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
        [ReadOnly] [SerializeField] private float previousRotation = 0;
        [ReadOnly] [SerializeField] private List<float> previousVelocity = new() { 0f, 0f };
        [ReadOnly] [SerializeField] private float weaponDelay;
        [ReadOnly] [SerializeField] private float lastAttackedTime;
        
        
        
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
            
            // 추가 보정 (키 입력 없을 때, 가속도가 완전히 0이 되지 않음)
            if (direction.magnitude <= 0.01f && rb.linearVelocity.magnitude <= 0.5f)
            {
                targetAccel = 0;
                rb.linearVelocity = Vector2.zero;
            }

            return Vector2.Lerp(rb.linearVelocity, targetVel, targetAccel * Time.fixedDeltaTime);
        }

        private Vector3 ReturnCalculatedRotation()
        {
            var direction = targetVector.normalized;

            if (direction.magnitude <= 0.1f)
                return new Vector3(0, 0, previousRotation);
            
            var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;
            
            previousRotation = Mathf.Round(angle / 45f) * 45f % 360f;
            
            return new Vector3(0, 0, previousRotation);
        }

        public void Attack()
        {
            var currentWeapon = ServiceLocator.Get<PlayerInventory>().currentWeapon;
            if (currentWeapon == null)
            {
                Debug.Log("No Weapon in Inventory");
                return;
            }

            if (!IsWeaponAttackable(currentWeapon))
            {
                Debug.Log($"{currentWeapon.name} is on Delay");
                return;
            }
            currentWeapon.Attack();
            lastAttackedTime = Time.time;
        }

        private bool IsWeaponAttackable(Weapon weapon)
        {
            weaponDelay = weapon.Delay;
            return Time.time >= lastAttackedTime + weaponDelay;
        }

        public void FixedUpdate()
        {
            rb.linearVelocity = ReturnCalculatedAcceleration();
            this.transform.eulerAngles = ReturnCalculatedRotation();
        }

        // 로그용
        private void DebugMessage()
        {
            var targetAcceleration = ReturnCalculatedAcceleration();
            var currentAcceleration = ((rb.linearVelocityX - previousVelocity[0]) / Time.fixedDeltaTime,
                (rb.linearVelocityY - previousVelocity[1]) / Time.fixedDeltaTime);
            
            previousVelocity = new List<float> { rb.linearVelocityX, rb.linearVelocityY };
            
            Debug.Log($"VelX : {previousVelocity[0]} VelY : {previousVelocity[1]} TargetAccel : {targetAcceleration} CurrentAccel : {currentAcceleration}");
        }
    }
}














