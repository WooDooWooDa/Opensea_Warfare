using System;
using Assets.Scripts.Inputs;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Ships.SOs;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;
using UnityEngine;
using Assets.Scripts.Common;
using JetBrains.Annotations;

namespace Assets.Scripts.Ships
{
    public abstract class Ship: MonoBehaviour, ISelectable, IHittable, IDamageSource, IDetectable
    {
        [SerializeField] private ShipTeam m_team;
        [SerializeField] private ShipInformation m_information;
        [SerializeField] private ShipStats m_stats;
        [SerializeField] private List<Module> m_modules = new();
        public bool Alive => !m_isMarkedAsDestroyed; //Change variable name
        public ShipStats Stats => m_stats;
        public Action<IHittable, Impact> OnHit { get; set; }
        public Func<float, Vector3, bool> OnTryDetect { get; set; }
        public Action<IDetectable, float, Vector3> OnDetected { get; set; }
        public Action<IDetectable> OnConceal { get; set; }
        public ShipTeam Team => m_team;
        public Rigidbody2D Body { get; private set; }

        public Action<Ship> OnShipDestroyed;
        public bool IsSelected => m_isSelected;

        private FleetManager m_fleet;
        private bool m_isSelected;
        private bool m_isMarkedAsDestroyed;

        private void Start()
        {
            switch (Team)
            {
                case ShipTeam.Player:
                    m_fleet = Main.Instance.GetManager<PlayerFleet>();
                    m_fleet.RegisterShipToFleet(this);
                    break;
            }

            Body = GetComponent<Rigidbody2D>();
            RegisterModules();
        }

        private void Update()
        {
            foreach (var mod in m_modules) {
                mod.UpdateModule(Time.deltaTime);
            }
        }

        public void OnDeselect()
        {
            m_isSelected = false;
            m_modules.ForEach(m => m.ShipDeselect());
        }

        public void OnSelect()
        {
            m_isSelected = true;
            ((PlayerFleet)m_fleet).FocusOn(this);
            m_modules.ForEach(m => m.ShipSelect());
        }
        
        public void Hit(Impact impact)
        {
            //hit feedback, dmg total widget
            OnHit?.Invoke(this, impact); // => camera if ship selected, shake
            
            if (Team == impact.DamageSource.Team)
            {
                Debug.Log("Watch out for friendly fire.");
            }
            
            var hull = GetModuleOfType<Hull>();
            var dmgTaken = hull.DamageOnImpact(impact);
            foreach (var module in GetModulesOfType(hull.GetRelatedModuleToPart(((HullPart)impact.HullPartHit)).ToArray()))
            {
                module.DamageOnImpact(impact);
            }
        }
        
        public bool TryDetected(float dist, Vector2 dir)
        {
            var detected = OnTryDetect?.Invoke(dist, dir) ?? false;
            if (detected)
            {
                OnDetected?.Invoke(this, dist, dir);
            }

            return detected;
        }

        private void DestroyShip()
        {
            m_isMarkedAsDestroyed = true; //Do not destroy ship => Mark it as destroy for fleet manager
            //destroy animation & particule
            //remove ship > replace with sinking animation
            OnShipDestroyed?.Invoke(this);
        }
        
        public IEnumerable<Module> GetModulesOfType(ModuleType[] type)
        {
            return m_modules.Where(module => type.Contains(module.Type));
        }
        
        [CanBeNull]
        public T GetModuleOfType<T>() where T : Module
        {
            return m_modules.OfType<T>().FirstOrDefault();
        }

        private void RegisterModules()
        {
            //Attached
            var modulesAttached = GetComponentsInChildren<Module>();
            m_modules.AddRange(modulesAttached);
            
            //BindListeners and init
            foreach (var module in m_modules)
            {
                module.Initialize(this);
                module.OnStateChanged += (damageable, state) => Debug.Log("'Module " + damageable + " has change state to " + state);
                module.OnDamageTaken += (damageable, f) =>
                {
                    Debug.Log("Damage taken on " + damageable + " : " + f);
                };
            }

            var hull = GetModuleOfType<Hull>();
            if (hull != null)
            {
                hull.OnDestroyed += (_) => DestroyShip();
            }
            var ammoBay = GetModuleOfType<AmmunitionBay>();
            if (ammoBay != null)
            {
                ammoBay.OnDestroyed += (_) => DestroyShip();
            }
        }
    }
}
