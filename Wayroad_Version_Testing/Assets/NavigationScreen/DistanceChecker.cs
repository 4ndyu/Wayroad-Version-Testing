using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    private ARLocation.ARLocationManager arLocationManager;
    private GameObject wayObject = GameObject.Find("WayPoint");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startNavigation script = (startNavigation)wayObject.GetComponent(typeof(startNavigation));
        arLocationManager = ARLocation.ARLocationManager.Instance;
        var cameraPos = arLocationManager.MainCamera.transform.position;
        var distance = Vector3.Distance(cameraPos, transform.position);
        //GameObject.Find("Distance").GetComponentInChildren<UnityEngine.UI.Text>().text = "" + distance;

        if(distance <= 20)
        {
            script.toggleEndScreen();
        }
    }
}
