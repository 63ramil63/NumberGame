using TMPro;
using UnityEngine;

public class FirstRowBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    private SpriteRenderer spriteRenderer;
    public bool isAvailable = true;
    public bool isSelected = false;

    private SecondRowBtnScript secondRowBtnScript;

    private LineRenderer lineRenderer;

    [Header("Icons Sprite")]
    [SerializeField]
    Sprite baseIcon;
    [SerializeField]
    Sprite selectedIcon;
    [SerializeField]
    Sprite disavailableIcon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        SetupLineRenderer();
    }

    private void Start()
    {
        ChangeIcon();
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
        lineMaterial.color = Color.aliceBlue;
        lineRenderer.material = lineMaterial;

        lineRenderer.enabled = false;
    }

    public void SetConnectedBtn(SecondRowBtnScript script)
    {
        secondRowBtnScript = script;
        secondRowBtnScript.ChangeIsAvailable(false);
        DrawConnectionLine();
    }

    void ChangeIcon()
    {
        if (!isSelected && isAvailable)
        {
            spriteRenderer.sprite = baseIcon;
        } else if (isSelected && isAvailable)
        {
            spriteRenderer.sprite = selectedIcon;
        } else if (!isSelected && !isAvailable)
        {
            spriteRenderer.sprite = disavailableIcon;
        }
    }

    public void ChangeIsAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        ChangeIcon();
    }

    public void ChangeIsSelected(bool isSelected)
    {
        if (isSelected)
        {
            // Если выделяем этот объект – снимаем выделение с любого другого выделенного
            FirstRowBtnScript selected = DataHolder.gameInfoManager.FindSelectedObjectIn1Row();
            if (selected != null && selected != this)
            {
                selected.ChangeIsSelected(false);
            }
        }
        this.isSelected = isSelected;
        ChangeIcon();
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
        if (DataHolder.isGameActive)
        {
            if (isAvailable)
            {
                ChangeIsSelected(!isSelected);
                ChangeIcon();
            }
            else
            {
                FreeObj();
            }
        }
    }

    public void FreeObj()
    {
        ChangeIsAvailable(true);
        ChangeIsSelected(false);
        secondRowBtnScript.ChangeIsAvailable(true);
        DecreaseNotFinalResult();
        DisconnectLine();
        ChangeIcon();
        secondRowBtnScript = null;
    }

    public void CloseObject(SecondRowBtnScript script)
    {
        ChangeIsSelected(false);
        ChangeIsAvailable(false);
        SetConnectedBtn(script);
        IncreaseNotFinalResult();
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

    void IncreaseNotFinalResult()
    {
        NotFinalResultBtnScript notFinalRes = DataHolder.gameInfoManager.notFinalResultObj;
        if (notFinalRes != null)
        {
            notFinalRes.ChangeNumber(number * secondRowBtnScript.GetNumber());
            ChangeIsAvailable(false);
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
