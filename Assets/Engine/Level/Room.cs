using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject target;

    public bool[] DoorPlacements = new bool[4]; //0 = down, 1 = left, 2 = right, 3 = up          (scan order)
    public GameObject[] DoorTiles = new GameObject[4]; //0 = down, 1 = left, 2 = right, 3 = up   (scan order)
    public bool isSpawnRoom = false;
    public bool spooled = false;
    private Vector3 _center = new Vector3();
    public Vector3 center
    {
        get
        {
            if(_center.x == 0 && _center.y == 0)
                _center = new Vector3(gameObject.transform.position.x + 7.2f, gameObject.transform.position.y + 5.6f);

            return _center;
        }
        set
        {
            _center = value;
        }
    }

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("CameraTarget")[0];
        center = new Vector3(transform.position.x+7.2f, transform.position.y+5.6f);
    }

    void RecieveCollide()
    { 
        target.SendMessage("MoveTarget", gameObject.transform.position);
    }

    public bool isAnyDoors()
    {
        if(DoorPlacements[0] == false)
            if(DoorPlacements[1] == false)
                if(DoorPlacements[2] == false)
                    if(DoorPlacements[3] == false)
                        return false;

        return true;
    }
}
