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
            InventoryItem item = ScriptableObject.CreateInstance<InventoryItem>();
            item.init(obj.GetComponent<Item>().stats);
            if(NextOpenIndexSlot() != null)
                InventoryList[System.Convert.ToInt32(NextOpenIndexSlot())] = item.Clone();
            else
                Debug.LogWarning("Inventory Full!");
        }   
    }

    public int? NextOpenIndexSlot()
    {
        int c = 0;
        foreach(InventoryItem i in InventoryList)
        {
            if(i == null)
                return c;

            c++;
        }

        return null;
    }
}
