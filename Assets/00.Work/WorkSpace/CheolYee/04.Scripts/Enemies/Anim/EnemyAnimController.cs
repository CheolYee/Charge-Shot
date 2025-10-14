using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.Anim
{
    public class EnemyAnimController : MonoBehaviour
    {
        private Enemy _enemy;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void AnimationEnd()
        {
            _enemy.AnimationEndTrigger();
        }

        public void AttackCast()
        {
            _enemy.Attack();
        }
    }
}