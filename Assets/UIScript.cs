using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        slider = GetComponentInChildren<Slider>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (DataHolder.countOfObj != 0)
        {
            slider.value = DataHolder.countOfObj;
        }
        else
        {
            DataHolder.countOfObj = (int)slider.value;
        }
        textMeshPro.text = slider.value.ToString();
        slider.onValueChanged.AddListener(v => {
            textMeshPro.text = v.ToString();
            DataHolder.countOfObj = (int) v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
