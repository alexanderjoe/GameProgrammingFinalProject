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

    bool tryAttack = false;

    public GameObject attackHitbox;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
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

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats _ps = player.GetComponent<PlayerStats>();

            if (_ps != null)
            {
                _ps.ReduceHP(
                    _es.getDmg());
                Debug.Log("Player HIT for " + _es.getDmg());
            }
        }
        else
        {
            Debug.Log("Collision with a GameObject containing " + collision.gameObject.tag);
        }
    }
}
