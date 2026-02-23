using TMPro;
using UnityEngine;

public class FirstRowBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    public bool isAvailable = true;
    public bool isSelected = false;

    private SecondRowBtnScript secondRowBtnScript;

    private LineRenderer lineRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        SetupLineRenderer();
    }

    public void Initialize(int number)
    {
        this.number = number;
        tmp.text = number.ToString();
    }

    void SetupLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true; // Важно для 2D
        lineRenderer.sortingOrder = 10; // Чтобы линия была поверх объектов
        lineRenderer.sortingLayerName = "Default"; // Укажите нужный слой
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = Color.yellow;
        lineRenderer.material = lineMaterial;

        lineRenderer.enabled = false;
    }

    public void SetConnectedBtn(SecondRowBtnScript script)
    {
        secondRowBtnScript = script;
        DrawConnectionLine();
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
            DisconnectLine();
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

    void DrawConnectionLine()
    {
        if (lineRenderer != null && secondRowBtnScript != null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = secondRowBtnScript.transform.position;
            startPos.z = 0;
            endPos.z = 0;

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
            lineRenderer.sortingOrder = -1;
            lineRenderer.enabled = true;
        }
    }

    void DisconnectLine()
    {
        lineRenderer.enabled = false;   
    }

    public int GetNumber()
    {
        return number;
    }
}
