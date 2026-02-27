using TMPro;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static int countOfObj;
    public static GameInfoManager gameInfoManager;
    public static Sprite[] backgrounds;

    public static bool isGameActive = false;

    public static void UpdateRecord(TextMeshProUGUI tmp)
    {
        if (PlayerPrefs.HasKey("Record" + countOfObj))
        {
            float time = PlayerPrefs.GetFloat("Record" + countOfObj);
            string formattedTime = GetTextFromTime(time);
            tmp.SetText("–екорд:\n" + formattedTime);
        }
        else
        {
            tmp.SetText("–екорд:\n00:00:00");
        }
    }

    public static string GetTextFromTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);          // общее количество минут
        int seconds = Mathf.FloorToInt(time % 60f);          // секунды (0Ц59)
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f); // миллисекунды (0Ц99)

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
