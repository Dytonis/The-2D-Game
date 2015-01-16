using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPlacer : MonoBehaviour 
{

	public Sprite[] tileList;
	public Sprite[] worldSpawnList;
	public Color[] colorList;
	public GameObject tile;
	public GameObject room;	
	public byte DoorColorElement = 3;

	[SerializeField]
	private string LEVEL_GENERATION = "Below are level generation values!";
	[SerializeField]
	private List<Sprite> roomList = new List<Sprite>();
	[SerializeField]
	private List<GameObject> roomLayout = new List<GameObject>();
	[SerializeField]
	private List<Sprite> roomList_TopDoors = new List<Sprite>();

	// Use this for initialization
	void Start () 
	{
		Debug.Log("Level generation started!");
		System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
		stop.Start();
		populateRoomTable();
		getLevelRoomPlacement();
		beginTilePlacement();
		stop.Stop();
		Debug.Log("Level generation finished in " + (stop.Elapsed.TotalMilliseconds * 1000) + "ns");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void getLevelRoomPlacement()
	{

	}

	private void populateRoomTable()
	{
		foreach(Sprite sroom in worldSpawnList)
		{
			roomList.Add(sroom);

			Texture2D t = sroom.texture;

			float sx = sroom.rect.x;
			float sy = sroom.rect.y;

			//check for doors at the top
			for(int y = (int)sy+(int)sroom.rect.height-1; y >= (sy); y-=(int)sroom.rect.height)
			{
				for(int x = (int)sx; x <= sx+sroom.rect.width; x++)
				{
					if(checkColor(t.GetPixel(x, y), colorList[DoorColorElement], 0.2f))
					{
						Sprite newRoom = Instantiate(sroom, room.transform.position, room.transform.rotation) as Sprite;
						roomList_TopDoors.Add(newRoom);
					}
				}
			}
		}
	}

	private void beginTilePlacement()
	{
		foreach(Sprite sp in worldSpawnList)
		{
			Texture2D t = sp.texture;
			
			Debug.Log("Size of texture: " + t.width + ", " + t.height);
			
			float sx = sp.rect.x;
			float sy = sp.rect.y;
			
			for(int y = (int)sy-1; y <= sy+10; y++)
			{
				for(int x = (int)sx; x <= sx+10; x++)
				{
					int c = 0;
					foreach(Color color in colorList)
					{
						if(checkColor(t.GetPixel(x,y), color, 0.2f))
						{
							GameObject newTile = Instantiate(tile, new Vector3((float)((x-1)*1.6)-(float)(sx*1.6), (float)((y-1)*1.6)-(float)(sy*1.6), 0), tile.transform.rotation) as GameObject;
							newTile.GetComponent<SpriteRenderer>().sprite=tileList[c];
							newTile.GetComponent<Tile>().tildID = (byte)(c);
							newTile.renderer.sortingOrder = -1;
							newTile.transform.parent = room.transform;
							
							#region colliderAssign
							//we use c as the tile ID because it is equal to Tile().tileID;
							if(c == 0) //FLOOR
							{
								newTile.GetComponent<Tile>().colliderType = 0;
								newTile.gameObject.layer = 8;
								newTile.GetComponent<BoxCollider2D>().enabled = false;
							}
							if(c == 1) //WALL
							{
								newTile.GetComponent<Tile>().colliderType = 1;
								newTile.gameObject.layer = 9;
							}
							if(c == 2) //HOLE
							{
								newTile.GetComponent<Tile>().colliderType = 2;
								newTile.gameObject.layer = 9;
							}
							if(c == 3)
							{
								newTile.GetComponent<Tile>().colliderType = 0;
								newTile.gameObject.layer = 8;
								newTile.GetComponent<BoxCollider2D>().enabled = false;
							}
							#endregion
						}
						
						c++;
					}
				}
			}
		}
	}

	private bool checkColor(Color a, Color b, float margin)
	{
		if(a.r <= (float)(b.r + margin) && a.r >= (float)(b.r - margin)) //match R
		{
			//Debug.Log("a: " + a.r + ", b: " + b.r + ", mb: " + (float)(b.r - margin));
			if(a.g <= (float)(b.g + margin) && a.g >= (float)(b.g - 1/margin)) //match G
			{
				//Debug.Log("a: " + a.g + ", b: " + b.g + ", mb: " + (float)(b.r - margin));
				if(a.b <= (float)(b.b + margin) && a.b >= (float)(b.b - margin)) //match B
				{
					//Debug.Log("a: " + a.b + ", b: " + b.b + ", mb: " + (float)(b.r - margin));
					return true;
				}
			}
		}

		return false;
	}
}

public class LevelLayout
{
	public GameObject[,] LevelGrid = new GameObject[10,10];

	private List<Sprite> rooms_topDoors;

	public LevelLayout(List<Sprite> topDoorsList)
	{
		rooms_topDoors = topDoorsList;
	}

	private void getFirstRoom()
	{

	}

	public void getLayout()
	{

	}

	public Sprite getRoom(bool requireTop = false, bool requireRight = false, bool requireBot = false, bool requireLeft = false)
	{

	}

	private int randomNumber(byte min, byte max)
	{
		return Random.Range((int)min, (int)max);
	}
}

public struct RoomInfo
{
	byte x = 0;
	byte y = 0;

	public RoomInfo(byte x, byte y)
	{
		this.x = x;
		this.y = y;
	}
}
