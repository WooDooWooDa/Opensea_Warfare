using UnityEngine;

namespace Assets.Scripts.Ships.SOs
{
    [CreateAssetMenu(fileName = "NewShipInfo", menuName = "SO/Ship/Information")]
    public class ShipInformation: ScriptableObject
    {
        public string Name;
        public ShipType Type;
        public Sprite TypeIcon;
        public Sprite FrameSprite;
    }
}
