using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private CanvasGroup dimmer;
    [SerializeField] private Button openButton;
    [SerializeField] private Button dimmerButton;
    [SerializeField] private float animDuration = .4f;

    private Coroutine animation;

    public bool IsOpen {  get; private set; }
    public float HiddenX => -panel.rect.width;

    private void Awake()
    {
        openButton.onClick.AddListener(Open);
        dimmerButton.onClick.AddListener(Close);

        // Start position is closed
        panel.anchoredPosition = new Vector2(HiddenX, 0);
        dimmer.alpha = 0f;
        dimmer.blocksRaycasts = false;
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(Open);
        dimmerButton.onClick.RemoveListener(Close);
    }

    public void Open() => Show(true);
    public void Close() => Show(false);

    public void Toggle() => Show(!IsOpen);

    private void Show(bool open)
    {
        if (IsOpen == open) return;

        IsOpen = open;

        dimmer.blocksRaycasts = open;

        if (animation != null)
        {
            StopCoroutine(animation);
        }
        animation = StartCoroutine(Animate(open ? 0f : HiddenX, open ? 1f : 0f));
    }

    private IEnumerator Animate(float targetX, float targetAlpha)
    {
        float startX = panel.anchoredPosition.x;
        float startAlpha = dimmer.alpha;
        float time = 0f;

        yield return null;   // пропустить тяжёлый первый кадр

        while (time < animDuration)
        {
            time += Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);
            float t = Mathf.SmoothStep(0f, 1f, time / animDuration);

            panel.anchoredPosition = new Vector2(Mathf.Lerp(startX, targetX, t), 0f);
            dimmer.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        panel.anchoredPosition = new Vector2(targetX, 0f);
        dimmer.alpha = targetAlpha;
        animation = null;
    }
}
