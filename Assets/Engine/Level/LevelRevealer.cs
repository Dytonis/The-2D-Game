using UnityEngine;
using System.Collections;

public class LevelRevealer : MonoBehaviour
{
    private GameObject[] gameObjectList;

    // Use this for initialization
    void Start()
    {
        //Runs after level placer, so this will ignore any new placed gameobjects. Thats OK
        gameObjectList = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject obj in gameObjectList)
            foreach (Transform t in obj.transform)
                t.gameObject.SetActive(false);
    }
}
