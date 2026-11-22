using UnityEngine;

public class UIConnection : MonoBehaviour
{
    private UIPolyLine polyline;
    private RectTransform from, to, space;
    private int lane, maxLanes;
    private const float LaneSpacing = 14f;

    public void Init(RectTransform from, RectTransform to, int lane, int maxLanes, Color color)
    {
        this.from = from;
        this.to = to;
        this.lane = lane;
        this.maxLanes = maxLanes;

        polyline = GetComponent<UIPolyLine>();
        polyline.color = color;

        space = (RectTransform)transform.parent;
        var rt = (RectTransform)transform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.pivot = space.pivot; // so that the coordinate systems coincide
        rt.offsetMin = rt.offsetMax = Vector2.zero; // stretch across the entire container of lines
        rt.localScale = Vector3.one;

        Rebuild();
    }

    private void Rebuild()
    {
        Vector2 a = EdgePoint(from, to);
        Vector2 b = EdgePoint(to, from);

        float midX = (a.x + b.x) * 0.5f;
        float laneX = midX + (lane - (maxLanes - 1) * 0.5f) * LaneSpacing;

        if (Mathf.Abs(a.y - b.y) < polyline.thickness)
        {
            float y = (a.y + b.y) * 0.5f;
            polyline.SetPoints(new Vector2[] { new Vector2(a.x, y), new Vector2(b.x, y) });
        }
        else
        {
            polyline.SetPoints(new Vector2[] { a, new Vector2(laneX, a.y), new Vector2(laneX, b.y), b });
        }
    }

    // the midpoint of the edge closest to the other button, in the coordinate system of the line container
    private Vector2 EdgePoint(RectTransform r, RectTransform other)
    {
        bool otherIsRight = other.position.x > r.position.x;
        float x = otherIsRight ? r.rect.xMax : r.rect.xMin;
        Vector3 world = r.TransformPoint(new Vector3(x, r.rect.center.y, 0));
        return space.InverseTransformPoint(world);
    }
}
