using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private float time, frames, fps;

    private void Update()
    {
        time += Time.unscaledDeltaTime;
        frames++;
        if (time >= 0.5f) { fps = frames / time; time = 0; frames = 0; }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 400, 60), $"FPS: {fps:F0}",
            new GUIStyle(GUI.skin.label) { fontSize = 40 });
    }
}