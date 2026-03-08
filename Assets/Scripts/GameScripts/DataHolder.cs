using TMPro;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static int countOfObj;
    public static GameInfoManager gameInfoManager;
    public static Sprite[] backgrounds;
    public static AudioClip startGameBtnSound;
    public static AudioClip endGameSound;
    public static AudioClip[] firstRowBtnSound;
    public static AudioClip[] secondRowBtnSound;
    public static bool isGameActive = false;

    [Header("Параметры размещения")]
    public static float spacing;              // Вертикальный отступ между объектами

    [Header("Настройки адаптации под размер")]
    public static float availableHeightRatio;   // доля высоты экрана, занимаемая колонкой (0.9 = 90%)
    public static float minObjectHeight;        // минимальный допустимый размер объекта
    public static float maxObjectHeight;        // максимальный допустимый размер объекта
    public static float desiredGap;              // желаемый зазор между объектами

    [Header("Настройки горизонтального размещения")]
    public static float firstRowNormX;   // позиция первого ряда (0 = левый край, 1 = правый край)
    public static float secondRowNormX;  // позиция второго ряда
    public static float thirdRowNormX;   // позиция третьего ряда (нефинальный результат)
    public static float resultNormX;      // позиция результата


    public static void UpdateRecord(TextMeshProUGUI tmp)
    {
        if (PlayerPrefs.HasKey("Record" + countOfObj))
        {
            float time = PlayerPrefs.GetFloat("Record" + countOfObj);
            string formattedTime = GetTextFromTime(time);
            tmp.SetText("Рекорд:\n" + formattedTime);
        }
        else
        {
            tmp.SetText("Рекорд:\n00:00:00");
        }
    }

    public static string GetTextFromTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);          // общее количество минут
        int seconds = Mathf.FloorToInt(time % 60f);          // секунды (0–59)
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f); // миллисекунды (0–99)

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
