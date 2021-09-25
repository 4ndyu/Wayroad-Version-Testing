using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable MemberCanBePrivate.Global

namespace ARLocation
{
    public class placeWaypoint : MonoBehaviour
    {
        //public PlaceAtLocation.PlaceAtOptions PlacementOptions;
        public GameObject Prefab;
        public GameObject Prefab2;
        public GameObject text;
        public GameObject text2;
        private readonly List<Location> locations = new List<Location>();
        private readonly List<GameObject> instances = new List<GameObject>();

        public void AddLocation()
        {
            //ARLocationProvider locationProvider = ARLocationProvider.Instance;

            /*if(String.Equals(text, "New Text"))
            {
                text.GetComponent<UnityEngine.UI.Text>().text = ARLocationProvider.Instance.CurrentLocation.latitude + "\n" + ARLocationProvider.Instance.CurrentLocation.longitude;
            }
            else
                text2.GetComponent<UnityEngine.UI.Text>().text = ARLocationProvider.Instance.CurrentLocation.latitude + "\n" + ARLocationProvider.Instance.CurrentLocation.longitude;*/

            var loc = new Location()
            {
                Latitude = ARLocationProvider.Instance.CurrentLocation.latitude,
                Longitude = ARLocationProvider.Instance.CurrentLocation.longitude,
                Altitude = 0,
                AltitudeMode = AltitudeMode.GroundRelative
            };

            var opts = new PlaceAtLocation.PlaceAtOptions()
            {
                HideObjectUntilItIsPlaced = true,
                MaxNumberOfLocationUpdates = 4,
                MovementSmoothing = 0.1f,
                UseMovingAverage = false
            };

            //PlaceAtLocation.CreatePlacedInstance(Prefab, loc, opts);

            /*var loc2 = new Location()
            {
                Latitude = -31.858988706632427,
                Longitude = 115.95100135305562,
                Altitude = 0,
                AltitudeMode = AltitudeMode.GroundRelative
            };

            PlaceAtLocation.CreatePlacedInstance(Prefab2, loc2, opts);
            */
            var instance = PlaceAtLocation.CreatePlacedInstance(Prefab, loc, opts);

            instance.name = $"{Prefab.name} - {locations.Count}";

            locations.Add(loc);
            instances.Add(instance);
        }
    }
}