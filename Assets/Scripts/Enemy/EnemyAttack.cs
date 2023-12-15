using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    int chaseDistance = 10;
    bool isChasing = false;

    EnemyStats _es;
    public PlayerStats _ps;

    bool tryAttack = false;

    public GameObject attackHitbox;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;

            if (distanceToPlayer <= 1.0)
            {
                //attack animation

                //Debug.Log("Attacking player");
                tryAttack = true;
            }
            else
            {
                tryAttack = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tryAttack)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _ps = collision.gameObject.GetComponent<PlayerStats>();
                _es = gameObject.GetComponent<EnemyStats>();
                _ps.DamagePlayer(_es.getDmg());
                //Debug.Log("Player HIT for " + _es.getDmg());
            }
            else
            {
                //Debug.Log("Collision with a GameObject containing " + collision.gameObject.tag);
            }
        }
    }

}
