using UnityEngine;

namespace Assets.Scripts.Weapons.SOs
{
    [CreateAssetMenu(fileName = "NewWeaponStats", menuName = "SO/Weapon/Stats")]
    public class WeaponStats : ScriptableObject
    {
        [Header("Information")]
        public string Name;
        public WeaponType Type;
        public Sprite WeaponSprite;
        public Sprite Icon;
        [Space]
        [Header("Salvo")]
        public Ammo[] PossibleAmmo;
        public float BaseFirepower;
        public float Cooldown;
        public float SwitchCooldown;
        public int CannonCount;
        [Header("Param")] 
        public float turnSpeed;
        public bool CanLockOnEnemy = true;
        public float EffectiveRange;
        public float MaxRange;
        public float Accuracy = 1;
    }
}
