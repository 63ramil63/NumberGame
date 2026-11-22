using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsed;
    private bool isRunning = true;

    public float Elapsed => elapsed;

    private GameTimer() { }
    public static GameTimer Instance { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (timerText == null) timerText = GetComponent<TextMeshProUGUI>();
        if (timerText == null) Debug.Log($"TextMesh component not found on obj {gameObject.name}");
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsed += Time.deltaTime;
        timerText.SetText(elapsed.ToString("F2")); // Example, 00,00,00
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
