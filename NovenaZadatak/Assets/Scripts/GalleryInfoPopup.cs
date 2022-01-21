using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryInfoPopup : MonoBehaviour
{
    public static GalleryInfoPopup Instance { get; private set; }

    [SerializeField] private RectTransform _popup;
    [SerializeField] private TextMeshProUGUI _photoName;
    [SerializeField] private Button _expandButton;
    [SerializeField] private PhotoExpander _photoExpander;

    private RectTransform _thumbnailToFollow;
    private Sprite _photo;
    private Vector3 _offset;
    private float _screenWidth;

    private void Awake()
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

    void Start()
    {
        _expandButton.onClick.AddListener(ExpandPhoto);
        _screenWidth = Screen.width;
    }

    private void Update()
    {
        if (_popup.gameObject.activeSelf)
        {
            if ((_thumbnailToFollow.position + _offset).x > _screenWidth - _popup.sizeDelta.x)
                _popup.position = _thumbnailToFollow.position + new Vector3(-_offset.x, _offset.y) - (Vector3.right * _popup.sizeDelta.x);
            else
                _popup.position = _thumbnailToFollow.position + _offset;
        }
    }

    public void OpenInfoPopup(string name, Sprite photo, RectTransform caller, Vector3 offset)
    {
        _photo = photo;
        _photoName.text = name;
        _thumbnailToFollow = caller;
        _offset = offset;

        _popup.gameObject.SetActive(true);
    }

    private void ExpandPhoto()
    {
        _photoExpander.ExpandPhoto(_photo);
    }
}
