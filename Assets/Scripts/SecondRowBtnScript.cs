using TMPro;
using UnityEngine;

public class SecondRowBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    public bool isAvailable = true;
    
    public bool isSelected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
    }
    public void Initialize(int number)
    {
        Debug.Log($"Number is {number}");
        this.number = number;
        tmp.text = number.ToString();
    }

    public void ChangeIsAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        ChangeText();
    }

    void ChangeText()
    {
        if (isAvailable)
        {
            tmp.text = number.ToString();
        }
        else
        {
            tmp.text = "_" + number.ToString() + "_";
        }
    }

    private void OnMouseDown()
    {
        if (isAvailable)
        {
            FirstRowBtnScript fRowScript = DataHolder.gameInfoManager.FindSelectedObjectIn1Row();
            if (fRowScript != null)
            {
                fRowScript.ChangeIsSelected(false);
                fRowScript.ChangeIsAvailable(false);
                fRowScript.SetConnectedBtn(this);
                NotFinalResultBtnScript notFinalRes = DataHolder.gameInfoManager.notFinalResultObj;
                if (notFinalRes != null)
                {
                    notFinalRes.ChangeNumber(fRowScript.GetNumber() * number);
                    ChangeIsAvailable(false);
                }
            }
        }
    }

    public int GetNumber()
    {
        return number;
    }
}
