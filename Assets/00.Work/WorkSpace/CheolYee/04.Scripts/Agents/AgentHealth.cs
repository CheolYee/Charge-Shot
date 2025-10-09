using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Agents
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;

        private float _maxHealth = 150f;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth;

        public float NormalizedHealth => _currentHealth / MaxHealth;


        private Agent _owner;

        public void Initialize(Agent owner, float health)
        {
            _maxHealth = health;
            _owner = owner;
            ResetHealth();
        }


        public void ResetHealth()
        {
            _currentHealth = MaxHealth;
        }

        public void Heal(float healAmount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, MaxHealth);
        }

        public void TakeDamage(float amount, Vector2 normal, float kbPower, bool isRight = false)
        {
            Debug.Assert(_owner != null, $"{nameof(_owner)} 의 체력이 초기화되지 않았습니다.");

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
            onHit?.Invoke();

            if (kbPower > 0)
            {
                //노말은 피격지점의 수직인 벡터니까 -1을 곱하면 피격 방향 벡텀가 나오게 된다
                _owner.MovementComponent.GetKnockBack(normal * -1, kbPower, isRight);
            }

            if (CurrentHealth <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }
}