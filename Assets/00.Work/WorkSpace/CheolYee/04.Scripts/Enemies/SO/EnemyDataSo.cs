using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.SO
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "SO/Enemy/EnemyData")]
    public class EnemyDataSo : ScriptableObject
    {
        [Header("Health Settings")]
        public float maxHealth;
        
        [Header("Movement Settings")]
        public float moveSpeed;
        public float jumpForce;
        public float knockbackDuration;

        [Header("Combat Settings")] 
        public float attackDamage;
        public float attackSpeed;
    }
}