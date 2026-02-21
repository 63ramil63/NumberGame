using TMPro;
using UnityEngine;

public class ResultBtnScript : MonoBehaviour
{
    private int number;
    private TextMeshPro tmp;
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
    public int GetNumber()
    {
        return number;
    }
}
