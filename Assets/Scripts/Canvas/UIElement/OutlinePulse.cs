using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class OutlinePulse : MonoBehaviour
{
    [SerializeField] private Color colorA = Color.cyan;
    [SerializeField] private Color colorB = Color.magenta;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 6f;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        float t = (Mathf.Sin(Time.unscaledTime * speed) + 1f) * .5f;
        outline.effectColor = Color.Lerp(colorA, colorB, t);
        outline.effectDistance = Vector2.one * Mathf.Lerp(minDistance, maxDistance, t);
    }
}
