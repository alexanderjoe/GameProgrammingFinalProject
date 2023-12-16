using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float _nextAttackTime = 0f;

    private PlayerStats _ps;

    private void Start()
    {
        _ps = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // When the player damages another entity.
    void Attack()
    {
        animator.SetTrigger("Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().ReduceHP(_ps.GetDamageDealt());
        }
    }
    
    // When the player is damaged by another entity.
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        _ps.DamagePlayer(damage);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}