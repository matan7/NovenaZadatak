using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class Page2Controller : MonoBehaviour
{
    [SerializeField] private Page2Model _model;

    [Header("UI")]
    [SerializeField] private Transform _thumbnailsContainer;
    [SerializeField] private GameObject _galleryThumbnailPrefab;
    [SerializeField] private GridLayoutGroup _galleryGrid;

    private List<GalleryThumbnail> _photos;

    void Start()
    {
        _photos = new List<GalleryThumbnail>();
        UpdateModelData("Croatian");
        ChangeLanguage.Instance.OnLanguageChange.AddListener(LoadTextOnly);
    }

    private void UpdateModelData(string lang)
    {
        StartCoroutine(LoadDataCRT(lang));
    }

    private void PopulateGallery(bool isTextUpdate)
    {
        if (isTextUpdate)
        {
            for (int i = 0; i < _photos.Count; i++)
            {
                _photos[i].Name = _model.Data.DataList[i].PictureName;
            }
        }
        else
        {
            for (int i = 0; i < _model.Data.DataList.Count; i++)
            {
                var obj = GameObject.Instantiate(_galleryThumbnailPrefab, _thumbnailsContainer);
                var thumb = obj.GetComponent<GalleryThumbnail>();
                thumb.SetupThumbnail(_model.Data.DataList[i].Picture, _galleryGrid.cellSize / 2);
                thumb.Name = _model.Data.DataList[i].PictureName;
                _photos.Add(thumb);
            }
        }
    }

    private void LoadTextOnly(string lang)
    {
        string json = "";
        string path = "";

        if (lang.Contains("English"))
            path = Application.streamingAssetsPath + "/Page2Data/Page2Data_Eng.json";
        else if (lang.Contains("Croatian"))
            path = Application.streamingAssetsPath + "/Page2Data/Page2Data_Cro.json";

        json = File.ReadAllText(path);
        if (json == string.Empty)
        {
            Debug.LogError($"Error: json data file is empty or not exist: {path}");
            return;
        }
        var data = JsonUtility.FromJson<Page2JsonFile>(json);

        for (int i = 0; i < _model.Data.DataList.Count; i++)
        {
            _model.Data.DataList[i].PictureName = data.PicturesData[i].PictureName;
        }

        PopulateGallery(true);
    }

    private IEnumerator LoadDataCRT(string lang)
    {
        string json = "";
        string path = "";

        if (lang.Contains("English"))
            path = Application.streamingAssetsPath + "/Page2Data/Page2Data_Eng.json";
        else if (lang.Contains("Croatian"))
            path = Application.streamingAssetsPath + "/Page2Data/Page2Data_Cro.json";

        json = File.ReadAllText(path);
        if (json == string.Empty)
        {
            Debug.LogError($"Error: json data file is empty or not exist: {path}");
            yield break;
        }
        var data = JsonUtility.FromJson<Page2JsonFile>(json);

        _model.Data = new Page2Data();
        _model.Data.DataList = new List<Page2PictureData>();
        for (int i = 0; i < data.PicturesData.Length; i++)
        {
            path = "file://" + Application.streamingAssetsPath + "/Page2Data/Photos/" + data.PicturesData[i].FileName;
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(path))
            {
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(request);
                    var dat = new Page2PictureData();
                    dat.Picture =  Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one / 2);
                    dat.PictureName = data.PicturesData[i].PictureName;
                    _model.Data.DataList.Add(dat);
                }
            }
        }
        PopulateGallery(false);
    }
}
