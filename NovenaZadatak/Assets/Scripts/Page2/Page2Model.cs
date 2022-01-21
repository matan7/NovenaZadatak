using System.Collections.Generic;
using UnityEngine;

public class Page2Model : MonoBehaviour
{
    public Page2Data Data;
}

[System.Serializable] 
public class Page2Data
{
    public List<Page2PictureData> DataList;
}

[System.Serializable]
public class Page2PictureData
{
    public string PictureName;
    public Sprite Picture;
}

public class Page2JsonFile
{
    public Page2PictureJsonData[] PicturesData;
}

[System.Serializable]
public class Page2PictureJsonData
{
    public string PictureName;
    public string FileName;
}