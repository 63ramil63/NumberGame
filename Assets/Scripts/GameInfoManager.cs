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
        Debug.Log($"firstRow is null == {firstRow == null}");
        Debug.Log($"secondRow is null == {secondRow == null}");
        Debug.Log($"notFinal is null == {notFinalResult == null}");
        Debug.Log($"finalRes is null == {finalResult == null}");
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

    public SecondRowBtnScript FindSelectedObjectIn2Row()
    {
        for (int i = 0; i < secondRowObjs.Length; i++)
        {
            
        }
        return null;
    }
}
