using TMPro;
using UnityEngine;

public class FirstRowBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    public bool isAvailable = true;
    public bool isSelected = false;
    private SecondRowBtnScript secondRowBtnScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
    }

    public void Initialize(int number)
    {
        this.number = number;
        tmp.text = number.ToString();
    }

    private void OnMouseEnter()
    {
        
    }

    public void SetConnectedBtn(SecondRowBtnScript script)
    {
        secondRowBtnScript = script;
    }

    public void ChangeIsAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        tmp.text = "_" + number.ToString() + "_";
    }

    void ChangeText()
    {
        if (isSelected)
        {
            tmp.text = "_" + tmp.text;
        }
        else
        {
            tmp.text = number.ToString();
        }
    }

    public void ChangeIsSelected(bool isSelected)
    {
        CheckBeforeSelect();
        this.isSelected = isSelected;
        ChangeText();
    }

    void CheckBeforeSelect()
    {
        FirstRowBtnScript script = DataHolder.gameInfoManager.FindSelectedObjectIn1Row();
        if (script != null && !isSelected)
        {
            script.ChangeIsSelected(false);
        }
    }

    private void OnMouseDown()
    {
        if (isAvailable)
        {
            ChangeIsSelected(!isSelected);
        }
        else
        {
            ChangeIsAvailable(true);
            ChangeIsSelected(false);
            secondRowBtnScript.ChangeIsAvailable(true);
            DecreaseNotFinalResult();
        }
    }

    void DecreaseNotFinalResult()
    {
        NotFinalResultBtnScript script = DataHolder.gameInfoManager.notFinalResultObj;
        if (script != null)
        {
            script.ChangeNumber(-secondRowBtnScript.GetNumber() * this.number);
            secondRowBtnScript = null;
        }
    }

    private void OnMouseExit()
    {

    }

    public int GetNumber()
    {
        return number;
    }
}
