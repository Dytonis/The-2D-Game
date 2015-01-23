using UnityEngine;
using System.Collections;

public class LevelRevealer : MonoBehaviour
{
    public float MaskDistance = 10f;
    private GameObject[] gameObjectList;

    // Use this for initialization
    void Start()
    {
        //Runs after level placer, so this will ignore any new placed gameobjects. Thats OK
        gameObjectList = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject obj in gameObjectList)
            obj.SetActive(false);
    }

    void Update()
    {
        /*foreach (GameObject go in gameObjectList)
        {
            if (Vector3.Distance(gameObject.transform.position, go.transform.position) <= MaskDistance)
            if (go.GetComponent<SpriteRenderer>())
                    go.SetActive(true);
        }*/
    }
}
