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
    IEnumerator SpawnTestTokenUnifDist(float minTime, float maxTime)
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            Vector3 position = BoingoCreature.transform.position;
            Instantiate(oingoAttack, position, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameLogicScript = GameObject.Find("GameLogic").GetComponent<GameLogicScript>();
        oingoAttackScript = gameObject.GetComponent<OingoAttackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        isSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        var tempTransform = transform;
        var bossScale = tempTransform.localScale;
        if (player.transform.position.x >= BoingoCreature.transform.position.x && bossScale.x > 0)
        {
            FlipBoss();
        }
        else if (player.transform.position.x <= BoingoCreature.transform.position.x && bossScale.x < 0)
        {
            FlipBoss();
        }

    }
    void FlipBoss()
    {
        var tempTransform = transform;
        var bossScale = tempTransform.localScale;
        bossScale.x *= -1;
        tempTransform.localScale = bossScale;
    }
}
