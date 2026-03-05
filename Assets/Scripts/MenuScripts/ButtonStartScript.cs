
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonStartScript : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }
    void LoadScene()
    {
        if (DataHolder.startGameBtnSound != null)
        {
            SoundManagerScript.Instance.PlaySound(DataHolder.startGameBtnSound);
        }
        SceneManager.LoadScene("GameScene");
    }
}
