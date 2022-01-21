using UnityEngine;
using UnityEngine.Events;

public class ChangeLanguage : MonoBehaviour
{
    public static ChangeLanguage Instance { get; private set; }
    public LanguageChangeEvent OnLanguageChange;

    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void ChangeLanguageTo(string lang)
    {
        OnLanguageChange.Invoke(lang);
        Debug.Log($"Language changed to:{lang}");
    }
}
[System.Serializable]
public class LanguageChangeEvent : UnityEvent<string>
{

}