using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingoCreatureScript : MonoBehaviour
{
    public GameObject BoingoCreature;
    public GameObject GameState;
    public GameLogicScript _gameLogicScript;
    public OingoAttackScript oingoAttackScript;
    public GameObject oingoAttack;
    GameObject player;
    public bool isSpawned;
    public bool isAlive;
    public int distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _gameLogicScript = GameObject.Find("GameLogic").GetComponent<GameLogicScript>();
        oingoAttackScript = gameObject.GetComponent<OingoAttackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        isSpawned = true;
        distanceFromPlayer = 100;
    }

    // Update is called once per frame
    void Update()
    {
        var tempTransform = transform;
        var bossScale = tempTransform.localScale;
        if (player.transform.position.x >= BoingoCreature.transform.position.x && bossScale.x > 0 && player.transform.position.y >= BoingoCreature.transform.position.y)
        {
            FlipBoss();
        }
        else if (player.transform.position.x <= BoingoCreature.transform.position.x && bossScale.x < 0 && player.transform.position.y <= BoingoCreature.transform.position.y)
        {
            FlipBoss();
        }
        else if(player.transform.position.x <= BoingoCreature.transform.position.x && bossScale.x < 0 && player.transform.position.y >= BoingoCreature.transform.position.y)
        {
            FlipBoss();
        }
        else if (player.transform.position.x >= BoingoCreature.transform.position.x && bossScale.x < 0 && player.transform.position.y <= BoingoCreature.transform.position.y)
        {
            FlipBoss();
        }
    }
    void FlipBoss()
    {
        var tempTransform = transform;
        var bossScale = tempTransform.localScale;
        bossScale.x *= -1;
        bossScale.y *= -1;
        tempTransform.localScale = bossScale;
    }
}
