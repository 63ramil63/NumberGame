using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPolyLine : MaskableGraphic
{
    public float thickness = 6f;
    private readonly List<Vector2> points = new List<Vector2>();

    protected override void Awake()
    {
        base.Awake();
        raycastTarget = false;
    }

    public void SetPoints(IList<Vector2> newPoints)
    {
        points.Clear();
        points.AddRange(newPoints);
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        int n = points.Count;
        if (n < 2) return;

        float half = thickness * .5f;
        for (int i = 0; i < n; i++)
        {
            Vector2 normal;
            float scale = 1f;

            if (i == 0) normal = Perp(points[1] - points[0]);
            else if (i == n - 1) normal = Perp(points[i] - points[i - 1]);
            else
            {
                Vector2 n1 = Perp(points[i] - points[i - 1]);
                Vector2 n2 = Perp(points[i + 1] - points[i]);
                normal = (n1 + n2).normalized;
                scale = 1f / Mathf.Max(Vector2.Dot(normal, n1), .3f); // smooth fracture with no "steps"
            }

            Vector2 offset = normal * half * scale;
            vh.AddVert(points[i] + offset, color, Vector2.zero);
            vh.AddVert(points[i] - offset, color, Vector2.zero);
        }

        for (int i = 0; i < n - 1; i++)
        {
            int a = i * 2;
            vh.AddTriangle(a, a + 1, a + 2);
            vh.AddTriangle(a + 1, a + 3, a + 2);
        }
    }

    private static Vector2 Perp(Vector2 dir)
    {
        dir.Normalize();
        return new Vector2(-dir.y, dir.x);
    }
}
