using Assets.Scripts.Weapons.SOs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponSlotWidget : MonoBehaviour
    {
        [SerializeField] public Button SelectionButton;
        [SerializeField] private Image m_weaponIcon; 
        [SerializeField] private bool IsSelected;

        public WeaponStats WeaponStats
        {
            get => m_stats;
            set
            {
                m_stats = value;
                UpdateWidget();
            }
        }
        private WeaponStats m_stats;
        
        private void UpdateWidget()
        {
            m_weaponIcon.sprite = m_stats.Icon;
        }
    }
}