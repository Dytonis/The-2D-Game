using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public StatsPackage stats;
    public Sprite[] ItemSpriteList;

    public float cooldownRemaining = 0.0f;

    public void Activate()
    {
        if(cooldownRemaining <= 0.0f)
        {

        }
    }
    
    public void Awake()
    {
        stats = Namer.getStats(1);

        if(stats.TYPE == "Dagger")
            GetComponent<SpriteRenderer>().sprite = ItemSpriteList[0];
        
        if(stats.TYPE == "Sword")
            GetComponent<SpriteRenderer>().sprite = ItemSpriteList[1];

        if(stats.TYPE == "Magic")
            GetComponent<SpriteRenderer>().sprite = ItemSpriteList[2];

    }
}
