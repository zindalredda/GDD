using UnityEngine;

namespace GDD.Item.Items
{
    [CreateAssetMenu(fileName = "Default_Weapon", menuName = "Pirate/Item Data/Weapon")]
    public class Weapon : ItemData
    {
        [Header("무기 스탯 설정")]
        [SerializeField] private float damage = 10f;
        [SerializeField] private float range = 1.5f;
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private WeaponType type;
        
        public float Damage 
        { 
            get => damage; 
            set => damage = value; 
        }

        public float Range 
        { 
            get => range; 
            set => range = value; 
        }

        public float Delay 
        { 
            get => delay; 
            set => delay = value; 
        }

        public WeaponType Type
        {
            get => type;
            set => type = value;
        }

        public bool Attack()
        {
            // TODO: 모션 연동 및 공격 판정
            Debug.Log($"{type} Attacked! Damage: {damage}, Range: {range}");
            return true;
        }
    }
}