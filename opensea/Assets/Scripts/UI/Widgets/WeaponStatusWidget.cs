using Assets.Scripts.Weapons;
using Assets.Scripts.Weapons.SOs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponStatusWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_armamentNumber;
        [SerializeField] private Slider m_cooldownSlider;
        [SerializeField] private TextMeshProUGUI m_cooldownText;
        
        private WeaponStats m_stats;
        private Weapon m_associatedWeapon;

        public void SetWeaponRef(Weapon weapon)
        {
            m_associatedWeapon = weapon;
            m_stats = weapon.Stats;
        }
    }
}