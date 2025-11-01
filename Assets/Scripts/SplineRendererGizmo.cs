using UnityEngine;

[ExecuteInEditMode]
public class SplineRendererGizmo : MonoBehaviour
{
    public Spline spline;
    public int resolution = 50;
    public Color color = Color.green;

    [Header("LineRenderer")]
    public bool useLineRenderer = true;
    public float lineWidth = 0.05f;
    public Material lineMaterial;

    LineRenderer lr;

    void OnEnable()
    {
        if (spline == null) spline = GetComponent<Spline>();
        SetupLineRenderer();
        UpdateLineRenderer();
    }

    void Update()
    {
        if (spline == null) spline = GetComponent<Spline>();
        if (useLineRenderer) UpdateLineRenderer();
    }

    void OnValidate()
    {
        // keep line renderer in sync when values change in inspector
        SetupLineRenderer();
        UpdateLineRenderer();
    }

    void SetupLineRenderer()
    {
        if (!useLineRenderer)
        {
            // if present but disabled, don't remove it, just disable
            if (lr != null) lr.enabled = false;
            return;
        }

        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
            lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lr.receiveShadows = false;
        }

        lr.enabled = true;
        lr.widthMultiplier = Mathf.Max(0f, lineWidth);
        lr.useWorldSpace = true;
        lr.loop = (spline != null && spline.loop);
        lr.numCapVertices = 2;

        if (lineMaterial != null)
        {
            // use sharedMaterial to avoid creating an instance of the material in edit mode
            lr.sharedMaterial = lineMaterial;
        }
        else
        {
            // assign a simple default material if none provided
            // use sharedMaterial here as well to avoid instantiation/leaks
            if (lr.sharedMaterial == null || lr.sharedMaterial.shader == null)
            {
                var mat = new Material(Shader.Find("Sprites/Default"));
                lr.sharedMaterial = mat;
            }
        }
    }

    void UpdateLineRenderer()
    {
        if (spline == null) return;
        // ensure resolution at least1
        int res = Mathf.Max(1, resolution);
        if (useLineRenderer && lr == null) SetupLineRenderer();

        Vector3 prev = spline.GetPoint(0f);
        if (!useLineRenderer)
        {
            // keep gizmo drawing only
            return;
        }

        lr.positionCount = res + 1;
        for (int i = 0; i <= res; i++)
        {
            float t = (float)i / res;
            Vector3 p = spline.GetPoint(t);
            lr.SetPosition(i, p);
            prev = p;
        }

        // ensure loop flag matches spline
        lr.loop = spline.loop;
    }

    void OnDrawGizmos()
    {
        if (spline == null) spline = GetComponent<Spline>();
        if (spline == null) return;

        Gizmos.color = color;
        Vector3 prev = spline.GetPoint(0f);
        for (int i = 1; i <= resolution; i++)
        {
            float t = (float)i / resolution;
            Vector3 p = spline.GetPoint(t);
            Gizmos.DrawLine(prev, p);
            prev = p;
        }
    }
}
