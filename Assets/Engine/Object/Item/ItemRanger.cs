using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ItemRanger : MonoBehaviour 
{
    public float PickupRange = 0.5f;
    public LayerMask mask;
    public List<GameObject> ItemList = new List<GameObject>();
    public List<GameObject> FilteredItemList = new List<GameObject>();
    public List<GameObject> NearestItemList = new List<GameObject>();

    public GameObject ItemPanelUI;
    public GameObject Canvas;
    private GameObject ui;

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

        if(NearestItemList.Count > 0)
        {
            Vector3 itemScreenSpace = Camera.main.WorldToScreenPoint(NearestItemList[0].transform.position);
            if(ui == null)
            {
                ui = Instantiate(ItemPanelUI, new Vector3(itemScreenSpace.x, itemScreenSpace.y, itemScreenSpace.z), NearestItemList[0].transform.rotation) as GameObject;
                ui.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

            }
            if(ui != null)
            {
                ui.transform.position = new Vector3(itemScreenSpace.x, ((itemScreenSpace.y + 50 + 70) <= (Camera.main.pixelHeight)) ? itemScreenSpace.y + 70 : (Camera.main.pixelHeight) - 50, itemScreenSpace.z);
                ui.transform.GetChild(0).GetComponent<Text>().text = "<b>" + NearestItemList[0].GetComponent<Item>().stats.NAME + "</b>";
                ui.transform.GetChild(0).GetComponent<Text>().color = NearestItemList[0].GetComponent<Item>().stats.RARITYCOLOR;
                ui.transform.GetChild(1).GetComponent<Text>().text = "DAMAGE: " + NearestItemList[0].GetComponent<Item>().stats.DAMAGE;
            }
        }
        else
        {
            if(ui != null)
                Destroy(ui);
        }
    }

    public GameObject GetNearestItem()
    {
        if(NearestItemList.Count > 0)
            return NearestItemList[0];

        else return null;
    }
}