using UnityEngine;

public enum ColorRole { Background, Panel, Text, Button, ButtonText, Accent}

[CreateAssetMenu(fileName = "ThemePallete", menuName = "Theme/Pallete")]
public class ThemePallete : ScriptableObject
{
    [System.Serializable]
    public class Theme
    {
        public Color background = Color.white;
        public Color panel = Color.white;
        public Color text = Color.black;
        public Color button = Color.white;
        public Color buttonText = Color.black;
        public Color accent = Color.cyan;
    }

    public Theme light = new Theme();
    public Theme dark = new Theme();

    public Color Get(int theme, ColorRole role)
    {
        Theme t = theme == 0 ? light : dark;
        switch (role)
        {
            case ColorRole.Background: return t.background;
            case ColorRole.Panel: return t.panel;
            case ColorRole.Text: return t.text;
            case ColorRole.Button: return t.button;
            case ColorRole.ButtonText: return t.buttonText;
            default: return t.accent;
        }
    }
}
