using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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
    public static List<BoundsInt> BinarySpacePartition(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt> ();
        List<BoundsInt> roomsList = new List<BoundsInt> ();

        roomQueue.Enqueue(spaceToSplit);
        while(roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue ();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(UnityEngine.Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomQueue, room);
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {                    
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList;
    }

    private static void SplitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x+xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x-xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y-ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
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
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1),//UP-RIGHT
        new Vector2Int(1,-1),//RIGHT-DOWN
        new Vector2Int(-1,-1),//DOWN-LEFT
        new Vector2Int(-1,1)//LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//UP
        new Vector2Int(1,1),//UP-RIGHT
        new Vector2Int(1,0),//RIGHT
        new Vector2Int(1,-1),//RIGHT-DOWN
        new Vector2Int(0,-1),//DOWN
        new Vector2Int(-1,-1),//DOWN-LEFT
        new Vector2Int(-1,0),//LEFT
        new Vector2Int(-1,1)//LEFT-UP
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}