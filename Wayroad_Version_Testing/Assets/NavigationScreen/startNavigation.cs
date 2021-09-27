using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startNavigation : MonoBehaviour
{
    public GameObject WayPoint;
    public GameObject NavigationScreen;
    public GameObject MenuButton;

    void Start()
    {
        NavigationScreen.SetActive(false);
    }

    public void cancelNavigation()
    {
        NavigationScreen.SetActive(false);

        Destroy(GameObject.Find("Arrow(Clone)"));

        MenuButton.SetActive(true);
        MenuButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    public void placeDestinations()
    {
        var latit = 0.0;
        var longit = 0.0;
        var destination = GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text;

        switch(destination)
        {
            case "Gym":
                latit = -32.06543923858472;
                longit = 115.83433198513916;
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

        GameObject.Find("ConfirmationScreen").SetActive(false);
        NavigationScreen.SetActive(true);
        MenuButton.SetActive(false);
        GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text = destination;

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

        ARLocation.PlaceAtLocation.CreatePlacedInstance(WayPoint, loc, opts, false);
    }
}
