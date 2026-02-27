using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public bool isRunning = true;
    private float currentTime;


    public float getCurrentTime()
    {
        return currentTime;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateDisplay(currentTime);
        }
        if (!DataHolder.isGameActive)
        {
            SetTimerActive(false);
        }
    }

    private void UpdateDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);          // общее количество минут
        int seconds = Mathf.FloorToInt(time % 60f);          // секунды (0Ц59)
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f); // миллисекунды (0Ц99)

        timer.SetText(DataHolder.GetTextFromTime(time));
    }

    private void SetTimerActive(bool b)
    {
        isRunning = b;
    }

    public void SetFinalTime(float time)
    {
        UpdateDisplay(time);
        SetTimerActive(false);
    }

}
