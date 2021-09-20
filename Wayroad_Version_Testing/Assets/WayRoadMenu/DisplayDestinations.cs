using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Purpose: To read a file that contains the name of the destinations in Murdoch
 *           and display each destinations as buttons to start navigation to that
 *           destination.
 *           This Script is run in Unity.
 *           
 *  Author: Andy Lam Yu
 *  Date:   28/08/2021
 *  Version:    1
 */
public class DisplayDestinations : MonoBehaviour
{
    // A template for each item that going to be displayed in the scroll view
    public GameObject itemTemplate1;
    // A second item template to have alternation colours of buttons
    public GameObject itemTemplate2;
    // A template to spawn the sub title that going to be displayed in the scroll view
    public GameObject subTitleTemplate;
    // The location where the items are going be placed
    public GameObject content;

    public string dataFile = "destinations";

    // Start is called before the first frame update
    void Start()
    {
        TextAsset fileAsset = Resources.Load(dataFile) as TextAsset;
        
        string fileContent = fileAsset.text;
        string[] fileLines = fileContent.Split('\n');

        foreach (string line in fileLines)
        {
            string[] locations = line.Split(',');

            var index = 0;

            foreach(string place in locations)
            {
                GameObject temp;

                if(index == 0)
                {
                    // Change the name of the button seen on the Unity Interface
                    subTitleTemplate.name = place;
                    temp = Instantiate(subTitleTemplate);
                }
                else
                {
                    if(index % 2 == 0)
                    {
                        itemTemplate2.name = place;
                        temp = Instantiate(itemTemplate2);
                    }
                    else
                    {
                        itemTemplate1.name = place;
                        temp = Instantiate(itemTemplate1);
                    }
                    
                }
                
                temp.GetComponentInChildren<UnityEngine.UI.Text>().text = place;
                //temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.parent = content.transform;
                GameObject.Find(place+"(Clone)").transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

                index++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
