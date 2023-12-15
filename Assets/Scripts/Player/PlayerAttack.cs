using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attack_hitbox1;
    public GameObject attack_hitbox2;

    GameLogicScript _gls;
    EnemyStats _es;
    PlayerStats _ps;

    

    bool attack1 = false;
    bool attack2 = false;

    bool tryingAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        attack_hitbox1.SetActive(false);
        attack_hitbox2.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            attack1 = true;
            tryingAttack = true;
            StartCoroutine(Attack_Delay());
        } else if (Input.GetKeyDown(KeyCode.K)){
            attack2 = true;
            tryingAttack = true;
            StartCoroutine(Attack_Delay());
        } else
        {
            tryingAttack=false;
        }
    }

    IEnumerator Attack_Delay()
    {
        Debug.Log("attack1: " + attack1 + " attack2: " + attack2);
        if (attack1)
        {
            attack_hitbox1.SetActive(true);
        } else
        {
            attack_hitbox2.SetActive(true);
        }
        yield return new WaitForSeconds(.01f);
        if (attack1)
        {
            attack_hitbox1.SetActive(false);
            attack1 = false;
        }
        else
        {
            attack_hitbox2.SetActive(false);
            attack2 = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tryingAttack && collision.gameObject.CompareTag("Enemy"))
        {
            _es = collision.gameObject.GetComponent<EnemyStats>();

            if (_ps == null)
            {
                _ps = GetComponent<PlayerStats>();
            }

            if (_es != null && _ps != null)
            {
                _es.ReduceHP(_ps.GetDamageDealt());
                Debug.Log("Skele HIT for " + _ps.GetDamageDealt());
                tryingAttack = false;
            }
            else
            {
                Debug.Log("EnemyStats or PlayerStats component not found on the collided object or player object.");
            }
        }
        else
        {
            Debug.Log("Collision with a GameObject containing "+collision.gameObject.tag);
        }
    }


}
