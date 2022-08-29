using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    public struct Monster
    {
       public int LocX;
       public int LocY;
       public int monsterID;
       public bool flyer;
       public GameObject obj;
        public float posX;
        public float posY;
        
        public Monster(int LocX, int LocY, int monsterID, bool flyer)
        {
            this.LocX = LocX;
            this.LocY = LocY;
            
            this.monsterID = monsterID;
            this.flyer = flyer;
            this.obj = new GameObject();
            //the posX and posY is for the position on the physical grid, the locX and locY is for the position on the tileset
            this.posX = (LocX-1)/2+0.25f;
            this.posY = (LocY-1)/2+0.25f;
        }
        public bool isFlyer()
        {
            return this.flyer;
        }
        public void addX()
        {
            
            this.posX += 0.5f;
        }
        public void addY()
        {
            
            this.posY += 0.5f;
        }
        public void subX()
        {
            
            this.posX-=0.5f;
           
        }
        public void subY()
        {
           
            this.posY -= 0.5f;
        }
        
    }
    public List<Monster> monsters = new List<Monster>();
    public playercontroller PC;
    public TileGeneration TG;
    float timer = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer < 0)
        {
            AImovement();
            timer = 1;
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
        
    }

    //takes a generated monster from tilemanager and registers it for AI manipulation
    public void registerMonsters(int locX, int locY, int monsterID, Sprite sprite)
    {
        

        switch(monsterID)
        {


            case 0:
                break;

            case -1:
                monsters.Add(new Monster(locX, locY, monsterID, true));
                monsters[monsters.Count - 1].obj.transform.position = new Vector3(monsters[monsters.Count - 1].posX, monsters[monsters.Count - 1].posY, -1);
                monsters[monsters.Count - 1].obj.AddComponent<SpriteRenderer>();
                monsters[monsters.Count - 1].obj.GetComponent<SpriteRenderer>().sprite = sprite;
                Debug.Log("Monster registered!");
                break;
        }

    }

    public void clearMonsters()
    {
        foreach(Monster m in monsters)
        {
            //TG.replaceTile(-1, (int)m.obj.transform.position.x, (int)m.obj.transform.position.y);
           //TG.removeTile( m.LocX, m.LocY);
            Debug.Log("plreaced");
            Object.Destroy(m.obj);
        }
        monsters.Clear();
        
    }


    void AImovement()
    {
        Debug.Log("movement AI on");
        for(int i = 0; i < monsters.Count; i++)
        {
            //update trueX trueY
           
            //get player location
            float goalX = Mathf.Round(PC.playerX() * 100.0f) * 0.01f; 
            float goalY = Mathf.Round(PC.playerY() * 100.0f) * 0.01f;
            //next, find the next square that will get us as close as possible to the player that is adjacent to our monster's square
            //more specifically: find the 8 squares surrounding the monster, pick the one that brings the monster closest to the player, and put the monster there
            //if the square is blocked, choose the square clockwise of it, repeat until square that is empty is found
            /*
             * 1-2-3-
             * 4-M-6
             * 7-8-9
             * M is current location or 5
             * 
             */
            //Debug.Log("trying my best!");
            Debug.Log(monsters[i].obj.transform.position.x - goalX);
            Debug.Log(monsters[i].obj.transform.position.y - goalY);
            if (monsters[i].obj.transform.position.x - goalX < 0)
            {
                monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x + 0.5f, monsters[i].obj.transform.position.y, -1);
                if (monsters[i].obj.transform.position.y - goalY < 0)
                {
                    Debug.Log(3);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y+ 0.5f, -1);
                }
                else if(monsters[i].obj.transform.position.y -goalY > 0)
                {
                    Debug.Log(9);
                   
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y- 0.5f, -1);
                }
                else
                {
                    Debug.Log(6);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y, -1);
                }
            }
            else if(monsters[i].obj.transform.position.x - goalX > 0)
            {
                Debug.Log("hi");
                monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x - 0.5f, monsters[i].obj.transform.position.y, -1);
                
                if (monsters[i].obj.transform.position.y - goalY < 0)
                {
                    Debug.Log(1);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y + 0.5f, -1);
                }
                else if (monsters[i].obj.transform.position.y - goalY > 0)
                {
                    Debug.Log(7);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y- 0.5f, -1);
                }
                else
                {
                    Debug.Log(4);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y, -1);
                }
            }
            else
            {
                if (monsters[i].obj.transform.position.y -goalY < 0)
                {
                    Debug.Log(2);
                    
                    monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y+ 0.5f, -1);
                }
                else if (monsters[i].obj.transform.position.y - goalY > 0)
                {
                    
                     monsters[i].obj.transform.position = new Vector3(monsters[i].obj.transform.position.x, monsters[i].obj.transform.position.y- 0.5f, -1);
                    Debug.Log(8);
                }
                else
                {
                    Debug.Log("combat");
                }
            }
            

        }
    }
}
