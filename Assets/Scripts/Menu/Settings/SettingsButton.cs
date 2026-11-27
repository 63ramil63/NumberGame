using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Button showRecordButton;
    [SerializeField] private Button resetRecordButton;
    [SerializeField] private Button switchThemeButton;

    private void Awake()
    {
        showRecordButton.onClick.AddListener(ShowRecords);
        switchThemeButton.onClick.AddListener(SwitchTheme);
        resetRecordButton.onClick.AddListener(ResetRecord);
    }

    private void OnDestroy()
    {
        showRecordButton.onClick.RemoveListener(ShowRecords);
        switchThemeButton.onClick.RemoveListener(SwitchTheme);
        resetRecordButton.onClick.RemoveListener(ResetRecord);
    }

    private void ShowRecords()
    {
        SaveStorage.ShowRecord = (SaveStorage.ShowRecord + 1) % 2;
    }

    private void SwitchTheme()
    {
        SaveStorage.Theme = (SaveStorage.Theme + 1) % 2;
    }

    private void ResetRecord()
    {
        SaveStorage.ResetRecords();
    }
}
