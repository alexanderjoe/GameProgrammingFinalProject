using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using UnityEngine;

public class HallwayFirstDungeonGen : SimpleWalkGenerator
{
    [SerializeField]
    private int hallLength = 14, hallCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;


    protected override void RunProceduralGeneration()
    {
        HallwayFirstGen();
    }

    private void HallwayFirstGen()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateHalls(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToMakeAmount = Mathf.RoundToInt(potentialRoomPositions.Count*roomPercent);
        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToMakeAmount).ToList();
        
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParams, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateHalls(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currPos = startPosition;
        potentialRoomPositions.Add(currPos);

        for (int i = 0; i < hallCount; i++)
        {
            var hall = ProceduralGenerationAlgos.RandomHallway(currPos, hallLength);
            currPos = hall[hall.Count-1];
            potentialRoomPositions.Add(currPos);
            floorPositions.UnionWith(hall);
        }
    }
}
