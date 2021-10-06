using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startNavigation : MonoBehaviour
{
    public GameObject WayPoint;
    public GameObject Cookies;
    public GameObject NavigationScreen;
    public GameObject MenuButton;
    private string startlocation;
    private string endlocation;
    private List<ARLocation.Location> coords = new List<ARLocation.Location>();
    private int numberOfWaypoints = 0;

    void Start()
    {
        NavigationScreen.SetActive(false);
    }

    public void cancelNavigation()
    {
        NavigationScreen.SetActive(false);

        for(int i = 0; i < numberOfWaypoints; i++)
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
        //startlocation = "115.83498857938919, -32.06757678844314";
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
            string[] tempCoords;
            coords = new List<ARLocation.Location>();
            tempCoords = startlocation.Split(',');
            coords.Add(new ARLocation.Location(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0])));

            for (int i = 0; i < split3.Length; i++)
            {
                temp = split3[i].TrimStart(removeChar);
                temp = temp.TrimEnd(removeChar);
                tempCoords = temp.Split(',');

                coords.Add(new ARLocation.Location(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0])));
            }

            tempCoords = endlocation.Split(',');
            coords.Add(new ARLocation.Location(System.Convert.ToDouble(tempCoords[1]), System.Convert.ToDouble(tempCoords[0])));

            //int index = 0;
            double differLat;
            double differLong;
            double biggestDiffer;
            double numberOfCookies;
            double modifier = 0.00002;
            double distanceModifierLat;
            double distanceModifierLong;
            double tempLat;
            double tempLong;
            ARLocation.Location tempLoc;

            numberOfWaypoints = 0;

            for (int i = 0; i < coords.Count; i++)
            {
                if (i > 0)
                {
                    differLat = coords[i - 1].Latitude - coords[i].Latitude;
                    differLong = coords[i - 1].Longitude - coords[i].Longitude;

                    if (differLat > differLong)
                        biggestDiffer = differLat;
                    else
                        biggestDiffer = differLong;

                    numberOfCookies = biggestDiffer / modifier;

                    distanceModifierLat = differLat / numberOfCookies;
                    distanceModifierLong = differLong / numberOfCookies;

                    tempLat = coords[i].Latitude;
                    tempLong = coords[i].Longitude;

                    for (int ii = 0; ii < numberOfCookies; ii++)
                    {
                        tempLat += distanceModifierLat;
                        tempLong += distanceModifierLong;
                        tempLoc = new ARLocation.Location(tempLat, tempLong);
                        
                        placeCookies(tempLoc, "Waypoint " + numberOfWaypoints);
                        numberOfWaypoints++;
                    }

                    placeWaypoint(coords[i], "Waypoint " + numberOfWaypoints);
                }

                numberOfWaypoints++;
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    private void placeWaypoint(ARLocation.Location loc, string name)
    {
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

    private void placeCookies(ARLocation.Location loc, string name)
    {
        var opts = new ARLocation.PlaceAtLocation.PlaceAtOptions()
        {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 4,
            MovementSmoothing = 0.1f,
            UseMovingAverage = false
        };

        Cookies.name = name;

        ARLocation.PlaceAtLocation.CreatePlacedInstance(Cookies, loc, opts, false);
    }
}
