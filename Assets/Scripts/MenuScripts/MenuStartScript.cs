
using UnityEngine;

public class MenuStartScript : MonoBehaviour
{
    private static bool isActivated = false;
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private AudioClip startGameBtnSound;
    [SerializeField]
    private AudioClip endGameSound;
    [SerializeField]
    private AudioClip[] firstRowBtnSound;
    [SerializeField]
    private AudioClip[] secondRowBtnSound;

    private void Awake()
    {
        if (background != null && backgrounds.Length > 0 && !isActivated)
        {
            DataHolder.backgrounds = backgrounds;
            isActivated = true;
        }
        if (background != null)
        {
            background.GetComponent<SpriteRenderer>().sprite = DataHolder.backgrounds[Random.Range(0, DataHolder.backgrounds.Length)];
            CameraView.ScaleToFillCamera(background, Camera.main, false);

        }
        if (endGameSound != null)
        {
            DataHolder.endGameSound = endGameSound;
        }
        if (firstRowBtnSound != null)
        {
            DataHolder.firstRowBtnSound = firstRowBtnSound;
        }
        if (secondRowBtnSound != null)
        {
            DataHolder.secondRowBtnSound = secondRowBtnSound;
        }
        if (startGameBtnSound != null)
        {
            DataHolder.startGameBtnSound = startGameBtnSound;
        }
    }

}
