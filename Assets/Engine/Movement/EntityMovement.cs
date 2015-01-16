using UnityEngine;
using System.Collections;

public class EntityMovement : MonoBehaviour 
{
	public float velx;
	public float vely;

	public float speedLimit; 

	public float friction;

	private float cutoffSpeed = 0.05f;

	private delegate bool Comparator(int x, int y);
	private Comparator equalTo = delegate(int x, int y) { return x == y; };
	private Comparator notEqualTo = delegate(int x, int y) { return x != y; };

	public enum CollisionTypes
	{
		CollideWithAllColliders,
		CollideWithSolidColliders,
		CollideWithHalfColliders,
		CollideWithNothing
	}
	public CollisionTypes CollisionType;

	public void Start()
	{

	}

	public void Update()
	{
		if(CollisionType != CollisionTypes.CollideWithNothing)
			checkCollision();

		gameObject.transform.Translate(velx*Time.deltaTime, vely*Time.deltaTime, 0);

		if(UnityEngine.Input.GetAxisRaw("UP") == 0 && UnityEngine.Input.GetAxisRaw("DOWN") == 0)
		{
			if(vely > 0)
			{
				vely /= friction + 1;
			}

			if(vely < 0)
			{
				vely /= friction + 1;
			}
		}

		if(UnityEngine.Input.GetAxisRaw("LEFT") == 0 && UnityEngine.Input.GetAxisRaw("RIGHT") == 0)
		{
			if(velx > 0)
			{
				velx /= friction + 1;
			}

			if(velx < 0)
			{
				velx /= friction + 1;
			}
		}

		if(Mathf.Abs(velx) <= cutoffSpeed)
			velx = 0;
		if(Mathf.Abs(vely) <= cutoffSpeed)
			vely = 0;
	}

	public void checkCollision()
	{
		float raydistance = 0.5f;
		byte operation = 0; //0 for equal, 1 for not equal.
		int targetColliderType = 1;

		if(CollisionType == CollisionTypes.CollideWithAllColliders)
		{
			targetColliderType = 0;
			operation = 1;
		}
		if(CollisionType == CollisionTypes.CollideWithSolidColliders)
			targetColliderType = 1;
		if(CollisionType == CollisionTypes.CollideWithHalfColliders)
			targetColliderType = 2;

		int layerMask = 1 << 9;

		Debug.Log("Layer mask: " + layerMask);

		//Vector2 CENTER = transform.position; MUMMY
		Vector2 TOPLEFT = new Vector2(transform.position.x - (2f/3f)+0.05f, transform.position.y + (2f/3f)-0.05f);
		Vector2 TOPRIGHT = new Vector2(transform.position.x + (2f/3f)-0.05f, transform.position.y + (2f/3f)-0.05f);
		Vector2 BOTLEFT = new Vector2(transform.position.x - (2f/3f)+0.05f, transform.position.y - (2f/3f)+0.05f);
		Vector2 BOTRIGHT = new Vector2(transform.position.x + (2f/3f)-0.05f, transform.position.y - (2f/3f)+0.05f);

		//TOPRIGHT -> UP
		RaycastHit2D HIT_TOPRIGHT_UP = Physics2D.Raycast(TOPRIGHT, Vector2.up, raydistance, layerMask);
		//TOPRIGHT-> RIGHT
		RaycastHit2D HIT_TOPRIGHT_RIGHT = Physics2D.Raycast(TOPRIGHT, Vector2.right, raydistance, layerMask);
		//TOPLEFT -> UP
		RaycastHit2D HIT_TOPLEFT_UP = Physics2D.Raycast(TOPLEFT, Vector2.up, raydistance, layerMask);
		//TOPLEFT -> LEFT
		RaycastHit2D HIT_TOPLEFT_LEFT = Physics2D.Raycast(TOPLEFT, -Vector2.right, raydistance, layerMask);
		//BOTRIGHT -> DOWN
		RaycastHit2D HIT_BOTRIGHT_DOWN = Physics2D.Raycast(BOTRIGHT, -Vector2.up, raydistance, layerMask);
		//BOTRIGHT-> RIGHT
		RaycastHit2D HIT_BOTRIGHT_RIGHT = Physics2D.Raycast(BOTRIGHT, Vector2.right, raydistance, layerMask);
		//BOTLEFT -> DOWN
		RaycastHit2D HIT_BOTLEFT_DOWN = Physics2D.Raycast(BOTLEFT, -Vector2.up, raydistance, layerMask);
		//BOTLEFT -> LEFT
		RaycastHit2D HIT_BOTLEFT_LEFT = Physics2D.Raycast(BOTLEFT, -Vector2.right, raydistance, layerMask);

		#region Collider_BottomFace
		if(HIT_TOPRIGHT_UP.collider != null)
		{
			if(HIT_TOPRIGHT_UP.transform.gameObject.GetComponent<Tile>())
			{
				if(Comparator_Execute(HIT_TOPRIGHT_UP.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{
					if(vely*Time.deltaTime > HIT_TOPRIGHT_UP.distance-0.05f)
					{
						vely = 0;
						transform.position = new Vector2(transform.position.x, HIT_TOPRIGHT_UP.point.y - (2f/3f));
					}
				}
			}
		}
		else if(HIT_TOPLEFT_UP.collider != null)
		{			
			if(HIT_TOPLEFT_UP.transform.gameObject.GetComponent<Tile>())
			{				
				if(Comparator_Execute(HIT_TOPLEFT_UP.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{					
					if(vely*Time.deltaTime > HIT_TOPLEFT_UP.distance-0.05f)
					{
						vely = 0;
						transform.position = new Vector2(transform.position.x, HIT_TOPLEFT_UP.point.y - (2f/3f));
					}
				}
			}
		}
		#endregion Collider_BottomFace

		#region Collider_LeftFace
		if(HIT_TOPRIGHT_RIGHT.collider != null)
		{			
			if(HIT_TOPRIGHT_RIGHT.transform.gameObject.GetComponent<Tile>())
			{				
				if(Comparator_Execute(HIT_TOPRIGHT_RIGHT.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{					
					if(velx*Time.deltaTime > HIT_TOPRIGHT_RIGHT.distance-0.05f)
					{
						velx = 0;
						transform.position = new Vector2(HIT_TOPRIGHT_RIGHT.point.x - (2f/3f), transform.position.y);
					}
				}
			}
		}
		else if(HIT_BOTRIGHT_RIGHT.collider != null)
		{			
			if(HIT_BOTRIGHT_RIGHT.transform.gameObject.GetComponent<Tile>())
			{	
				if(Comparator_Execute(HIT_BOTRIGHT_RIGHT.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{
					if(velx*Time.deltaTime > HIT_BOTRIGHT_RIGHT.distance-0.05f)
					{
						velx = 0;
						transform.position = new Vector2(HIT_BOTRIGHT_RIGHT.point.x - (2f/3f), transform.position.y);
					}
				}
			}
		}
		#endregion Collider_LeftFace

		#region Collider_TopFace
		if(HIT_BOTRIGHT_DOWN.collider != null)
		{		
			if(HIT_BOTRIGHT_DOWN.transform.gameObject.GetComponent<Tile>())
			{			
				if(Comparator_Execute(HIT_BOTRIGHT_DOWN.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{					
					if(-vely*Time.deltaTime > HIT_BOTRIGHT_DOWN.distance-0.05f)
					{
						vely = 0;
						transform.position = new Vector2(transform.position.x, HIT_BOTRIGHT_DOWN.point.y + (2f/3f));
					}
				}
			}
		}
		else if(HIT_BOTLEFT_DOWN.collider != null)
		{			
			if(HIT_BOTLEFT_DOWN.transform.gameObject.GetComponent<Tile>())
			{				
				if(Comparator_Execute(HIT_BOTLEFT_DOWN.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{					
					if(-vely*Time.deltaTime > HIT_BOTLEFT_DOWN.distance-0.05f)
					{
						vely = 0;
						transform.position = new Vector2(transform.position.x, HIT_BOTLEFT_DOWN.point.y + (2f/3f));
					}
				}
			}
		}
		#endregion Collider_TopFace

		#region Collider_RightFace
		if(HIT_BOTLEFT_LEFT.collider != null)
		{			
			if(HIT_BOTLEFT_LEFT.transform.gameObject.GetComponent<Tile>())
			{				
				if(Comparator_Execute(HIT_BOTLEFT_LEFT.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{					
					if(-velx*Time.deltaTime > HIT_BOTLEFT_LEFT.distance-0.05f)
					{
						velx = 0;
						transform.position = new Vector2(HIT_BOTLEFT_LEFT.point.x + (2f/3f), transform.position.y);
					}
				}
			}
		}
		else if(HIT_TOPLEFT_LEFT.collider != null)
		{			
			if(HIT_TOPLEFT_LEFT.transform.gameObject.GetComponent<Tile>())
			{	
				if(Comparator_Execute(HIT_TOPLEFT_LEFT.transform.gameObject.GetComponent<Tile>().colliderType, targetColliderType, (operation == 0) ? equalTo : notEqualTo))
				{
					if(-velx*Time.deltaTime > HIT_TOPLEFT_LEFT.distance-0.05f)
					{
						velx = 0;
						transform.position = new Vector2(HIT_TOPLEFT_LEFT.point.x + (2f/3f), transform.position.y);
					}
				}
			}
		}
		#endregion Collider_RightFace


		if(HIT_TOPLEFT_UP.point.x != 0 && HIT_TOPLEFT_UP.point.y != 0)
			Debug.DrawLine(TOPLEFT, HIT_TOPLEFT_UP.point, Color.green, 0.5f);
		if(HIT_TOPRIGHT_UP.point.x != 0 && HIT_TOPRIGHT_UP.point.y != 0)
			Debug.DrawLine(TOPRIGHT, HIT_TOPRIGHT_UP.point, Color.red, 0.5f);
		if(HIT_TOPRIGHT_RIGHT.point.x != 0 && HIT_TOPRIGHT_RIGHT.point.y != 0)
			Debug.DrawLine(TOPRIGHT, HIT_TOPRIGHT_RIGHT.point, Color.green, 0.5f);
		if(HIT_BOTRIGHT_RIGHT.point.x != 0 && HIT_BOTRIGHT_RIGHT.point.y != 0)
			Debug.DrawLine(BOTRIGHT, HIT_BOTRIGHT_RIGHT.point, Color.red, 0.5f);
		if(HIT_BOTRIGHT_DOWN.point.x != 0 && HIT_BOTRIGHT_DOWN.point.y != 0)
			Debug.DrawLine(BOTRIGHT, HIT_BOTRIGHT_DOWN.point, Color.green, 0.5f);
		if(HIT_BOTLEFT_DOWN.point.x != 0 && HIT_BOTLEFT_DOWN.point.y != 0)
			Debug.DrawLine(BOTLEFT, HIT_BOTLEFT_DOWN.point, Color.red, 0.5f);
		if(HIT_BOTLEFT_LEFT.point.x != 0 && HIT_BOTLEFT_LEFT.point.y != 0)
			Debug.DrawLine(BOTLEFT, HIT_BOTLEFT_LEFT.point, Color.green, 0.5f);
		if(HIT_TOPLEFT_LEFT.point.x != 0 && HIT_TOPLEFT_LEFT.point.y != 0)
			Debug.DrawLine(TOPLEFT, HIT_TOPLEFT_LEFT.point, Color.red, 0.5f);//*/

	}

	public void MoveCommand(string dir, float speed)
	{
		if(dir=="UP")
		{
			vely += speed;
		}
		if(dir=="DOWN")
		{
			vely -= speed;
		}
		if(dir=="LEFT")
		{
			velx -= speed;
		}
		if(dir=="RIGHT")
		{
			velx += speed;
		}

		if(velx >= speedLimit)
		{
			velx = speedLimit;
		}
		if(velx <= -speedLimit)
		{
			velx = -speedLimit;
		}
		if(vely >= speedLimit)
		{
			vely = speedLimit;
		}
		if(vely <= -speedLimit)
		{
			vely = -speedLimit;
		}
	}

	public float getDistance(Vector2 p1, Vector2 p2)
	{
		return Vector2.Distance(p1, p2);
	}

	private bool Comparator_Execute(int a, int b, Comparator c)
	{
		return c(a, b);
	}
}
