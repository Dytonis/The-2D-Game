using UnityEngine;
using System.Collections;
using UnityEditor;

public class Input : MonoBehaviour 
{

	public EntityMovement move;
	public float speed;

    public Component Affector;

    public enum DriveType
    {
        Keyboard,
        Driven
    }

    public DriveType DriverType;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
        if(DriverType == DriveType.Keyboard)
            {
    		if(UnityEngine.Input.GetAxisRaw("UP")==1f)
    		{
    			move.MoveCommand(IO.DirectionTypes.UP, speed);
    		}

    		if(UnityEngine.Input.GetAxisRaw("DOWN")==1f)
    		{
    			move.MoveCommand(IO.DirectionTypes.DOWN, speed);
    		}

    		if(UnityEngine.Input.GetAxisRaw("LEFT")==1f)
    		{
    			move.MoveCommand(IO.DirectionTypes.LEFT, speed);
    		}

    		if(UnityEngine.Input.GetAxisRaw("RIGHT")==1f)
    		{
    			move.MoveCommand(IO.DirectionTypes.RIGHT, speed);
    		}

            if(UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                if(Affector.GetType() == typeof(ItemPickerHandler))
                    Affector.SendMessage("PickupClosestItemIfInRange");
            }
        }
	}

    public void TriggerInput(IO.DirectionTypes dir)
    {
        if(DriverType == DriveType.Driven)
            move.MoveCommand(dir, speed);
        else
            Debug.LogWarning("Unable to call TriggerInput because Input is not being driven.");
    }

    public void TriggerInputRaw(Vector3 dir)
    {
        move.MoveCommandRaw(dir, speed);
    }
}
