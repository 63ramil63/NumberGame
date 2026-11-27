using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public const int MaxLanes = 5;

    [SerializeField] private TextMeshProUGUI currentResult;
    [SerializeField] private TextMeshProUGUI endResult;
    [SerializeField] private TextMeshProUGUI slashSign;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform rightPanel;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Button retryButton;

    [Header("Connections")]
    [SerializeField] private RectTransform linesParent;
    [SerializeField] private UIConnection connectionPrefab;
    [SerializeField] private Color[] laneColors;

    public TextMeshProUGUI CurrentResult => currentResult;
    public TextMeshProUGUI EndResult => endResult;
    public TextMeshProUGUI SlashSign => slashSign;
    public RectTransform LeftPanel => leftPanel;
    public RectTransform RightPanel => rightPanel;
    public Button RetryButton => retryButton;
    public TextMeshProUGUI BestTimeText => bestTimeText;

    private GameCanvas() { }
    public static GameCanvas Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        float time = SaveStorage.GetBest(GameInfo.CountOfNumbers);
        if (time < 0)
        {
            bestTimeText.SetText("Best Time\n--:--");
        } 
        else
        {
            bestTimeText.SetText("Best Time\n" + time.ToString("F2"));
        }
    }

    public UIConnection CreateConnection(RectTransform from, RectTransform to, int lane)
    {
        Color color = (laneColors != null && laneColors.Length > 0)
            ? laneColors[lane % laneColors.Length]
            : Color.white;

        UIConnection connection = Instantiate(connectionPrefab, linesParent);
        connection.Init(from, to, lane, MaxLanes, color);
        return connection;
    }

    public Color GetLaneColor(int lane) =>
        (laneColors != null && laneColors.Length > 0) ? laneColors[lane % laneColors.Length] : Color.white;
}
