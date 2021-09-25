using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startNavigation : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject WayPoint;
    
    public void placeDestinations()
    {
        var latit = 0.0;
        var longit = 0.0;
        var destination = GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text;

        switch(destination)
        {
            case "Gym":
                latit = -31.859018475327563;
                longit = 115.95126176507921;
                GameObject.Find("ConfirmationScreen").SetActive(false);
                break;
        }

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

        var instance = Instantiate(WayPoint, Prefab.gameObject.transform);
        ARLocation.PlaceAtLocation.AddPlaceAtComponent(instance, loc, opts);
    }
}
