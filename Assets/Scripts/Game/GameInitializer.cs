using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameObject buttonPrefab;
    private WinConditionDTO winConditionDTO;
    public int EndResult => winConditionDTO.result;
    public static GameInitializer Instance { get; private set; }
    private GameInitializer() { }

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
        winConditionDTO = Randomizer.GetWinConditionDTO(GameInfo.CountOfNumbers);
        InitializeGameObjects(GameInfo.CountOfNumbers);

        SetEndResultText();

        DataHolder.Instance.ResultChanged += UpdateCurrentResult;
    }

    private void UpdateCurrentResult(int result)
    {
        GameCanvas.Instance.CurrentResult.SetText(result.ToString());
    }

    

    private void SetEndResultText()
    {
        GameCanvas.Instance.EndResult.SetText(winConditionDTO.result.ToString());
    }

    private void InitializeGameObjects(int count)
    {
        if (buttonPrefab == null)
        {
            Debug.Log("Button prefab is null");
            return;
        }

        RectTransform leftPanel = GameCanvas.Instance.LeftPanel;
        RectTransform rightPanel = GameCanvas.Instance.RightPanel;

        CreateObjectInPanel(leftPanel, winConditionDTO.firstArray, true);
        CreateObjectInPanel(rightPanel, winConditionDTO.secondArray, false);
    }

    private void CreateObjectInPanel(RectTransform panel, int[] array, bool isFirstRow)
    {
        
        for (int i = 0; i < array.Length; i++)
        {
            var obj = Instantiate(buttonPrefab, panel);
            ButtonInfo info = obj.GetComponent<ButtonInfo>();
            info.Text.SetText(array[i].ToString());
            info.SetNumber(array[i]);

            if (isFirstRow)
            {
                obj.AddComponent<FirstRowButton>();
            }
            else
            {
                obj.AddComponent<SecondRowButton>();
            }
        }
    }
}
