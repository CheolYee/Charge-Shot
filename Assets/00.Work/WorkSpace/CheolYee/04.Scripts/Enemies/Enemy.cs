using _00.Work.Scripts.SO;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Agents;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.FSM;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies.SO;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies
{
    public enum EnemyBehaviourType
    {
        Air = 0,
        Idle = 1,
        Chase = 2,
        Jump = 3,
        Attack = 4,
        Death = 5,
    }

    public class Enemy : Agent, IPoolable
    {
        [Header("Enemy Settings")] 
        [SerializeField] private EnemyDataSo enemyData;
        [SerializeField] private string itemName;
        
        [Header("Attack Settings")]
        public float attackRadius; // 공격이 가능한 거리
        [HideInInspector] public float lastAttackTime;
        
        public string ItemName => itemName;
        public GameObject GameObject => gameObject;
        public float Damage { get; private set; }
        public float MoveSpeed { get; private set; }
        public float JumpForce { get; private set; }
        public float AttackSpeed { get; private set; }
        
        protected EnemyStateMachine StateMachine; //FSM 머신 설정
        
        public Transform TargetTransform {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            Damage = enemyData.attackDamage;
            JumpForce = enemyData.jumpForce;
            MoveSpeed = enemyData.moveSpeed;
            AttackSpeed = enemyData.attackSpeed;
            
            HealthComponent.Initialize(this, enemyData.maxHealth);
            MovementComponent.GetComponent<EnemyMovement>().Initialize(MoveSpeed, JumpForce);

            StateMachine = new EnemyStateMachine();
            
            StateMachine.AddState(EnemyBehaviourType.Idle, new EnemyIdleState(this, StateMachine, "IDLE"));
            StateMachine.AddState(EnemyBehaviourType.Chase, new EnemyChaseState(this, StateMachine, "CHASE"));
            StateMachine.AddState(EnemyBehaviourType.Attack, new EnemyAttackState(this, StateMachine, "ATTACK"));
            StateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, StateMachine, "DEATH"));
            
            StateMachine.Initialize(EnemyBehaviourType.Idle, this);
        }

        private void Start()
        {
            TargetTransform = GameManager.Instance.TargetTransform;
        }

        private void Update()
        {
            if (!IsDead)
            {
                HandleSpriteFlip(TargetTransform.position);
            }
        }
        
        public void SetDead() //죽은 상태로 만들기
        {
            StateMachine.ChangeState(EnemyBehaviourType.Death);
        }

        public void AnimationEndTrigger() //애니메이션이 끝났을 떄
        {
            StateMachine.CurrentState.AnimationEndTrigger(); //애니메이션 종료 시 현재 상태에 맞는 엔드트리거 실행
        }

        public void ResetItem()
        {
            IsDead = false;
            TargetTransform = GameManager.Instance.TargetTransform;
            StateMachine.ChangeState(EnemyBehaviourType.Idle);
            HealthComponent.ResetHealth();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
}