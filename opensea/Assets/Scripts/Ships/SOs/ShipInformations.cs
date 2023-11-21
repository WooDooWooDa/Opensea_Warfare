using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Ships.SOs
{
    [CreateAssetMenu(fileName = "NewShipInfo", menuName = "SO/Ship/Information")]
    public class ShipInformations: ScriptableObject
    {
        public string Name;
        public ShipType Type;
        public Sprite TypeIcon;
        public Sprite FrameSprite;
    }
}
