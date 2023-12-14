using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPlacementHelper
{
    Dictionary<PlacementType, HashSet<Vector2Int>> tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();

    HashSet<Vector2Int> roomFloorNoHallway;

    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoHallway)
    {
        Graph graph = new Graph(roomFloor);
        this.roomFloorNoHallway = roomFloorNoHallway;

        foreach(var position in roomFloorNoHallway)
        {
            int neighborsIn8Dir = graph.GetNeighbors8Directions(position).Count;
            PlacementType type = neighborsIn8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            if(tileByType.ContainsKey(type) == false){
                tileByType[type] = new HashSet<Vector2Int>();
            }

            if(type == PlacementType.NearWall && graph.GetNeighbors4Directions(position).Count == 0) 
            {
                continue;
            }
            tileByType[type].Add(position);
        }
    }

}
public enum PlacementType
{
    OpenSpace,
    NearWall
}