using UnityEngine;

public static class ThemeColors
{
    private static ThemePallete pallete;

    public static Color Get(int theme, ColorRole role)
    {
        if (pallete == null) pallete = Resources.Load<ThemePallete>("ThemePallete");
        return pallete != null ? pallete.Get(theme, role) : Color.magenta;
    }
}
