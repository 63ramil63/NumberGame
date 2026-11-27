using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class ThemedGraphic : MonoBehaviour
{
    [SerializeField] private ColorRole role = ColorRole.Text;

    private Graphic graphic;

    private void Awake()
    {
        graphic = GetComponent<Graphic>();
    }

    private void OnEnable()
    {
        SaveStorage.OnThemeChanged += Apply;
        Apply(SaveStorage.Theme);
    }

    private void OnDisable()
    {
        SaveStorage.OnThemeChanged -= Apply;
    }

    private void Apply(int theme)
    {
        graphic.color = ThemeColors.Get(theme, role);
    }
}
