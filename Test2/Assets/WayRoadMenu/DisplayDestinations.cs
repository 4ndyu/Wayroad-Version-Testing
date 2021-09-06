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
    public GameObject itemTemplate;
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

            foreach(string place in locations)
            {
                itemTemplate.name = place;
                var temp = Instantiate(itemTemplate);
                temp.GetComponentInChildren<UnityEngine.UI.Text>().text = place;
                temp.transform.parent = content.transform;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
