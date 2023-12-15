using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameState gameState;

    // Anything that needs to be done before the game starts
    void Start()
    {
        Time.timeScale = 1;
        gameState.Reset();
    }
}