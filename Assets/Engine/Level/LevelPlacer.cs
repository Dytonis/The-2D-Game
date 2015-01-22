﻿using UnityEngine;
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
    private string
        LEVEL_GENERATION = "Below are level generation values!";
    [SerializeField]
    private List<GameObject>
        roomLayout = new List<GameObject>();
    
    public int WorldWidth = 3;
    public int WorldHeight = 3;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Level generation started!");
        System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
        stop.Start();
        getLevelRoomPlacement();
        isCorrectPlacements(1);
        stop.Stop();
        Debug.Log("Level generation finished in " + (stop.Elapsed.TotalMilliseconds * 1000) + "ns");
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    private void isCorrectPlacements(int maxIterations)
    {
        foreach(GameObject room in roomLayout)
        {
            Collider2D HIT_LEFT = Physics2D.OverlapCircle(new Vector2(room.transform.position.x-16,room.transform.position.y), 1f); //raycast left

            if(HIT_LEFT != null) //check directly left
            {
                //cool theres a room to the left
                Debug.Log("wall detected: " + HIT_LEFT.collider.gameObject);
            }
            else
            {
                room.GetComponent<Room>().DoorTiles[1].gameObject.GetComponent<SpriteRenderer>().sprite = tileList[1]; //dorTiles[1] is the left door, tileList[1] is a wall tile.  
            }
        }
    }

    private void getLevelRoomPlacement()
    {   
        int width = WorldWidth;
        int height = WorldHeight;
    
        int[,] grid = LevelGenerator.getWorld(worldSpawnList.Length, width, height);
        
        for (int y = 0; y < height; y++)
        {   
            for (int x = 0; x < width; x++)
            { 
                GameObject newRoom = Instantiate(room, new Vector3(x * 16, y * 16), room.transform.rotation) as GameObject;
                roomLayout.Add(newRoom);

                if(Random.Range(1,3) == 1)
                {
                    beginTilePlacement(worldSpawnList [grid [x, y]], newRoom);
                    newRoom.GetComponent<Room>().DoorPlacements[0] = true;
                    newRoom.GetComponent<Room>().DoorPlacements[1] = true;
                    newRoom.GetComponent<Room>().DoorPlacements[2] = true;
                    newRoom.GetComponent<Room>().DoorPlacements[3] = true;
                }
                else
                {
                    newRoom.GetComponent<Room>().DoorPlacements[0] = false;
                    newRoom.GetComponent<Room>().DoorPlacements[1] = false;
                    newRoom.GetComponent<Room>().DoorPlacements[2] = false;
                    newRoom.GetComponent<Room>().DoorPlacements[3] = false;

                }
            }   
        }
    }
    
    private void beginTilePlacement(Sprite sp, GameObject roomParent)
    {
        
        Texture2D t = sp.texture;
            
        Debug.Log("Size of texture: " + t.width + ", " + t.height);
            
        float sx = sp.rect.x;
        float sy = sp.rect.y;
        int doorCount = 0;    

        for (int y = (int)sy-1; y <= sy+10; y++)
        {
            for (int x = (int)sx; x <= sx+10; x++)
            {
                int c = 0;
                foreach (Color color in colorList)
                {
                    if (checkColor(t.GetPixel(x, y), color, 0.1f))
                    {
                        GameObject newTile = Instantiate(tile, new Vector3((float)((x - 1) * 1.6) - (float)(sx * 1.6), (float)((y - 1) * 1.6) - (float)(sy * 1.6), 0), tile.transform.rotation) as GameObject;
                        newTile.GetComponent<SpriteRenderer>().sprite = tileList [c];
                        newTile.GetComponent<Tile>().tildID = (byte)(c);
                        newTile.renderer.sortingOrder = -1;
                        newTile.transform.parent = roomParent.transform;
                        float oldX = newTile.transform.position.x;
                        float oldY = newTile.transform.position.y;
                        newTile.transform.position = new Vector3((float)oldX + (float)roomParent.transform.position.x, (float)oldY + (float)roomParent.transform.position.y, 1f);
                            
                        #region colliderAssign
                        //we use c as the tile ID because it is equal to Tile().tileID;
                        if (c == 0)
                        { //FLOOR
                            newTile.GetComponent<Tile>().colliderType = 0;
                            newTile.gameObject.layer = 8;
                            newTile.GetComponent<BoxCollider2D>().enabled = false;
                        }
                        if (c == 1)
                        { //WALL
                            newTile.GetComponent<Tile>().colliderType = 1;
                            newTile.gameObject.layer = 9;
                        }
                        if (c == 2)
                        { //HOLE
                            newTile.GetComponent<Tile>().colliderType = 2;
                            newTile.gameObject.layer = 9;
                        }
                        if (c == 3)
                        { //DOOR
                            roomParent.GetComponent<Room>().DoorTiles[doorCount] = newTile;
                            newTile.GetComponent<Tile>().colliderType = 0;
                            newTile.gameObject.layer = 8;
                            newTile.GetComponent<BoxCollider2D>().enabled = false;
                            doorCount++;
                        }
                        #endregion
                    }
                        
                    c++;
                }
            }
        }
        
    }

    private bool checkColor(Color a, Color b, float margin)
    {
        if (a.r <= (float)(b.r + margin) && a.r >= (float)(b.r - margin))
        { //match R
            //Debug.Log("a: " + a.r + ", b: " + b.r + ", mb: " + (float)(b.r - margin));
            if (a.g <= (float)(b.g + margin) && a.g >= (float)(b.g - 1 / margin))
            { //match G
                //Debug.Log("a: " + a.g + ", b: " + b.g + ", mb: " + (float)(b.r - margin));
                if (a.b <= (float)(b.b + margin) && a.b >= (float)(b.b - margin))
                { //match B
                    //Debug.Log("a: " + a.b + ", b: " + b.b + ", mb: " + (float)(b.r - margin));
                    return true;
                }
            }
        }
        return false;
    }
}

public class LevelGenerator
{
    private static int x;
    private static int y;
    private static int[,] levelList;
    
    public static int[,] getWorld(int roomsSize, int levelWidth, int levelHeight) //returns array [x,y] of world file's ID (100% fill)
    {
        x = levelWidth;
        y = levelHeight;
        
        levelList = new int[x, y];
    
        for (int w = 0; w < y; w++)
        {
            for (int z = 0; z < x; z++)
            {
                levelList [z, w] = getRandomNumber(0, roomsSize);   
            }
        }
        
        return levelList;
    }
    
    private static int getRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}