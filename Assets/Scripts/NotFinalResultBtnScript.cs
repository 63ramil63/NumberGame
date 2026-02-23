using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotFinalResultBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        if(tmp == null)
        {
            Debug.Log("Error initialize tmp");
        }
    }
    public void Initialize(int number)
    {
        this.number = number;
        tmp.text = number.ToString();
    }

    public void ChangeNumber(int number)
    {
        this.number += number;
        tmp.text = this.number.ToString();
        CheckIfWin();
    }

    void CheckIfWin()
    {
        ResultBtnScript finalResult = DataHolder.gameInfoManager.finalResultObs;
        if (finalResult != null)
        {
            if (finalResult.GetNumber() == number)
            {
                if (!DataHolder.gameInfoManager.CheckFirstRowForAvailable())
                {
                    SceneManager.LoadScene("MainScene");
                }
            }
        }
    }
    public int GetNumber()
    {
        return number;
    }
}
