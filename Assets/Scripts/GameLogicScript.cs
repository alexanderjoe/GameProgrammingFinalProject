using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private EndGameScript endGameScript;
    public BoingoCreatureScript boingoCreatureScript;

    private Rigidbody2D _playerRigidbody2D;


    void Start()
    {
        boingoCreatureScript.isAlive = true;
        boingoCreatureScript.isSpawned = false;
    }
    
    void Update()
    {
        if(boingoCreatureScript.isAlive == false)
        {
            endGameScript.Setup();
        }
    }
}
