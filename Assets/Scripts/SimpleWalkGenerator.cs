using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private SimpleRandomWalkData randomWalkParams;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for(int i = 0; i < randomWalkParams.iterations; i++)
        {
            var path = ProceduralGenerationAlgos.SimpleRandomWalk(currentPosition, randomWalkParams.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParams. startRandomlyEachIter)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }
}
