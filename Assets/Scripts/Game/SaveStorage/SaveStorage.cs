using System;
using UnityEngine;

public static class SaveStorage
{
    private static readonly int[] levels = { 3, 4, 5 };

    public static string RecordKey(int count) => $"best_time_{count}";
    private const string ThemeKey = "theme";
    private const string ShowRecordsKey = "show_records";

    public static event Action<int> OnThemeChanged;
    public static event Action<int> OnShowRecordChanged;
    public static event Action OnResetRecordChanged;

    public static float GetBest(int countOfNumbers)
    {
        float time = PlayerPrefs.GetFloat(RecordKey(countOfNumbers), -1f);
        return time;
    }

    public static bool TrySetBest(int countOfNumbers, float time)
    {
        float best = PlayerPrefs.GetFloat(RecordKey(countOfNumbers), -1f);
        if (best >= 0f && time >= best) return false;

        PlayerPrefs.SetFloat(RecordKey(countOfNumbers), time);
        PlayerPrefs.Save();
        return true;
    }

    public static void ResetRecords()
    {
        foreach (int count in levels) PlayerPrefs.DeleteKey(RecordKey(count));
        PlayerPrefs.Save();
        OnResetRecordChanged?.Invoke();
    }

    public static int Theme
    {
        get => PlayerPrefs.GetInt(ThemeKey, 0);
        set
        {
            // 0 or 1
            int number = value % 2;
            PlayerPrefs.SetInt(ThemeKey, value % 2);
            PlayerPrefs.Save();
            OnThemeChanged?.Invoke(value % 2);
        }
    }

    public static int ShowRecord
    {
        get
        {
            return PlayerPrefs.GetInt(ShowRecordsKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(ShowRecordsKey, value % 2);
            PlayerPrefs.Save();
            OnShowRecordChanged?.Invoke(value % 2);
        }
    }
}
