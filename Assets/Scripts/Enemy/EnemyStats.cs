﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    int hp;

    [SerializeField]
    int armor;

    [SerializeField]
    int dmg;

    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        armor = 1;
        dmg = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReduceHP(int reduction)
    {
        hp -= reduction;
    }

    void setAnimation()
    {
        //TODO;
        //default to walking
        //else swing or take damage from player
    }
}
