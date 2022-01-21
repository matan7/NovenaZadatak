using System.IO;
using UnityEngine;
using TMPro;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel _model;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _page1Button;
    [SerializeField] private TextMeshProUGUI _page2Button;
    [SerializeField] private TextMeshProUGUI _languageCroButton;
    [SerializeField] private TextMeshProUGUI _languageEng2Button;

    void Start()
    {
        UpdateModelData("Croatian");

        ChangeLanguage.Instance.OnLanguageChange.AddListener(UpdateModelData);
    }

    private void UpdateModelData(string lang)
    {
        string json = "";
        string path = "";

        if (lang.Contains("English"))
            path = Application.streamingAssetsPath + "/HomeData/HomeScreenData_Eng.json";
        else if (lang.Contains("Croatian"))
            path = Application.streamingAssetsPath + "/HomeData/HomeScreenData_Cro.json";
        
        json = File.ReadAllText(path);
        if (json == string.Empty)
        {
            Debug.LogError($"Error: json data file is empty or not exist: {path}");
            return;
        }
        _model.Data = JsonUtility.FromJson<HomeData>(json);
        UpdateViewWithNewData();
    }

    private void UpdateViewWithNewData()
    {
        _page1Button.SetText(_model.Data.Page1ButtonTxt);
        _page2Button.SetText(_model.Data.Page2ButtonTxt);
        _languageCroButton.SetText(_model.Data.CroLanguageButtonTxt);
        _languageEng2Button.SetText(_model.Data.EngLanguageButtonTxt);
    }
}
