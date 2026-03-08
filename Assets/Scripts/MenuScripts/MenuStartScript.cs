
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

    [Header("Параметры размещения")]
    public float spacing = 2f;              // Вертикальный отступ между объектами

    [Header("Настройки адаптации под размер")]
    public float availableHeightRatio = 0.9f;   // доля высоты экрана, занимаемая колонкой (0.9 = 90%)
    public float minObjectHeight = 1.5f;        // минимальный допустимый размер объекта
    public float maxObjectHeight = 3.0f;        // максимальный допустимый размер объекта
    public float desiredGap = 0.1f;              // желаемый зазор между объектами

    [Header("Настройки горизонтального размещения")]
    public float firstRowNormX = 0.15f;   // позиция первого ряда (0 = левый край, 1 = правый край)
    public float secondRowNormX = 0.5f;  // позиция второго ряда
    public float thirdRowNormX = 0.7f;   // позиция третьего ряда (нефинальный результат)
    public float resultNormX = 0.9f;      // позиция результата

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
        SetTransformForObj();
    }

    private void SetTransformForObj()
    {
        DataHolder.spacing = spacing;

        DataHolder.availableHeightRatio = availableHeightRatio;
        DataHolder.minObjectHeight = minObjectHeight;
        DataHolder.maxObjectHeight = maxObjectHeight;
        DataHolder.desiredGap = desiredGap;

        DataHolder.firstRowNormX = firstRowNormX;
        DataHolder.secondRowNormX = secondRowNormX;
        DataHolder.thirdRowNormX = thirdRowNormX;
        DataHolder.resultNormX = resultNormX;
    }
}
