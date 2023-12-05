namespace Assets.Scripts.Ships.Common
{
    public enum DamageState
    {
        Undamaged, //work like normal
        Damaged, //work less effiencently, can be repaired (module hp > 50)
        Disabled, //Doesnt work, but can be repaired (Need another name) (module hp = 0 && canbedestroyed = false)
        Destroyed //Doesnt work, cant be repaired
    }
}
