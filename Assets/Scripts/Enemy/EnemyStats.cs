using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int hp;
    private int _maxHp;

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
        _maxHp = hp = 20 + ((gameState.level - 1) * 5);
        armor = 1 + (gameState.level - 1) / 2;
        dmg = 5 + (gameState.level - 1) * 2;
    }

    // Update is called once per frame
    void Update()
    {
        int r = rand.Next(101);
        healthBar.fillAmount = (float)hp / _maxHp;
        if (hp <= 0)
        {
            if (r <= 65)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            else
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

    public int getDmg()
    {
        return dmg;
    }
}