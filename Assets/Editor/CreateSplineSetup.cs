using UnityEditor;
using UnityEngine;

public class CreateSplineSetup
{
 [MenuItem("GameObject/3D Object/Create Spline Setup", false,10)]
 static void Create()
 {
 GameObject root = new GameObject("SplineRoot");
 Spline spline = root.AddComponent<Spline>();
 SplineRendererGizmo giz = root.AddComponent<SplineRendererGizmo>();
 giz.spline = spline;

 // Create four control points
 for (int i=0;i<4;i++)
 {
 GameObject cp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
 cp.name = "ControlPoint_" + i;
 cp.transform.parent = root.transform;
 cp.transform.localScale = Vector3.one *0.2f;
 cp.transform.position = new Vector3(i *1.5f, Mathf.Sin(i) *0.5f, (i%2==0) ?0f :1f);
 spline.controlPoints.Add(cp.transform);
 // remove collider
 Object.DestroyImmediate(cp.GetComponent<Collider>());
 }

 // Ball
 GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
 ball.name = "SplineBall";
 ball.transform.parent = root.transform;
 ball.transform.localScale = Vector3.one *0.3f;
 ball.AddComponent<SplineBall>().spline = spline;
 Object.DestroyImmediate(ball.GetComponent<Collider>());

 Selection.activeGameObject = root;
 }
}
