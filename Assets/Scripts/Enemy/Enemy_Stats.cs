using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    int hp;

    [SerializeField]
    int armor;

    [SerializeField]
    int dmg;

    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        armor = 1;
        dmg = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Reduce_Hp(int reduction)
    {
        hp -= reduction;
    }

    public void Reduce_Armor(int reduction)
    {
        if(armor > 0 && reduction >= 1)
        {
            armor -= reduction;
        }
    }


}
