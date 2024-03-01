using System.Collections.Generic;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Enemy;

namespace Assets.Scripts.Managers
{
    public class EnemyFleet : FleetManager
    {
        private List<KeyValuePair<Enemy, Ship>> m_discoveredEnemies;
        
        public override void Initialize()
        {
            base.Initialize();
            m_ships.ForEach(RegisterShipToFleet);
        }
    }
}