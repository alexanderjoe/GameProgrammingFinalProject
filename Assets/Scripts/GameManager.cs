using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoomFirstDungeonGenerator generator;
    [SerializeField] public GameObject exitPortalPrefab;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        generator.GenerateDungeon();
        var safePlace = generator.FindSafeSpawnLocation();
        SpawnPortal();
        player.transform.position = new Vector3(safePlace.x, safePlace.y, 0);
        player.SetActive(true);
    }

    void SpawnPortal()
    {
        var safePortal = generator.FindSafeExitPortalLocation();
        Instantiate(exitPortalPrefab, new Vector3(safePortal.x, safePortal.y, 0), Quaternion.identity);
    }
}
