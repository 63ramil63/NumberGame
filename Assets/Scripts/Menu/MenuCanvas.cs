using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Image background;
    [SerializeField] private Color darkThemeColor;
    [SerializeField] private Color lightThemeColor;

    [SerializeField] private TextMeshProUGUI record1;
    [SerializeField] private TextMeshProUGUI record2;
    [SerializeField] private TextMeshProUGUI record3;

    private MenuCanvas() { }
    public static MenuCanvas Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
