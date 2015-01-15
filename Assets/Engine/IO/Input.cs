using UnityEngine;
using System.Collections;

public class Input : MonoBehaviour 
{

	public EntityMovement move;
	public float speed;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(UnityEngine.Input.GetAxisRaw("UP")==1f)
		{
			move.MoveCommand("UP", speed);
		}

		if(UnityEngine.Input.GetAxisRaw("DOWN")==1f)
		{
			move.MoveCommand("DOWN", speed);
		}

		if(UnityEngine.Input.GetAxisRaw("LEFT")==1f)
		{
			move.MoveCommand("LEFT", speed);
		}

		if(UnityEngine.Input.GetAxisRaw("RIGHT")==1f)
		{
			move.MoveCommand("RIGHT", speed);
		}
	}
}
