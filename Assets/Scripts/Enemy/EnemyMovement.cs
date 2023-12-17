using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public int chaseDistance = 10;
        public bool isChasing;

        private Animator _animator;
        private GameObject _player;

        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_player == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (distanceToPlayer <= chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (isChasing)
            {
                var enemyPosition = transform.position;
                var playerPosition = _player.transform.position;

                Vector3 playerDirection = playerPosition - enemyPosition;

                _animator.SetFloat(Horizontal, playerDirection.x);
                _animator.SetFloat(Vertical, playerDirection.y);

                transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(Horizontal, 0);
                _animator.SetFloat(Vertical, 0);
            }
        }
    }
}