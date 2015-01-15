using UnityEngine;

public class Tile : MonoBehaviour 
{
 	public byte colliderType;
	public byte tildID;

	void Start()
	{
		Debug.DrawRay(transform.position, Vector2.up, Color.yellow, 10f);
	}
}