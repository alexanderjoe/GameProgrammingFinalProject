using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject skeletronPrefab;

    public void SpawnEnemies(List<Vector2Int> spawnLocs, int level)
    {
        foreach (var position in spawnLocs)
        {
            for (int i = 0; i < level; i++)
            {
                SpawnEnemy(position);
            }
        }
    }
    
    void SpawnEnemy(Vector2Int position)
    {
        Instantiate(skeletronPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }
}
