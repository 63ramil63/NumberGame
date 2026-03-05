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

    private FirstRowBtnScript connectedObj;

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
        if (!DataHolder.isGameActive)
        {
            return;
        }
        if (isAvailable)
        {
            connectedObj = DataHolder.gameInfoManager.FindSelectedObjectIn1Row();
            if (connectedObj != null)
            {
                connectedObj.CloseObject(this);
            }
        }
        else
        {
            connectedObj.FreeObj();
            FirstRowBtnScript firstRowBtnScript = DataHolder.gameInfoManager.FindSelectedObjectIn1Row();
            if (firstRowBtnScript != null)
            {
                connectedObj = firstRowBtnScript;
                connectedObj.CloseObject(this);
            }
        }
        int i = Random.Range(0, DataHolder.secondRowBtnSound.Length);
        SoundManagerScript.Instance.PlaySound(DataHolder.secondRowBtnSound[i]);
    }

    public int GetNumber()
    {
        return number;
    }
}
