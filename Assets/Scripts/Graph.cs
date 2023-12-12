using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graph 
{
    public static List<Vector2Int> neighbors4Directions = new List<Vector2Int>
    {
        new Vector2Int(0,1),//up
        new Vector2Int(1,0),//right
        new Vector2Int(0,-1),//down
        new Vector2Int(1,-0),//left
    };
    public static List<Vector2Int> neighbors8Directions = new List<Vector2Int> {
        new Vector2Int(0,1),//up
        new Vector2Int(1,0),//right
        new Vector2Int(0,-1),//down
        new Vector2Int(1,-0),//left
        new Vector2Int(1,1),//diagonal1
        new Vector2Int(1,-1),//diagonal2
        new Vector2Int(-1,-1),//diagonal3
        new Vector2Int(-1,1),//diagonal4
    };
    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> verticies)
    {
        graph = new List<Vector2Int>(verticies);
    }
    public List<Vector2Int> GetNeighbors4Directions(Vector2Int startPosition)
    {
        return GetNeighbors(startPosition, neighbors4Directions);
    }
    public List<Vector2Int> GetNeighbors8Directions(Vector2Int startPosition)
    {
        return GetNeighbors(startPosition, neighbors8Directions);
    }

    
    private List<Vector2Int> GetNeighbors(Vector2Int startPosition, List<Vector2Int> neighborsOffsetList)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        foreach(var neighborDirection in neighborsOffsetList)
        {
            Vector2Int potentialNeighbor = startPosition + neighborDirection;
            if(graph.Contains(potentialNeighbor))
            {
                neighbors.Add(potentialNeighbor);
            }

        }

        return neighbors;
    }
}
