using UnityEngine;

public class GameInfoManager : MonoBehaviour
{
    private FirstRowBtnScript[] firstRowObjs;
    private SecondRowBtnScript[] secondRowObjs;
    public NotFinalResultBtnScript notFinalResultObj;
    public ResultBtnScript finalResultObs;

    public void Initialize(FirstRowBtnScript[] firstRow, SecondRowBtnScript[] secondRow, NotFinalResultBtnScript notFinalResult, ResultBtnScript finalResult)
    {
        this.firstRowObjs = firstRow;
        this.secondRowObjs = secondRow;
        this.notFinalResultObj = notFinalResult;
        this.finalResultObs = finalResult;
    }

    public FirstRowBtnScript FindSelectedObjectIn1Row()
    {
        for (int i = 0; i < firstRowObjs.Length; i++)
        {
            if (firstRowObjs[i].isSelected)
            {
                return firstRowObjs[i];
            }
        }
        return null;
    }

    public bool CheckFirstRowForAvailable()
    {
        for (int i = 0; i < firstRowObjs.Length; i++)
        {
            if (firstRowObjs[i].isAvailable)
            {
                return true;
            }
        }
        return false;
    }
}
