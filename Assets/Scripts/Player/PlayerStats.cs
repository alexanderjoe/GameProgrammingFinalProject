using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameState gameState;
    
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
    
    [SerializeField]
    public Image healthBar;

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
        healthBar.fillAmount = (float)player_health / 100;
        
        // use coins collected to upgrade stats
        // for every 3 coins, increase damage dealt by 1
        {
            var amount = gameState.coinsCollected / 3;
            player_damage_dealt = 10 + amount;
        }
        
        // for every 10 coins, increase armor by 1
        {
            var amount = gameState.coinsCollected / 10;
            player_armor = amount;
        }
        
    }

    public int GetDamageDealt()
    {
        return player_damage_dealt;
    }

    public float GetAttackSpeed()
    {
        return player_attack_speed;
    }

    public int GetArmor()
    {
        return player_armor;
    }

    public int GetHealth()
    {
        return player_health;
    }

    public void DamagePlayer(int amount)
    {
        // apply armor
        amount -= player_armor;
        if (amount < 0)
        {
            amount = 1;
        }
        
        player_health = Mathf.Clamp(player_health - amount, 0, 100);
    }
    
}
