using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RoomFirstDungeonGenerator generator;
    public GameObject portalPrefab;
    public GameState gameState;
    public GameObject deathScreen;

    private GameObject _player;
    private EnemySpawner _enemySpawner;
    private PlayerStats _playerStats;

    // Start is called before the first frame update
    void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();

        gameState.level += 1;

        generator.GenerateDungeon();
        SpawnPortal();

        var spawnLocs = generator.FindEnemySpawnLocations();
        _enemySpawner.SpawnEnemies(spawnLocs, gameState.level);

        var safePlace = generator.FindSafeSpawnLocation();
        _player.transform.position = new Vector3(safePlace.x, safePlace.y, 0);
    }

    private void Update()
    {
        if (_playerStats.GetHealth() <= 0)
        {
            // Time.timeScale = 0;
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