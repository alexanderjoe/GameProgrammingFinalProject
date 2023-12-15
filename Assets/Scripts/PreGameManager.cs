using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject winScreen;

    void Start()
    {
        if (gameState.level == 10)
        {
            winScreen.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}