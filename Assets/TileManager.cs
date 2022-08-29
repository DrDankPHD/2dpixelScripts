using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
    
{
    public TileGeneration tg;
    [SerializeField]
    Dictionary<Vector2Int, string> jsonTilemaps = new Dictionary<Vector2Int, string>();



    public void addTilemap(int x, int y, string json)
    {
        Vector2Int tileGlobalPos = new Vector2Int(x, y);
        try
        {
            jsonTilemaps.Add(tileGlobalPos, json);
        }
        catch
        {
            Debug.LogError("A tilemap exists at that location or a json was not provided!");
        }
    }

    public string getTilemap(int x, int y)
    {   
        string jsonTileset = "";
        string value = "";
        Vector2Int tileGlobalPos = new Vector2Int(x, y);
        if(jsonTilemaps.TryGetValue(tileGlobalPos, out value))
        {
            jsonTileset = jsonTilemaps[tileGlobalPos];
        }
        if(string.IsNullOrEmpty(jsonTileset))
        {
            Debug.Log(string.Format("No tileset found at pair: {0}, {1}. Creating new tileset.", x, y));
            tg.newTileset();
        }
        tileGlobalPos = new Vector2Int(x, y);
        jsonTileset = jsonTilemaps[tileGlobalPos];
        return jsonTileset;
    }
    //removes a special tile, replacing it with defaulttile
    public void removeTile(int x, int y)
    {
        tg.removeTile(x, y);

    }
    public void overWrite(int x, int y, string newjson)
    {
        Vector2Int tileGlobalPos = new Vector2Int(x, y);
        jsonTilemaps.Remove(tileGlobalPos);
        jsonTilemaps.Add(tileGlobalPos, newjson);
    }
}


