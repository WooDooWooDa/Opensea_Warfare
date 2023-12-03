using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public enum ModuleState
    {
        Working, //work like normal
        Damaged, //work less effiencently, can be repaired (module hp > 50)
        HS, //Doesnt work, but can be repaired (Need another name) (module hp = 0 && canbedestroyed = false)
        Destroyed //Doesnt work, cant be repaired
    }
}