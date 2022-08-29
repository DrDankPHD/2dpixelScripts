using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBar : MonoBehaviour
{
    public GameObject panel;
    public GameObject txtbar;
    public GameObject ynPanel;
    public GameObject ynTxt;
    public GameObject ynImage;
    public EventManager em;
    
    private bool textOn;
    public bool optionSelector = true;
    private bool selectorOn;
    
    // Start is called before the first frame update
    void Start()
    {
        
        panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txtbar.GetComponent<Text>().color = new Color(1, 1, 1, 0);
        textOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interactions"))
        {
            if(selectorOn)
            {
                Debug.Log(optionSelector);
            }
            if(textOn)
            {
                if(selectorOn)
                {
                    selectorOn = false;
                    em.Answer(optionSelector);
                    if(optionSelector)
                    {
                        ynImage.transform.localPosition = new Vector3(ynImage.transform.localPosition.x, ynImage.transform.localPosition.y - 50, ynImage.transform.localPosition.z);
                        optionSelector = false;
                    }

                    optionSelector = false;

                    return;
                }
                if(em.nextEv())
                {
                    txtbar.GetComponent<Text>().text = em.nextEvent();
                }
                else
                {
                    textOn = false;
                }
            }
            
        }
        if (selectorOn)
        {
            ynPanel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            ynTxt.GetComponent<Text>().color = new Color(1, 1, 1, 1);
            ynImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            ynPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            ynTxt.GetComponent<Text>().color = new Color(1, 1, 1, 0);
            ynImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        if (textOn)
        {
            panel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            txtbar.GetComponent<Text>().color = new Color(1, 1, 1, 1);

        }
        else
        {
            panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            txtbar.GetComponent<Text>().color = new Color(1, 1, 1, 0);
        }
        if(selectorOn)
        {
            
            if (Input.GetButtonUp("up"))
            {
                //12
                optionSelector = !optionSelector;
                if (optionSelector)
                {
                    ynImage.transform.localPosition = new Vector3(ynImage.transform.localPosition.x, ynImage.transform.localPosition.y + 50, ynImage.transform.localPosition.z);
                }
                else
                {

                    ynImage.transform.localPosition = new Vector3(ynImage.transform.localPosition.x, ynImage.transform.localPosition.y - 50, ynImage.transform.localPosition.z);
                }

            }
            if(Input.GetButtonUp("down"))
            {
                //-16
                optionSelector = !optionSelector;
                if (optionSelector)
                {
                    ynImage.transform.localPosition = new Vector3(ynImage.transform.localPosition.x, ynImage.transform.localPosition.y + 50, ynImage.transform.localPosition.z);
                }
                else
                {

                    ynImage.transform.localPosition = new Vector3(ynImage.transform.localPosition.x, ynImage.transform.localPosition.y -50, ynImage.transform.localPosition.z);
                }
            }
           
        }
    }
   public void setText(string text)
    {
        txtbar.GetComponent<Text>().text = text;
    }
    public void switchON()
    {
        textOn = true;
    }
    public void switchOFF()
    {
        textOn = false;
    }
    public void selSwitchON()
    {
        selectorOn = true;
    }
    public void selSwitchOFF()
    {
        selectorOn = false;
    }
    public bool textCheck()
    {
        return textOn;
    }
}
