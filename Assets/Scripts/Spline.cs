using UnityEngine;
using System.Collections.Generic;

// Simple Catmull-Rom spline component. Assign control points (Transforms) as children or separate objects.
public class Spline : MonoBehaviour
{
 public List<Transform> controlPoints = new List<Transform>();
 public bool loop = false;

 // Returns a position on the spline for t in [0,1]
 public Vector3 GetPoint(float t)
 {
 int count = controlPoints.Count;
 if (count ==0) return transform.position;
 if (count ==1) return controlPoints[0].position;

 if (loop)
 {
 float total = count;
 float p = Mathf.Clamp01(t) * total;
 int intPoint = Mathf.FloorToInt(p);
 float weight = p - intPoint;
 int p0 = (intPoint -1 + count) % count;
 int p1 = intPoint % count;
 int p2 = (intPoint +1) % count;
 int p3 = (intPoint +2) % count;
 return CatmullRom(controlPoints[p0].position, controlPoints[p1].position, controlPoints[p2].position, controlPoints[p3].position, weight);
 }
 else
 {
 float total = Mathf.Max(1, count -1);
 float p = Mathf.Clamp01(t) * total;
 int intPoint = Mathf.FloorToInt(p);
 if (intPoint >= total) intPoint = Mathf.FloorToInt(total -1e-4f);
 float weight = p - intPoint;
 int p0 = Mathf.Max(intPoint -1,0);
 int p1 = intPoint;
 int p2 = Mathf.Min(intPoint +1, count -1);
 int p3 = Mathf.Min(intPoint +2, count -1);
 return CatmullRom(controlPoints[p0].position, controlPoints[p1].position, controlPoints[p2].position, controlPoints[p3].position, weight);
 }
 }

 // Numerical tangent by sampling nearby points
 public Vector3 GetTangent(float t)
 {
 float delta =0.001f;
 float a = Mathf.Clamp01(t - delta);
 float b = Mathf.Clamp01(t + delta);
 return (GetPoint(b) - GetPoint(a)).normalized;
 }

 Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
 {
 // Standard Catmull-Rom spline
 return 0.5f * ((2f * p1) + (-p0 + p2) * t + (2f * p0 -5f * p1 +4f * p2 - p3) * t * t + (-p0 +3f * p1 -3f * p2 + p3) * t * t * t);
 }
}
