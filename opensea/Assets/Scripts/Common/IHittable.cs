using System;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Ships.Common
{
    public interface IHittable
    {
        public Action<IHittable, Impact> OnHit { get; set; }
        public void Hit(Impact impact);
    }
}