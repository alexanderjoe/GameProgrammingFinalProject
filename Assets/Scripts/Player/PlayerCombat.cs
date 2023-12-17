using Enemy;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AudioClip[] attackSounds;
    
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    private float _nextAttackTime = 0f;

    private PlayerStats _ps;
    private AudioSource _audioSource;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    private void Start()
    {
        _ps = GetComponent<PlayerStats>();
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger(Attack1);
                _nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // When the player damages another entity.
    // This is actually called by the animation so the hit occurs at the right time.
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAttack>().TakeDamage(_ps.GetDamageDealt());
            // enemy.GetComponent<EnemyStats>().ReduceHP(_ps.GetDamageDealt());
        }
    }
    
    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
    }
    
    // When the player is damaged by another entity.
    public void TakeDamage(int damage)
    {
        animator.SetTrigger(Hit);
        _ps.DamagePlayer(damage);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}