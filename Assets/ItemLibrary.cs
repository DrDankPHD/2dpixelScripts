using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// create a dictionary that just stores ID:ItemName Pairs
/// </summary>
public class ItemLibrary : MonoBehaviour
{


    public Dictionary<int, string> itemLibrary = new Dictionary<int, string>();
    // Start is called before the first frame update
    //initialize fill itemLibrary with items
    void Start()
    {
        itemLibrary.Add(1, "rock");
        itemLibrary.Add(2, "wood");
        itemLibrary.Add(3, "building material");
    }

    //return item name when called
    public string getItemNameFromID(int id)
    {
        string value = "";
        if(itemLibrary.TryGetValue(id, out value))
        {
            value = itemLibrary[id];
            return value;
        }
        else
        {
            Debug.LogError(string.Format("Item ID: {0} does not exist in ItemLibrary!", id));
            return null;
        }
    }
    
}
