using UnityEngine;
using UnityEngine.UI;

public class ButtonBorder : MonoBehaviour
{
    [SerializeField] private Image border;

    private void Awake()
    {
        Hide();
    }

    public void Show(Color color)
    {
        border.color = color;
        border.enabled = true;
    }

    public void Hide()
    {
        border.enabled = false;
    }
}
