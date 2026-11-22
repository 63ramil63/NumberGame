using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button btn;
    [SerializeField] private int number;

    public TextMeshProUGUI Text => text;
    public Button Button => btn;
    public int Number => number;
    public void SetNumber(int number)
    {
        this.number = number;
    }

    private void Awake()
    {
        if (btn == null) btn = GetComponent<Button>();
    }
}
