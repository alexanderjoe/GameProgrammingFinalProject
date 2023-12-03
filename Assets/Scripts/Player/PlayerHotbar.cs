using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbar : MonoBehaviour
{
    Item[] items;
    int itemCount;
    const int MAX_ITEMS = 4; //I picked 4 for no reason, probably change later
    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        items = new Item[MAX_ITEMS];
        //maybe have some kind of starting items?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Try_AddItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("ITEM ERROR: tried to add null item");
            return false;
        }

        if(items.Length == MAX_ITEMS)
        {
            Debug.Log("ITEM ERROR: hotbar is full");
            return false;
        }
        items[itemCount] = item;
        itemCount++;

        Debug.Log("Item " + item + " successfully added!");
        return true;
    }

    bool Try_RemoveItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("ITEM ERROR: tried to remove null item");
            return false;
        }

        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                itemCount--;
            }
        }
        Debug.Log("Item " + item + " successfully removed!");
        return true;
    }
}
