using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoomFirstDungeonGenerator generator;
    public GameObject portalPrefab;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        generator.GenerateDungeon();
        var safePlace = generator.FindSafeSpawnLocation();
        SpawnPortal();
        player.transform.position = new Vector3(safePlace.x, safePlace.y, 0);
    }

    void SpawnPortal()
    {
        var portalPlace = generator.FindPortalSpawnLocation();
        Instantiate(portalPrefab, new Vector3(portalPlace.x, portalPlace.y, 0), Quaternion.identity);
    }
}
