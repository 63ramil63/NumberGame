using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotFinalResultBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    private GameUIController gameUIController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        if(tmp == null)
        {
            Debug.Log("Error initialize tmp");
        }
        gameUIController = GameObject.Find("Canvas").GetComponent<GameUIController>();
    }
    public void Initialize(int number)
    {
        this.number = number;
        tmp.SetText(number.ToString());
    }

    public void ChangeNumber(int number)
    {
        this.number += number;
        tmp.SetText(this.number.ToString());
        CheckIfWin();
    }

    void CheckIfWin()
    {
        ResultBtnScript finalResult = DataHolder.gameInfoManager.finalResultObs;
        float newTime = gameUIController.getCurrentTime();
        if (finalResult != null)
        {
            if (finalResult.GetNumber() == number)
            {
                if (!DataHolder.gameInfoManager.CheckFirstRowForAvailable())
                {
                    DataHolder.isGameActive = false;
                    gameUIController.SetFinalTime(newTime);
                    Debug.Log(newTime);
                    if (!PlayerPrefs.HasKey("Record" + DataHolder.countOfObj))
                    {
                        PlayerPrefs.SetFloat("Record" + DataHolder.countOfObj, newTime);
                    } 
                    else
                    {
                        float oldTime = PlayerPrefs.GetFloat("Record" + DataHolder.countOfObj);
                        if (newTime < oldTime)
                        {
                            PlayerPrefs.SetFloat("Record" + DataHolder.countOfObj, newTime);
                        }
                    }
                }
            }
        }
    }
    public int GetNumber()
    {
        return number;
    }
}
