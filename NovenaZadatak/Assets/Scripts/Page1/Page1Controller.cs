using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class Page1Controller : MonoBehaviour
{
    [SerializeField] private Page1Model _model;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _background;
    [SerializeField] private Image _photoThumb;
    [SerializeField] private Button _expandPhotoButton;


    void Start()
    {
        UpdateModelTextData("Croatian");
        UpdateModelData();
        ChangeLanguage.Instance.OnLanguageChange.AddListener(UpdateModelTextData);
        _expandPhotoButton.onClick.AddListener(()=> { PhotoExpander.Instance.ExpandPhoto(_model.Data.Photo); });
    }

    private void UpdateModelTextData(string lang)
    {
        string json = "";
        string path = "";

        if (lang.Contains("English"))
            path = Application.streamingAssetsPath + "/Page1Data/Page1TextData_Eng.json";
        else if (lang.Contains("Croatian"))
            path = Application.streamingAssetsPath + "/Page1Data/Page1TextData_Cro.json";

        json = File.ReadAllText(path);
        if (json == string.Empty)
        {
            Debug.LogError($"Error: json data file is empty or not exist: {path}");
            return;
        }
        _model.TextData = JsonUtility.FromJson<Page1TextData>(json);
        UpdateViewWithNewData();
    }

    private void UpdateModelData()
    {
        StartCoroutine(LoadAssets());
    }

    private IEnumerator LoadAssets()
    {
        string path = "file://" + Application.streamingAssetsPath + "/Page1Data/";
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(path + "2J0A6302 grayscale 1.png"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(request);
                _model.Data.BackgroundImage = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one / 2);
            }
        }
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(path + "DSC_065_Dujo Bušljeta 1.png"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(request);
                _model.Data.Photo = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one / 2);
            }
        }
        _background.sprite = _model.Data.BackgroundImage;
        _photoThumb.sprite = _model.Data.Photo;
    }

    private void UpdateViewWithNewData()
    {
        _header.text = _model.TextData.HeaderTxt;
        _text.text = string.Empty;
        for (int i = 0; i < _model.TextData.TextTxt.Length; i++)
        {
            _text.text += _model.TextData.TextTxt[i] + "\n";
        }
    }
}

