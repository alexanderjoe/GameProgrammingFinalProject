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

        List<List<Vector2Int>> hallways = CreateHalls(floorPositions, potentialRoomPositions);

        CreateHalls(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for(int i = 0; i < hallways.Count; i++)
        {
            //hallways[i] = IncreaseHallSizeByOne(hallways[i]);
            hallways[i] = IncreaseHallwayBrush3x3(hallways[i]);
            floorPositions.UnionWith(hallways[i]);
            
        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private List<Vector2Int> IncreaseHallwayBrush3x3(List<Vector2Int> hallways)
    {
        List<Vector2Int> newHallway = new List<Vector2Int>();
        for(int i =1; i < hallways.Count;i++)
        {
            for(int x = -1; x < 2; ++x) 
            { 
                for(int y = -1; y < 2; ++y)
                {
                    newHallway.Add(hallways[i-1] + new Vector2Int(x, y));
                }
            }
        }
        return newHallway;
    }

    private List<Vector2Int> IncreaseHallSizeByOne(List<Vector2Int> hallways)
    {
        List<Vector2Int> newHalls = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for(int i = 1; i < hallways.Count; ++i)
        {
            Vector2Int directionFromCell = hallways[i] - hallways[i - 1];
            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                //corner case
                for(int x = -1; x < 2; ++x)
                {
                    for(int y = -1; y < -2; ++y)
                    {
                        newHalls.Add(hallways[i-1]+ new Vector2Int(x,y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                //add single cell in direction + 90 degrees
                Vector2Int newHallTileOffset = GetDirection90From(directionFromCell);
                newHalls.Add(hallways[i - 1]);
                newHalls.Add(hallways[i - 1] + newHallTileOffset);
            }
        }

        return newHalls;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if(direction == Vector2Int.up){
            return Vector2Int.right;
        }if(direction == Vector2Int.right){
            return Vector2Int.down;
        }if(direction == Vector2Int.down){
            return Vector2Int.left;
        }if(direction == Vector2Int.left){
            return Vector2Int.right;
        }return Vector2Int.zero;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach(var position in deadEnds)
        {
            if (roomPositions.Contains(position) == false)
            {
                var roomPos = RunRandomWalk(randomWalkParams, position);
                roomPositions.UnionWith(roomPos);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(var position in floorPositions)
        {
            int neighborCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighborCount++;
                }
            }
            if(neighborCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
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

    private List<List<Vector2Int>> CreateHalls(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currPos = startPosition;
        potentialRoomPositions.Add(currPos);
        List<List<Vector2Int>> halls = new List<List<Vector2Int>>();

        for (int i = 0; i < hallCount; i++)
        {
            var hall = ProceduralGenerationAlgos.RandomHallway(currPos, hallLength);
            halls.Add(hall);
            currPos = hall[hall.Count-1];
            potentialRoomPositions.Add(currPos);
            floorPositions.UnionWith(hall);
        }

        return halls;
    }
}
