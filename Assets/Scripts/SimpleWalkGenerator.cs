using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkData randomWalkParams;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParams, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData randomWalkParams, Vector2Int position)
    {
        var currentPosition = position;
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
