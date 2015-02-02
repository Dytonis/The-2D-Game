using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemRanger : MonoBehaviour 
{
    public float PickupRange = 0.5f;
    public LayerMask mask;
    public List<GameObject> ItemList = new List<GameObject>();
    public List<GameObject> FilteredItemList = new List<GameObject>();
    public List<GameObject> NearestItemList = new List<GameObject>();

	// Update is called once per frame
	void Update () 
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(gameObject.transform.position, PickupRange, mask);

        foreach(Collider2D h in hit)
            if(h.transform.GetComponent<Item>())
                Debug.DrawLine(gameObject.transform.position, h.transform.position);

        ItemList = hit.Select(x => x.gameObject).Distinct().ToList();
        FilteredItemList = ItemList.Where((GameObject x) => x.GetComponent<Item>()).ToList();
        NearestItemList = FilteredItemList.OrderBy((GameObject x) => Vector3.Distance(x.transform.position, gameObject.transform.position)).ToList(); 
    }

    public GameObject GetNearestItem()
    {
        if(NearestItemList.Count > 0)
            return NearestItemList[0];

        else return null;
    }
}