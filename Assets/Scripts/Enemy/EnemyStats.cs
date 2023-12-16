using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int hp;

    [SerializeField] int armor;

    [SerializeField] int dmg;

    [SerializeField] GameObject coinPrefab;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameState gameState;

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