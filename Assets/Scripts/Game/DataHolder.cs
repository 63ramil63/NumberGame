using System;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public FirstRowButton selectedButton;
    public bool isGameEnded = false;
    private int currentResult = 0;
    public event Action<int> ResultChanged;
    public int CurrentResult
    {
        get => currentResult;
        set
        {
            if (currentResult == value) return;
            currentResult = value;
            ResultChanged?.Invoke(currentResult);
        }
    }

    private DataHolder() { }
    public static DataHolder Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
