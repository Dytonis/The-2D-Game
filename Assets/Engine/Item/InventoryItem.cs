using UnityEngine;
using System.Collections;

public class InventoryItem : ScriptableObject
{
    public StatsPackage stats;
    public GameObject objectHandle;

    public InventoryItem(GameObject prefab, StatsPackage package)
    {
        objectHandle = prefab;
        stats = package;
    }	
}
