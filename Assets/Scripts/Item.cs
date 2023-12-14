using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int uses;

    [SerializeField]
    string name;

    [SerializeField]
    string description;

    // Start is called before the first frame update
    void Start()
    {
        //IAN:not sure about initializing fields, item creation should od that
    }
}
