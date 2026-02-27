using TMPro;
using UnityEngine;

public class SecondRowBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
    private SpriteRenderer spriteRenderer;
    public bool isAvailable = true;

    [Header("Icons Sprite")]
    [SerializeField]
    Sprite baseIcon;
    [SerializeField]
    Sprite disavailableIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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


    void ChangeIcon()
    {
        if (isAvailable)
        {
            spriteRenderer.sprite = baseIcon;
        }
        else if (!isAvailable)
        {
            spriteRenderer.sprite = disavailableIcon;
        }
    }


    public void ChangeIsAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        ChangeIcon();
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
