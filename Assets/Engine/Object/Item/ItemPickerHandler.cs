using UnityEngine;
using System.Collections;

public class ItemPickerHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickupClosestItemIfInRange() //message reciever from input
    {
        if(GetComponent<ItemRanger>() && GetComponent<Inventory>())
        {
            if(GetComponent<ItemRanger>().GetNearestItem())
            {
                GameObject itemToPick = GetComponent<ItemRanger>().GetNearestItem();
                GetComponent<Inventory>().AddToInventory(itemToPick);
                Destroy(itemToPick);
            }
        }
    }
}
