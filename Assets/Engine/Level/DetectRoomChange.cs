using UnityEngine;
using System.Collections;

public class DetectRoomChange : MonoBehaviour 
{
	void Update()
    {
        int layerMask = 1 << 10;

        RaycastHit2D HIT_UP = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 0.5f, layerMask);
        RaycastHit2D HIT_RIGHT = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 0.5f, layerMask);
        RaycastHit2D HIT_DOWN = Physics2D.Raycast(gameObject.transform.position, -Vector2.up, 0.5f, layerMask);
        RaycastHit2D HIT_LEFT = Physics2D.Raycast(gameObject.transform.position, -Vector2.right, 0.5f, layerMask);

        if(HIT_UP.collider != null)
        {
            if(HIT_UP.collider.transform.gameObject.GetComponent<Room>())
            {
                HIT_UP.collider.transform.gameObject.SendMessage("RecieveCollide");

                foreach(Transform child in HIT_UP.collider.transform)
                    child.gameObject.SetActive(true);
            }
        }
        if(HIT_RIGHT.collider != null)
        {
            if(HIT_RIGHT.collider.transform.gameObject.GetComponent<Room>())
            {
                HIT_RIGHT.collider.transform.gameObject.SendMessage("RecieveCollide");
                foreach(Transform child in HIT_RIGHT.collider.transform)
                    child.gameObject.SetActive(true);
            }
        }
        if(HIT_DOWN.collider != null)
        {
            if(HIT_DOWN.collider.transform.gameObject.GetComponent<Room>())
            {
                HIT_DOWN.collider.transform.gameObject.SendMessage("RecieveCollide");
                foreach(Transform child in HIT_DOWN.collider.transform)
                    child.gameObject.SetActive(true);
            }
        }
        if(HIT_LEFT.collider != null)
        {
            if(HIT_LEFT.collider.transform.gameObject.GetComponent<Room>())
            {
                HIT_LEFT.collider.transform.gameObject.SendMessage("RecieveCollide");
                foreach(Transform child in HIT_LEFT.collider.transform)
                    child.gameObject.SetActive(true);
            }
        }
    }
}
