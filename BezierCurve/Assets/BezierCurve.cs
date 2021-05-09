using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierCurve : MonoBehaviour
{
    public GameObject obj;

    [Range(0, 1)]
    public float value;

    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;


    private void Update()
    {
        obj.transform.position = Bezier(value);

    }

    public Vector3 Bezier(float _value)
    {
        Vector3 ptp1 = p1.position + (p2.position - p1.position) * _value;
        Debug.Log(ptp1);
        Vector3 ptp2 = p2.position + (p3.position - p2.position) * _value;
        Debug.Log(ptp2);
        Vector3 ptp3 = p3.position + (p4.position - p3.position) * _value;
        Debug.Log(ptp3);

        Vector3 ptp4 = ptp1 + (ptp2 - ptp1) * _value;
        Debug.Log(ptp4);
        Vector3 ptp5 = ptp2 + (ptp3 - ptp2) * _value;
        Debug.Log(ptp5);

        return ptp4 + (ptp5 - ptp4) * _value;
    }
}

[CanEditMultipleObjects]
[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEdit : Editor
{
    private void OnSceneGUI()
    {
        BezierCurve generator = (BezierCurve)target;

        generator.p1.position = Handles.PositionHandle(generator.p1.position, Quaternion.identity);
        generator.p2.position = Handles.PositionHandle(generator.p2.position, Quaternion.identity);
        generator.p3.position = Handles.PositionHandle(generator.p3.position, Quaternion.identity);
        generator.p4.position = Handles.PositionHandle(generator.p4.position, Quaternion.identity);

        Handles.DrawLine(generator.p1.position, generator.p2.position);
        Handles.DrawLine(generator.p2.position, generator.p3.position);
        Handles.DrawLine(generator.p3.position, generator.p4.position);

        float count = 0.1f;
        for (float i = 0; i < 1; i += count)
        {
            Handles.DrawLine(generator.Bezier(i), generator.Bezier(i+ count));
        }
    }
}
