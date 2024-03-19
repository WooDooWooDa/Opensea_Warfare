using Assets.Scripts.Ships;

namespace Assets.Scripts.Common
{
    public interface IDamageSource
    {
        public ShipTeam Team { get; }
    }
}
