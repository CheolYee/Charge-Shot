using _00.Work.WorkSpace.CheolYee._04.Scripts.Agents;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.SO;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies
{
    public class Enemy : Agent
    {
        [Header("Enemy Settings")] 
        [SerializeField] private EnemyDataSo enemyData;
        public float Damage { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Damage = enemyData.attackDamage;
            HealthComponent.Initialize(this, enemyData.maxHealth);
        }
    }
}