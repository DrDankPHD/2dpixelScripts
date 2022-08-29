using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingLibrary : MonoBehaviour
{
    public Dictionary<int, int> craftingLibrary = new Dictionary<int, int>();
    // Start is called before the first frame update
    //initialize fill CraftingLibrary with recipies
    void Start()
    {
        craftingLibrary.Add(12, 3);

    }

    //when crafting, send the array of crafting materials sorted numerically and merged into one int, then check that int as a key
    //if successful, return the item, if false, throw an error
    public int craft(int id)
    {
        int value;
        if (craftingLibrary.TryGetValue(id, out value))
        {
            value = craftingLibrary[id];
            return value;
        }
        else
        {
            
            Debug.LogError(string.Format("attempted recipie using: {0} does not exist in craftingLibrary!", id));
            return -1;
        }
    }
}

