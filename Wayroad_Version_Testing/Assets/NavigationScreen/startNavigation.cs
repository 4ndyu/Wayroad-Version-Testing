using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startNavigation : MonoBehaviour
{
    public GameObject WayPoint;
    public GameObject NavigationScreen;
    public GameObject MenuButton;
    //public LineRender Line;
    private List<string> coords = new List<string>();
    private string startlocation;
    //private string startlocation;
    private string endlocation;

    void Start()
    {
        NavigationScreen.SetActive(false);
    }

    public void cancelNavigation()
    {
        NavigationScreen.SetActive(false);

        for(int i = 0; i < coords.Count; i++)
        {
            Destroy(GameObject.Find("Waypoint " + i + "(Clone)"));
        }

        MenuButton.SetActive(true);
        MenuButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    public void placeDestinations()
    {
        var latit = 0.0;
        var longit = 0.0;
        var destination = GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text;

        switch (destination)
        {
            case "Gym":
                latit = -32.06543923858472;
                longit = 115.83433198513916;
                // Testing Purposes
                /*latit = -31.857660698226443;
                longit = 115.94790095721805;*/
                break;
            case "Kim E Beazley Lecture Theatre":
                latit = -32.066869234389834;
                longit = 115.8352874127619;
                break;
            case "Geoffrey Bolton Library":
                latit = -32.066953334694134;
                longit = 115.83475633537392;
                break;
            case "Student Centre":
                latit = -32.06614495516071;
                longit = 115.83552740423676;
                break;
            case "Student Hub":
                latit = -32.06621120378571;
                longit = 115.83425744444472;
                break;
            case "Tavern":
                latit = -32.0653792826709;
                longit = 115.83468123349937;
                break;
        }
        ///
        startlocation = ARLocation.ARLocationProvider.Instance.CurrentLocation.longitude + ", " + ARLocation.ARLocationProvider.Instance.CurrentLocation.latitude;
        //startlocation = "115.95101413863327, -31.85901250680718";
        endlocation = longit + ", " + latit;

        Debug.Log("Location: " + startlocation + "\nDestination: " + endlocation);

        var URL = "https://api.mapbox.com/directions/v5/mapbox/walking/" + startlocation + ";" + endlocation +
            "?geometries=geojson&access_token=pk.eyJ1IjoiYnJhZHkxODAiLCJhIjoiY2t0MmJmdDhkMG5teTJwbm1yYmVwYWI3ZyJ9.jiKb9Kq44DDtB1AH4fg03g";

        WWW www = new WWW(URL);

        StartCoroutine(WaitForRequest(www));
        ///
        GameObject.Find("ConfirmationScreen").SetActive(false);
        NavigationScreen.SetActive(true);
        MenuButton.SetActive(false);
        GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text = destination;
        
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            // Seperates the URL response into two half, where the second half starts with the coordinates
            string[] split1 = www.text.Split(new string[] { "coordinates\":" }, System.StringSplitOptions.None);
            // Is further split to isolate the coordinates values
            string[] split2 = split1[1].Split(new string[] { "\"type\":" }, System.StringSplitOptions.None);
            // Splits to have each values in the array to be one coordinate
            string[] split3 = split2[0].Split(new string[] { "],[" }, System.StringSplitOptions.None);

            // Characters to remove characters that are not needed.
            char[] removeChar = { '[', ']', ',' };
            string temp;

            for (int i = 0; i < split3.Length; i++)
            {
                temp = split3[i].TrimStart(removeChar);
                temp = temp.TrimEnd(removeChar);

                coords.Add(temp);
            }

            int index = 0;

            foreach (string values in coords)
            {
                ///
                string[] tempCoords = values.Split(',');

                if(index == 0)
                {
                    string[] tempCoords2 = startlocation.Split(',');
                    placeWaypoint(System.Convert.ToDouble(tempCoords2[0]), System.Convert.ToDouble(tempCoords2[1]), "Waypoint " + index);
                    index++;

                    placeWaypoint(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0]), "Waypoint " + index);
                    //Line.addLine(GameObject.Find("Waypoint 0(Clone)").transform, GameObject.Find("Waypoint 1(Clone)").transform);
                }
                else if(index == coords.Count)
                {
                    placeWaypoint(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0]), "Waypoint " + index);
                    index++;

                    string[] tempCoords2 = endlocation.Split(',');

                    placeWaypoint(System.Convert.ToDouble(tempCoords2[0]), System.Convert.ToDouble(tempCoords2[1]), "Waypoint " + index);

                    //Line.addLine(GameObject.Find("Waypoint "+ (index - 1) + "(Clone)").transform, GameObject.Find("Waypoint " + index + "(Clone)").transform);
                }
                else
                {
                    placeWaypoint(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0]), "Waypoint " + index);
                    //Line.addLine(GameObject.Find("Waypoint " + (index - 1) + "(Clone)").transform, GameObject.Find("Waypoint " + index + "(Clone)").transform);
                }

                ///
                index++;
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    private void placeWaypoint(double latit, double longit, string name)
    {

        var loc = new ARLocation.Location(latit, longit, 0)
        {
            Latitude = latit,
            Longitude = longit,
            Altitude = 0,
            AltitudeMode = ARLocation.AltitudeMode.GroundRelative
        };

        var opts = new ARLocation.PlaceAtLocation.PlaceAtOptions()
        {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 4,
            MovementSmoothing = 0.1f,
            UseMovingAverage = false
        };

        WayPoint.name = name;

        ARLocation.PlaceAtLocation.CreatePlacedInstance(WayPoint, loc, opts, false);
    }
}
