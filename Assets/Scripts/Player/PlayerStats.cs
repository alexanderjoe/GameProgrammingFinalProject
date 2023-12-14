using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    int player_health;

    [SerializeField]
    int player_speed;
    //get set movement script stuff

    [SerializeField]
    int player_damage_dealt; //base damage of attack1, we can just incremement/scale with more equipment or whatever

    [SerializeField]
    float player_attack_speed;

    [SerializeField]
    int player_armor; //IAN: maybe we have stuff that provides Damage reduction as armor?

    // Start is called before the first frame update
    void Start()
    {
        //initialize to default values
        player_armor = 0;
        player_health = 100; //IAN: 100 is just a number I put
        player_damage_dealt = 10;
        player_attack_speed = 1; //1 per second is what I'm aiming for here
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: gets sets
}