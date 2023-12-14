using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/PreStartScene");
    }
}