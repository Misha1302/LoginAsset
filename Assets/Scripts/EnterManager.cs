using UnityEngine;

public static class EnterManager
{
    public static bool Entered
    {
        get => PlayerPrefs.GetInt("Entered") == 1;
        set => PlayerPrefs.SetInt("Entered", value ? 1 : 0);
    }
    
    public static string PersonName
    {
        get => PlayerPrefs.GetString("PersonName");
        set => PlayerPrefs.SetString("PersonName", value);
    }
}