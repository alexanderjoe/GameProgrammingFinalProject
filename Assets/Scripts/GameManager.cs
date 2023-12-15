using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public RoomFirstDungeonGenerator generator;
    public GameObject portalPrefab;
    public GameState gameState;
    public GameObject deathScreen;

    private GameObject player;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        gameState.level += 1;
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        generator.GenerateDungeon();
        var safePlace = generator.FindSafeSpawnLocation();
        SpawnPortal();
        player.transform.position = new Vector3(safePlace.x, safePlace.y, 0);
    }

    private void Update()
    {
        if (playerStats.GetHealth() <= 0)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void SpawnPortal()
    {
        var portalPlace = generator.FindPortalSpawnLocation();
        Instantiate(portalPrefab, new Vector3(portalPlace.x, portalPlace.y, 0), Quaternion.identity);
    }
}