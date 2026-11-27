using TMPro;
using UnityEngine;

public class RecordTimeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int countOfNumber = 1;

    private void Awake()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SaveStorage.OnShowRecordChanged += SetVisibility;
        SaveStorage.OnResetRecordChanged += UpdateText;
        SetVisibility(SaveStorage.ShowRecord);
    }

    private void OnDestroy()
    {
        SaveStorage.OnShowRecordChanged -= SetVisibility;
        SaveStorage.OnResetRecordChanged -= UpdateText;
    }

    private void SetVisibility(int showRecord)
    {
        if (showRecord == 0)
        {
            text.SetText("");
        }
        else
        {
            float time = SaveStorage.GetBest(countOfNumber);
            text.SetText(time < 0 ? "--" : time.ToString("F2"));
        }
    }

    private void UpdateText()
    {
        float time = SaveStorage.GetBest(countOfNumber);
        text.SetText(time < 0 ? "--" : time.ToString("F2"));
    }
}
