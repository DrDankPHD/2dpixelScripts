using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public GameObject player;
    public TileGeneration tg;
    public EventManager em;
    public TextBar tb;
    public AIScript ai;
    public CharacterSheetController csc;
    float roundedX;
    float roundedY;
    public int lookX;
    public int lookY;

    int trueX;
    int trueY;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float playerX()
    {
        return player.transform.position.x;
    }
    public float playerY()
    {
        return player.transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        //get the player's position-based location and determing where he is standing based on the tileGrid(2d array thing)
        roundedX = player.transform.position.x;
        roundedY = player.transform.position.y;
       if(roundedX - (int)roundedX >0.4999999999)
        {
            trueX = (int)roundedX + 1;
            trueX *= 2;
        }
       else
        {
            trueX = (int)roundedX;
            trueX *= 2;
            trueX++;
        }
       if(roundedY - (int)roundedY > 0.499999999)
        {
            trueY = (int)roundedY + 1;
            trueY *= 2;
        }
       else
        {
            trueY = (int)roundedY;
            trueY *= 2;
            trueY++;
        }
        int currTile = GameObject.Find("Tilemap").GetComponent<TileGeneration>().tileGrid[trueX, trueY];
        if(currTile > 0)
        {
            if(trueY +1 > 12)
            {
                player.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            }
            else
            if(trueY-1 <= 0)
            {
                player.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            }
            else
            if(trueX +1>23)
            {
                player.transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
            }
            else
            if (trueX -1 <= 0)
            {
                player.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            }
            else
                player.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

            return;
        }
        
        if(tb.textCheck() || csc.charSheetOpen)
        {
            return;
        }
        if (Input.GetButtonDown("openSheet"))
        {
            if (csc.charSheetOpen)
            {
                return;
            }
            else
                csc.activate();
        }
        //Debug.Log(trueX);
        //Debug.Log(trueY);
        if (Input.GetButtonDown("up"))
        {
            if (trueY + 1 > 12)
            {
                Debug.Log("Edge hit!");
                trueY = 1;
                tg.globalY++;
                ai.clearMonsters();
                tg.loadTileset(tg.globalX,tg.globalY);
                player.transform.position = new Vector3(transform.position.x, transform.position.y - 5.5f, transform.position.z);
            }
            else
            {
                int nextTile = GameObject.Find("Tilemap").GetComponent<TileGeneration>().tileGrid[trueX, trueY + 1];
                if (nextTile > 0)
                {
                    
                    if(nextTile == 1)
                    {
                        lookX = trueX;
                        lookY = trueY + 1;
                        em.alertBar(1);
                    }
                    else
                    if(nextTile == 2)
                    {
                        lookX = trueX;
                        lookY = trueY + 1;
                        em.alertBar(2);
                    }

                }
                else
                {
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                }
            }
        }
        if (Input.GetButtonDown("down"))
        {

            if (trueY - 1 <= 0)
            {
                Debug.Log("Edge hit!");
                trueY = 12;
                tg.globalY--;
                ai.clearMonsters();
                tg.loadTileset(tg.globalX, tg.globalY);
                player.transform.position = new Vector3(transform.position.x, transform.position.y + 5.5f, transform.position.z);
            }
            else
            {
                int nextTile = GameObject.Find("Tilemap").GetComponent<TileGeneration>().tileGrid[trueX, trueY - 1];
                if (nextTile > 0)
                {

                    if (nextTile == 1)
                    {
                        lookX = trueX;
                        lookY = trueY - 1;
                        em.alertBar(1);
                    }
                    else
                    if (nextTile == 2)
                    {
                        lookX = trueX;
                        lookY = trueY - 1;
                        em.alertBar(2);
                    }
                }
                else
                {
                    player.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                }
            }
        }
        if (Input.GetButtonDown("left"))
        {

            if (trueX - 1 <= 0)
            {
                Debug.Log("Edge hit!");
                trueX = 23;
                tg.globalX--;
                ai.clearMonsters();
                tg.loadTileset(tg.globalX, tg.globalY);
                player.transform.position = new Vector3(transform.position.x + 11.0f, transform.position.y, transform.position.z);
            }
            else
            {
                int nextTile = GameObject.Find("Tilemap").GetComponent<TileGeneration>().tileGrid[trueX - 1, trueY];
                if (nextTile > 0)
                {

                    if (nextTile == 1)
                    {
                        lookX = trueX - 1;
                        lookY = trueY;
                        em.alertBar(1);
                    }
                    else
                    if (nextTile == 2)
                    {
                        lookX = trueX - 1;
                        lookY = trueY;
                        em.alertBar(2);
                    }
                }
                else
                {
                    player.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                }
            }
        }
        if (Input.GetButtonDown("right"))
        {

            if (trueX + 1 > 23)
            {
                Debug.Log("Edge hit!");
                trueX = 1;
                tg.globalX++;
                ai.clearMonsters();
                tg.loadTileset(tg.globalX, tg.globalY);
                player.transform.position = new Vector3(transform.position.x - 11.0f, transform.position.y, transform.position.z);

            }
            else
            {
                int nextTile = GameObject.Find("Tilemap").GetComponent<TileGeneration>().tileGrid[trueX + 1, trueY];
                if (nextTile > 0)
                {

                    if (nextTile == 1)
                    {
                        lookX = trueX + 1;
                        lookY = trueY;
                        em.alertBar(1);
                    }
                    else
                    if (nextTile == 2)
                    {
                        lookX = trueX + 1;
                        lookY = trueY;
                        em.alertBar(2);
                    }
                }
                else
                {
                    player.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                }
            }
        }
    }
}
