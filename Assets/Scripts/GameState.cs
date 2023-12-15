using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState", order = 1)]
public class GameState : ScriptableObject
{
    public int coinsCollected;
    public int enemiesKilled;
    
    public int level;
    
    public void Reset()
    {
        coinsCollected = 0;
        enemiesKilled = 0;
        level = 0;
    }
}
