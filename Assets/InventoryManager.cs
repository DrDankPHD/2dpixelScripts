using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages player inventory
/// Inventory is an array of 2d vectors, the 'x' being the item ID, and 'y' being the amount you have
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public ItemLibrary itemLibrary;
    public int inventorySize = 10;
    public Vector2Int[] inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Vector2Int[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if (inventory[0].y == 0)
            {
                Debug.Log("Your inventory is empty!");
            }
            else
            {
                if(inventory[0].y > 1)
                Debug.Log(string.Format("You have {0} {1}s in your inventory!",inventory[0].y, itemLibrary.getItemNameFromID(inventory[0].x)));
                else
                    Debug.Log(string.Format("You have {0} {1} in your inventory!", inventory[0].y, itemLibrary.getItemNameFromID(inventory[0].x)));
            }
        }
    }
   

    public void AddItem(int item, int amount)
    {
        Vector2Int newItem = new Vector2Int(item, amount);
        int index = 0;
        //check if item is already in inventory
        foreach(Vector2Int v2i in inventory)
        {
            //if it is, add amount to the y
            if(v2i.x == item)
            {
                inventory[index].y += amount;
                return;
            }
            else if(v2i.x == 0)
            {
                //check if current v2i is empty, if so, add item there and end.
                
                inventory[index] = newItem;
                return;
            }
            else
            {
                index++;

            }
        }
        
       

    }
    public void ReduceItem(int item)
    {
        int index = 0;
        foreach (Vector2Int v2i in inventory)
        {
            if(v2i.x == item)
            {
                inventory[index].y--;
                return;
            }
            else if (v2i.x == 0)
            {
               
                throw new System.Exception("Ayo someone tried reducing/removing an item that doesn't exist? Call the devs NOW!");
                
            }
            else
            {
                index++;
            }
        }
    }
    public string getItemName(int i)
    {
        return itemLibrary.getItemNameFromID(i);
    }    
    
    public bool isEmpty()
    {
        if (inventory[0].x == 0)
        {
            return true;
        }
        else return false;
    }
    
    
}
