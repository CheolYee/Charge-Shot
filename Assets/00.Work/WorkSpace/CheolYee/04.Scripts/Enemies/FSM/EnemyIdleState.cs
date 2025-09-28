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
            
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }

        public override void Exit()
        {
            if (_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
            
            base.Exit();
        }
    }
}