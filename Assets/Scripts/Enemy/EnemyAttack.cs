using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public Transform attackPoint;
        public LayerMask playerLayers;

        public float attackRange = 0.5f;
        public float attackRate = 2f;
        private float _nextAttackTime = 0f;
        private bool _inRange;

        private Animator _animator;
        private PlayerCombat _playerCombat;
        private EnemyStats _es;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Damage = Animator.StringToHash("Damage");

        void Start()
        {
            _playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            _animator = GetComponent<Animator>();
            _es = GetComponent<EnemyStats>();
        }

        void Update()
        {
            if (!_inRange) return;
            if (!(Time.time >= _nextAttackTime)) return;

            Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);

            if (colInfo != null)
            {
                _animator.SetTrigger(Attack);
                _nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        void AttackPlayer()
        {
            if (!_inRange) return;
            _playerCombat.TakeDamage(_es.getDmg());
        }

        public void TakeDamage(int damage)
        {
            _animator.SetTrigger(Damage);
            _es.ReduceHP(damage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _inRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _inRange = false;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}