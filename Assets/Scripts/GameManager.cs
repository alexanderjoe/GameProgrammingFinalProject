using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoomFirstDungeonGenerator generator;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        generator.GenerateDungeon();
        var safePlace = generator.FindSafeSpawnLocation();
        player.transform.position = new Vector3(safePlace.x, safePlace.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
