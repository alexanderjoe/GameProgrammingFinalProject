﻿using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int hp;

    [SerializeField] int armor;

    [SerializeField] int dmg;

    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject potionPrefab;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameState gameState;

    Random rand = new Random();


    void Start()
    {
        // use level to determine stats
        hp = 20 + (gameState.level * 5);
        armor = 1 + (gameState.level / 2);
        dmg = 5 + (gameState.level * 2);
    }

    // Update is called once per frame
    void Update()
    {
        int r = rand.Next(101);
        healthBar.fillAmount = (float)hp / 20;
        if (hp <= 0)
        {
            Debug.Log("SKELETON DROPPPING " + r);
            if(r <= 65)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            } else if(r >= 35)
            {
                Instantiate(potionPrefab, transform.position, Quaternion.identity);
            }
            
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