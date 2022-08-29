using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileGeneration : MonoBehaviour
{
    
    public Tilemap tilemap;
    public Sprite defaultTile;
    public Sprite rockTile;
    public Sprite treeTile;
    public Sprite m1Tile;
    //bool below to restrict amount of monsters spawned
    private bool monsterSpawned = false;
    public int gx = 10;
    public int gy = 6;
    int x = 11;
    int y = 6;
    public int globalX = 0;
    public int globalY = 0;
    public int[,] tileGrid = new int[24,13];
    public AIScript AI;

    public TileManager tManager;
    
    
    
    public class currTileset
    {
        public int[] flattenedTileSet;
       
    }
    
    string jsonTileset;
    

    
    // Start is called before the first frame update
    void Start()
    {
        newTileset();
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void newTileset()
    {
        
        while (!(x <= -12))
        {


            Vector3Int vc3 = new Vector3Int(0, 0, 0);
            Tile basicTile = new Tile();

            

            vc3.x = x;
            vc3.y = y;
            //whenever adding new stuff, be sure to also add new case conditions on line 110
            //whenever a monster tile is added, be sure to alert the AI script so it can grab the tile immediately
            int rando = Random.Range(0, 100);
            if (2 < rando && rando < 69) { basicTile.sprite = defaultTile; tileGrid[x + 12, y + 6] = 0; }
            if (70 < rando && rando <=  80) { basicTile.sprite = rockTile; tileGrid[x + 12, y + 6] = 1; }
            if (rando >= 90) { basicTile.sprite = treeTile; tileGrid[x + 12, y + 6] = 2; }
            if (rando == 1 && !monsterSpawned) { basicTile.sprite = defaultTile; tileGrid[x + 12, y + 6] = 0; monsterSpawned = true; AI.registerMonsters(x+12, y+6, -1, m1Tile); }
            if (!basicTile.sprite)
            {
                basicTile.sprite = defaultTile;
            }
            tilemap.SetTile(vc3, basicTile);

            y--;
            if (y <= -6)
            {
                x--;
                y = 6;
            }

        }


        saveTileset();
        x = 11;
        y = 6;
        monsterSpawned = false;
    }
   public void loadTileset(int globalPosX, int globalPosY)
    {
        AI.clearMonsters();
        

        string jsonTileset = tManager.getTilemap(globalPosX, globalPosY);

        currTileset current = new currTileset();
        
        try
        {
            current = JsonUtility.FromJson<currTileset>(jsonTileset);
        }
        catch
        {
            throw new System.Exception("Shit broke");
        }
        
        tileGrid = unflatten(current.flattenedTileSet);
        Vector3Int vc3 = new Vector3Int(0, 0, 0);
        Tile basicTile = new Tile();
        x = -11;
        y = -5;
        //whenever a monster tile is placed, be sure to alert the AI script so it can grab the tile immediately
        while (!(x >= 12))
        {
            switch (tileGrid[x + 12, y + 6])
            {
                case 0:
                    basicTile.sprite = defaultTile;
                    break;
                case 1:
                    basicTile.sprite = rockTile;
                    break;
                case 2:
                    basicTile.sprite = treeTile;
                    break;
                case -1:
                    AI.registerMonsters(x + 11, y + 5, -1, m1Tile);
                    basicTile.sprite = defaultTile;
                    break;

            }
            Debug.Log("lol");
            vc3.x = x;
            vc3.y = y;
            tilemap.SetTile(vc3, basicTile);
            y++;
            if (y >= 7)
            {
                y = -5;
                x++;
            }
        }
        x = 11;
        y = 6;
    }
    public void saveTileset()
    {
        currTileset current = new currTileset();
        current.flattenedTileSet = flatten(tileGrid);
        string json = JsonUtility.ToJson(current);
        Debug.Log(json);
        Debug.Log(current.flattenedTileSet.Length);
        //System.IO.File.WriteAllText("D:/Users/mayer/Simple Mechanic Scripts/Saves" + "/Tileset.json", json);
        tManager.addTilemap(globalX, globalY, json);
    }    
    public int[] flatten(int[,] tobeFlattened)
    {
        int arrayParser = 0;
        int[] flattened = new int[276];
        //lowest tile location is 1,1 and our highest grid location is 23,13
        //parse through tileset and store it in a 1d array for json storage
        //start lowest(1,1) and go through Y
        //essentially, start at lowest, then go UP, after hitting ceilling, start at x=2, y=1 then go UP, repeat until done
        for (int fx = 1; fx < 24; fx++)
        {
            for (int fy = 1; fy < 13; fy++)
            {
                flattened[arrayParser] = tobeFlattened[fx, fy];
                arrayParser++;
            }

        }
        arrayParser = 0;
        return flattened;
    }
    public int[,] unflatten(int[] toBeUnflattened)
    {
        int arrayParser = 0;
        int[,] unflattened = new int[24, 13];
        for (int fx = 1; fx < 24; fx++)
        {
            for (int fy = 1; fy < 13; fy++)
            {
                unflattened[fx, fy] = toBeUnflattened[arrayParser];
                
                arrayParser++;
               
            }
        }

        return unflattened;
    }
    //removes tile, replacing with default tile
    public void removeTile(int x, int y)
    {
        tileGrid[x, y] = 0;
        Vector3Int removedSpot = new Vector3Int(x - 12, y - 6, 0);
        Tile basicTile = new Tile();
        basicTile.sprite = defaultTile;
        
        tilemap.SetTile(removedSpot, basicTile);
        tilemap.RefreshTile(removedSpot);
        Debug.Log("Removed");
        currTileset current = new currTileset();
        current.flattenedTileSet = flatten(tileGrid);
        string json = JsonUtility.ToJson(current);
        Debug.Log(json);
        Debug.Log(current.flattenedTileSet.Length);
        tManager.overWrite(globalX, globalY, json);
    }
    //returns the ID of the tile at the given position
    public int getTile(int x, int y)
    {
        Debug.Log(tileGrid[x, y]);
        return tileGrid[x , y ];
    }
    public void replaceTile(int tileID, int locX, int locY)
    {
        Vector3Int vc3 = new Vector3Int(locX , locY , 0);
        tileGrid[locX + 12, locY + 6] = tileID;
        Tile basicTile = new Tile();
        switch (tileID)
        {
            case 0:
                basicTile.sprite = defaultTile;
                break;
            case 1:
                basicTile.sprite = rockTile;
                break;
            case 2:
                basicTile.sprite = treeTile;
                break;
            case -1:
                basicTile.sprite = defaultTile;
                break;

        }
        tilemap.SetTile(vc3, basicTile);
        
        
    }
}
