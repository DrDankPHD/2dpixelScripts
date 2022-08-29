using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    public class Events
    {

        string[] eventset = new string[30];
        bool[] questionSet = new bool[30];
        public int[] eventFlags = new int[30];
        //use this for getting event at starting points and so on
        public string getEvent(int index)
        {
            return eventset[index];
        }
        public void setEvent(int index, string eventText)
        {
            eventset[index] = eventText;
        }
        //check if this is a question, defaults to false
        public bool getQuestion(int index)
        {
            if (questionSet[index])
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        //sets a question at said event
        public void setQuestion(int index, bool q)
        {
            questionSet[index] = q;
        }

        //checks if there is another text after current
        public bool nextStr(int index)
        {
            if (this.getEvent(index) != null)
            {
                return true;

            }
            return false;
        }
        //sets a method to be executed after text is completed
        public void setFlag(int index, int flag)
        {
            eventFlags[index] = flag;
        }
        //runs flags if one is found
        
    }
        public TileGeneration tg;
        public playercontroller pc;
        public int currentEvent;
        public int eventIndex = 0;
        public GameObject textbar;
        public TextBar eventbar;
        public InventoryManager iv;
        Dictionary<int, Events> events = new Dictionary<int, Events>();
        // Start is called before the first frame update
        void Start()
        {
            Events treeRun = new Events();
            treeRun.setEvent(0, "You ran into a tree.");
            treeRun.setEvent(1, "Use Axe on the tree?");
            treeRun.setEvent(2, "You didn't chop the tree.");
            treeRun.setEvent(4, "Tree_down.");
            treeRun.setEvent(5, "Got x1 wood!");
            treeRun.setFlag(4, 1);
            treeRun.setFlag(5, 2);
            treeRun.setQuestion(1, true);
            Events rockRun = new Events();
            rockRun.setEvent(0, "You ran into a rock");
            rockRun.setEvent(1, "Use pickaxe on the rock?");
            rockRun.setEvent(2, "You didn't break the rock.");
            rockRun.setEvent(4, "Rock_down.");
            rockRun.setEvent(5, "Got x1 rock!");
            rockRun.setQuestion(1, true);
            rockRun.setFlag(4, 1);
            rockRun.setFlag(5, 3);
            events.Add(2, treeRun);
            events.Add(1, rockRun);
        }
        public void runFlags(int index)
        {
            if (events[currentEvent].eventFlags[index] != 0)
            {
                switch (events[currentEvent].eventFlags[index])
                {
                    //break whatever the player is looking at
                    case 1:
                    Debug.Log(pc.lookX);
                    Debug.Log(pc.lookY);
                        tg.removeTile(pc.lookX, pc.lookY);

                        break;
                    //add 1 wood
                    case 2:
                    Debug.Log("Gave player 1 wood!");
                    iv.AddItem(2, 1);
                    break;
                case 3:
                    Debug.Log("Gave player 1 rock!");
                    iv.AddItem(1, 1);
                    break;
                }

            }
            else
            {
                Debug.Log("No special flags found here");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void alertBar(int eventKey)
        {
            eventIndex = 0;
            currentEvent = eventKey;
            eventbar.setText(events[eventKey].getEvent(eventIndex));
            if (events[eventKey].getQuestion(eventIndex))
            {
                eventbar.selSwitchON();
            }
            eventbar.switchON();
        }
        public string nextEvent()
        {
            eventIndex++;
        runFlags(eventIndex);
        if (events[currentEvent].nextStr(eventIndex))
            {
                if (events[currentEvent].getQuestion(eventIndex))
                {

                    eventbar.selSwitchON();
                }
                return events[currentEvent].getEvent(eventIndex);
            }
            else
            {
                throw new System.Exception("There are no events past this point");
            }

        }
        public bool nextEv()
        {
            if (events[currentEvent].nextStr(eventIndex + 1))
            {
                return true;
            }
            else
            {
                currentEvent = 0;
                eventIndex = 0;
                return false;
            }

        }
        public void Answer(bool answer)
        {
            if (answer)
            {
                eventIndex += 3;
                eventbar.setText(events[currentEvent].getEvent(eventIndex));
            runFlags(eventIndex);
            if (events[currentEvent].getQuestion(eventIndex))
                {
                    eventbar.selSwitchON();
                }

            }
            else
            {
                eventIndex += 1;
                eventbar.setText(events[currentEvent].getEvent(eventIndex));
                runFlags(eventIndex);
            }
        }
    
}
