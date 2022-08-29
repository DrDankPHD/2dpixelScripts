using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class CharacterSheetController : MonoBehaviour { 
    public GameObject self;
    public bool charSheetOpen = false;
    public InventoryManager im;
    public CraftingLibrary cl;
    //inventory slot to be cloned
    public GameObject inventorySlot;
    //parent of all inventory slots, each inventory slot must be put under parent to be displayed
    public GameObject inventoryParent;
    List<GameObject> copySlots = new List<GameObject>();
    public GameObject craftingPanel;
    // Start is called before the first frame update
    private GameObject selectedSlot = null;
    private List<int> craftingSetup = new List<int>();

    void Start()
    {
        self.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("openSheet"))
        {
            charSheetOpen = false;
            self.SetActive(false);
            foreach (GameObject copy in copySlots)
            {
                GameObject.Destroy(copy);
                Debug.Log("yup");
            }
            craftingPanel.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        if(charSheetOpen)
        {
            if(selectedSlot == null)
            {
                self.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.grey;
                self.transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                self.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                self.transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void activate()
    {
        self.SetActive(true);
        charSheetOpen = true;
        setUpInventory();
    }
    public void setUpInventory()
    {
        inventorySlot.SetActive(false);
        if (im.isEmpty())
        {
            return;
        }
        foreach (Vector2Int item in im.inventory)
        {
            if (item.x == 0)
            {
                break;
            }
            if(item.y != 0 )
            {
                //Debug.Log(item.x);
                GameObject copySlot = GameObject.Instantiate(inventorySlot);
                copySlot.SetActive(true);
                copySlots.Add(copySlot);
                copySlot.transform.GetChild(0).GetComponent<Text>().text = im.getItemName(item.x);
                copySlot.transform.GetChild(1).GetComponent<Text>().text = "         x" + item.y.ToString();
                copySlot.name = item.x.ToString() + "," + item.y.ToString();
                copySlot.transform.parent = inventoryParent.transform;
                copySlot.transform.SetAsLastSibling();
            }
           


        }

    }


   

    public void SlotClicked(UnityEngine.UI.Button thisbutton)

    { 
        string buttontext = thisbutton.transform.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        selectedSlot = thisbutton.transform.gameObject;
        Debug.Log(buttontext);
    }
    public void AddToCrafting()
    {
        //Debug.Log(selectedSlot.name);
        if (int.Parse(selectedSlot.name.Substring(2, 1)) < 1)
        {
            Debug.Log("Item is at 0, can't add!");
            return;
        }
        else
        {
            foreach(int i in craftingSetup)
            {
                if(i == int.Parse(selectedSlot.name.Substring(0, 1)))
                {
                    Debug.Log("Item is already in crafting!");
                    return;
                }
            }
            craftingSetup.Add(int.Parse(selectedSlot.name.Substring(0, 1)));
            
            if (string.IsNullOrEmpty(craftingPanel.transform.GetChild(0).GetComponent<Text>().text))
            {
                craftingPanel.transform.GetChild(0).GetComponent<Text>().text += im.getItemName(int.Parse(selectedSlot.name.Substring(0, 1)));
            }
            else
            {
                craftingPanel.transform.GetChild(0).GetComponent<Text>().text += "  +  " + im.getItemName(int.Parse(selectedSlot.name.Substring(0, 1)));
            }
        }




    }
    public void craft()
    {
        if (craftingSetup.Count.Equals(0)) return;
        craftingSetup.Sort();
        string craftingInput = "";
        foreach(int i in craftingSetup)
        {
            craftingInput += i.ToString();
        }
        int craftingFinal = int.Parse(craftingInput);
        Debug.Log(craftingFinal);
        int craftingOutput = cl.craft(craftingFinal);
        Debug.Log(craftingOutput);
        if (craftingOutput == -1)
        {
            Debug.Log("The crafting input has no valid output!");
            return;
        }
        else
        {
            /*tell invmanager to reduce the input items affected by 1
             * then add the output item.
            */
            foreach (char c in craftingInput)
            {
                //haha cheesing
                Debug.Log(c - '0');
                im.ReduceItem(c - '0');
            }
            im.AddItem(craftingOutput, 1);
            //now update the inventory display
            foreach (GameObject copy in copySlots) GameObject.Destroy(copy);

            setUpInventory();
            craftingPanel.transform.GetChild(0).GetComponent<Text>().text = "";
            craftingSetup.Clear();
        }

    }

}
