using UnityEngine;

public class MenuStartScript : MonoBehaviour
{
    private static bool isActivated = false;
    [SerializeField]
    GameObject background;

    [SerializeField]
    Sprite[] backgrounds;
    private void Awake()
    {
        if (background != null && backgrounds.Length > 0 && !isActivated)
        {
            DataHolder.backgrounds = backgrounds;
            isActivated = true;
        }
        if (background != null)
        {
            CameraView.ScaleToFillCamera(background, Camera.main, false);
            background.GetComponent<SpriteRenderer>().sprite = DataHolder.backgrounds[Random.Range(0, DataHolder.backgrounds.Length)];
        }
    }

}
