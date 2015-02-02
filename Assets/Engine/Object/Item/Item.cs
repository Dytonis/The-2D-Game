using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public StatsPackage stats;
    public Sprite[] ItemSpriteList;
    
    public void Activate()
    {
        
    }
    
    public void Awake()
    {
        stats = Namer.getStats(1);

        if(stats.TYPE == "Dagger")
            GetComponent<SpriteRenderer>().sprite = ItemSpriteList[0];
        
        if(stats.TYPE == "Sword")
            GetComponent<SpriteRenderer>().sprite = ItemSpriteList[1];

    }
}
