using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleWalkGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool RandomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProceduralGenerationAlgos.BinarySpacePartition(new BoundsInt((Vector3Int)startPosition, new Vector3Int
            (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (RandomWalkRooms) 
        {
            floor = CreateRoomsRandomly(roomList);
        }
        else { 
            floor = CreateSimpleRooms(roomList); 
        }

        

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach(var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> hallways = ConnectRooms(roomCenters);
        floor.UnionWith(hallways);

        tilemapVisualizer.PaintFloorTiles(floor);

        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParams, roomCenter);
            foreach(var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.y >= (roomBounds.yMin - offset)&&position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> hallways = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        roomCenters.Remove(currentRoomCenter);
        while(roomCenters.Count > 0) 
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newHallway = CreateHallway(currentRoomCenter, closest);
            currentRoomCenter = closest;
            hallways.UnionWith(newHallway);
        }

        return hallways;
    }

    private HashSet<Vector2Int> CreateHallway(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> hallway = new HashSet<Vector2Int>();
        var position = currentRoomCenter;

        hallway.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            hallway.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            hallway.Add(position);
        }
        return hallway;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float dist = float.MaxValue;
        foreach(var position in roomCenters)
        {
            float currDist = Vector2.Distance(position, currentRoomCenter);
            if(currDist < dist)
            {
                dist = currDist;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach(var room in roomList)
        {
            for(int col = offset; col < room.size.x - offset; ++col)
            {
                for(int row = offset; row < room.size.y - offset; ++row)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col , row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}
