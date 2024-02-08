using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    [CreateAssetMenu(fileName = "newModuleInformation", menuName = "SO/Ship/ModuleInfo", order = 0)]
    public class ModuleInformation : ScriptableObject
    {
        public ModuleType Type;
        public float MaxHp;
        public bool CanBeDamaged = true;
    }
}