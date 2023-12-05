using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgos
{
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosintion, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        
        path.Add(startPosintion);
        var previousPosition = startPosintion;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }
    public static List<Vector2Int> RandomHallway(Vector2Int startPos, int hallwayLength) 
    {
        List<Vector2Int> hallway = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPos;
        hallway.Add(startPos);

        for (int i = 0; i < hallwayLength; ++i)
        {
            currentPosition += direction;
            hallway.Add(currentPosition);
        }

        return hallway;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//UP
        new Vector2Int(1,0),//RIGHT
        new Vector2Int(0,-1),//DOWN
        new Vector2Int(-1,0)//LEFT
    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}