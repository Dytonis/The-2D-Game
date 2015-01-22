using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject target;

    public bool[] DoorPlacements = new bool[4]; //0=up, 1 = right, 2 = down, 3 = left          (manual order)
    public GameObject[] DoorTiles = new GameObject[4]; //0 = down, 1 = left, 2 = right, 3 = up (scan order)

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("CameraTarget")[0];
    }

    void RecieveCollide()
    { 
        target.SendMessage("MoveTarget", gameObject.transform.position);
    }
}
