using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public int countOfNumbers = 3;

    public TextMeshProUGUI numberText;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        if (btn == null) Debug.Log($"Button is null on obj {gameObject.name}");

        if (numberText == null) Debug.Log($"Number text is null on obj {gameObject.name}");
        else numberText.SetText(countOfNumbers.ToString());
    }

    public void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameInfo.CountOfNumbers = countOfNumbers;
        SceneManager.LoadScene("GameScene");
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnClick);
    }
}
