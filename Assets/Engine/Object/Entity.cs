using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour 
{
	public float health = 1f; //initialize as 1 so the object doesnt get killed as it spawns
	public bool autoKillAtNoHealth = true;

	public void Start()
	{

	}

	public void Update()
	{
		if(autoKillAtNoHealth)
			if(health <= 0)
				Debug.Log (kill ());
	}

	public bool kill()
	{
		try
		{
			Destroy(gameObject);
			return true;
		}
		catch(UnityException)
		{
			return false;
		}
	}

	public void takeDamage(float damage)
	{
		health -= damage;
	}
	public void takeDamage(int damage)
	{
		health -= (float)damage;
	}

	public void healDamage(float heal)
	{
		health += heal;
	}
	public void healDamage(int heal)
	{
		health += (float)heal;
	}
}
