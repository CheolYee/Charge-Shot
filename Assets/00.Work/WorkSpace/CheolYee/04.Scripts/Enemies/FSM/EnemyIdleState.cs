using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.FSM
{
    public class EnemyIdleState : State
    {
        
        //아이들 상태
        
        private Coroutine _delayCoroutine = null;
        
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            Vector3 dir =  Enemy.TargetTransform.position - Enemy.transform.position; //방향 설정
            float distance = dir.magnitude; //거리 가져와서
            //공격 사거리보가 짧고, 쿨타임이 지났으면
            if (Enemy.MovementComponent.CanMove && distance > Enemy.attackRadius)
                StateMachine.ChangeState(EnemyBehaviourType.Chase);
            
            if (distance < Enemy.attackRadius && Enemy.lastAttackTime + Enemy.AttackSpeed < Time.time)
            {
                StateMachine.ChangeState(EnemyBehaviourType.Attack); //공격으로 설정  
            }
            
        }

        public override void Exit()
        {
            if (_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
            
            base.Exit();
        }
    }
}