using UnityEngine;

public static class FrameRateSetup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;  
    }
}