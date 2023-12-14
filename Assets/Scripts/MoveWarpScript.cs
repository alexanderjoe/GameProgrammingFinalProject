using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveWarpScript : MonoBehaviour
{
    public GameObject warpTile;

    public float itemXSpread = 5;
    public float itemYSpread = 0;

    void Start()
    {
        
    }
    void SpreadItems()
    {
        Vector3 randPosition = new Vector3((int)Random.Range(-itemXSpread, itemXSpread), (int)Random.Range(-itemYSpread,itemYSpread), 0);//using 0 for the Z cause 2D game lol
        GameObject clone = Instantiate(warpTile, randPosition, Quaternion.identity);

    }
    void Update()
    {
        
    }
}
