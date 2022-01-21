using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page1Model : MonoBehaviour
{
    public Page1TextData TextData;
    public Page1Data Data = new Page1Data();
}
[System.Serializable]
public class Page1Data
{
    public Sprite BackgroundImage;
    public Sprite Photo;
}
[System.Serializable]
public class Page1TextData
{
    public string HeaderTxt;
    public string[] TextTxt;
}