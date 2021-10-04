using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    public LineRenderer LineRenderer;
    private int index = 0;

    public void addLine(Transform WayPoint1, Transform WayPoint2)
    {
        LineRenderer.startColor = Color.red;
        LineRenderer.endColor = Color.red;

        LineRenderer.startWidth = 0.3f;
        LineRenderer.endWidth = 0.3f;

        LineRenderer.SetPosition(0, WayPoint1.position);
        index++;
        LineRenderer.SetPosition(index, WayPoint2.position);
        index++;
    }
}
