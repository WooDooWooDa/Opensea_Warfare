using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponSlotWidget : MonoBehaviour
    {
        [SerializeField] public WeaponType WeaponType;
        [SerializeField] public GameObject Separator;
        [SerializeField] public Button SelectionButton; 
        [SerializeField] public Transform m_weaponStatusParent;
        public bool IsActive;

        [SerializeField] private Transform m_closeSlot;
        [SerializeField] private Transform m_openSlot;
        [SerializeField] private Image m_weaponIcon;
        

        public void SetActive(WeaponType selectedWeaponType)
        {
            IsActive = WeaponType == selectedWeaponType;
            //BUG-P0 fix this status not showing up for main
            m_weaponStatusParent.gameObject.SetActive(IsActive); //add anim up/down
            //open and close the weapon action slot
            m_openSlot.gameObject.SetActive(IsActive);
            m_closeSlot.gameObject.SetActive(!IsActive);
        }
    }
}