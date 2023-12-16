using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    int hp;

    [SerializeField]
    int armor;

    [SerializeField]
    int dmg;

    [SerializeField]
    GameObject coinPrefab;
    
    [SerializeField] private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        armor = 1;
        dmg = 5;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)hp / 20;
        if (hp <= 0)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ReduceHP(int reduction)
    {
        reduction -= armor;
        hp -= reduction;
    }

    void setAnimation()
    {
        //TODO;
        //default to walking
        //else swing or take damage from player
    }

    public int getDmg()
    {
        return dmg;
    }
}
