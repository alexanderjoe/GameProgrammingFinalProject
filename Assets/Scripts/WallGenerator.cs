using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWall(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neigborBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList) 
            {
                var neigborPosition = position + direction;
                if (floorPositions.Contains(neigborPosition))
                {
                    neigborBinaryType += "1";
                }
                else
                {
                    neigborBinaryType += "0";  
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neigborBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighborsBinaryType = "";
            foreach(var direction in Direction2D.cardinalDirectionsList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborsBinaryType += "1";

                }
                else
                {
                    neighborsBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighborsBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) 
        { 
            foreach(var direction in directionList) 
            {
                var neighborPostion = position + direction;
                if (floorPositions.Contains(neighborPostion)==false)
                {
                    wallPositions.Add(neighborPostion);
                }
            }
        }
        return wallPositions;
    }
}
