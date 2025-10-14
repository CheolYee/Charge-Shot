using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Turret
{
    [CreateAssetMenu(fileName = "Capacitor Data", menuName = "SO/CapacitorData", order = 0)]
    public class CapacitorData : ScriptableObject
    {
        public float maxDamage;
        public float minDamage;
        public float chargeCooldown;
        public float maxChargeTime;
        public int id;
        public int price;
        public Color color;
    }
}