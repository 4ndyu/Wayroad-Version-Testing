using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    //public GameObject prefab;
    //public LineRenderer line = gameObject.AddComponent<LineRenderer>();
    private List<Vector3> pos = new List<Vector3>();

    public void addLine(Vector3 WayPoint1, Vector3 WayPoint2)
    {
        LineRenderer lineObject = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();

        pos.Add(WayPoint1);
        pos.Add(WayPoint2);

        lineObject.startColor = Color.red;
        lineObject.endColor = Color.red;

        lineObject.startWidth = 0.3f;
        lineObject.endWidth = 0.3f;

        lineObject.SetPositions(pos.ToArray());
    }
}
