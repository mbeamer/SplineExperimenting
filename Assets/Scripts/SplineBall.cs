using UnityEngine;

// Moves a ball along the spline. Attach to a sphere and set the spline reference.
public class SplineBall : MonoBehaviour
{
    public Spline spline;
    public float speed = 0.2f; // normalized units per second
    public bool orientToSpline = true;
    public bool loop = true;

    float t = 0f;

    void Start()
    {
        if (spline == null) spline = FindAnyObjectByType<Spline>();
    }

    void Update()
    {
        if (spline == null) return;
        float dt = speed * Time.deltaTime;
        t += dt;
        if (loop) t %= 1f;
        else t = Mathf.Clamp01(t);
        transform.position = spline.GetPoint(t);
        if (orientToSpline)
        {
            Vector3 tangent = spline.GetTangent(t);
            if (tangent.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
            }
        }
    }
}
