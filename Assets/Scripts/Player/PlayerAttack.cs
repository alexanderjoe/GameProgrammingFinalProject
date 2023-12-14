using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attack_hitbox1;
    public GameObject attack_hitbox2;

    bool attack1 = false;
    bool attack2 = false;

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
            StartCoroutine(Attack_Delay());
        } else if (Input.GetKeyDown(KeyCode.K)){
            attack2 = true;
            StartCoroutine(Attack_Delay());
        }
    }

    IEnumerator Attack_Delay()
    {
        if (attack1)
        {
            attack_hitbox1.SetActive(true);
        } else
        {
            attack_hitbox2.SetActive(true);
        }
        yield return new WaitForSeconds(.1f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("skele"))
        {
            //deal damage
            Debug.Log("Skele HIT");
        } else
        {
            Debug.Log("\n\n\n "+gameObject.name);
        }
    }
}
