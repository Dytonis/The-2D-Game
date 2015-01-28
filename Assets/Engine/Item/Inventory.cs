using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<InventoryItem>
        InventoryList = new List<InventoryItem>();
    
    public void AddToInventory(GameObject obj)
    {
        if (obj.GetComponent<Item>())
        {
            InventoryItem item = new InventoryItem(obj, new StatsPackage());
            InventoryList.Add(item);
        }   
    }
}
