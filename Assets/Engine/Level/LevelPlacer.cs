using UnityEngine;
using System.Collections;

public class LevelPlacer : MonoBehaviour 
{

	public Sprite[] tileList;
	public Sprite[] worldSpawnList;
	public Color[] colorList;

	public GameObject tile;


	// Use this for initialization
	void Start () 
	{
		foreach(Sprite sp in worldSpawnList)
		{
			Texture2D t = sp.texture;

			Debug.Log("Size of texture: " + t.width + ", " + t.height);

			float sx = sp.rect.x;
			float sy = sp.rect.y;

			for(int y = (int)sy; y <= sy+15; y++)
			{
				for(int x = (int)sx; x <= sx+15; x++)
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

							#region colliderAssign
							//we use c as the tile ID because it is equal to Tile().tileID;
							if(c == 0)
							{
								newTile.GetComponent<Tile>().colliderType = 0;
								newTile.gameObject.layer = 8;
								newTile.GetComponent<BoxCollider2D>().enabled = false;
							}
							if(c == 1)
							{
								newTile.GetComponent<Tile>().colliderType = 1;
								newTile.gameObject.layer = 9;
							}
							if(c == 2)
								newTile.GetComponent<Tile>().colliderType = 2;
							#endregion
						}

						c++;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
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
