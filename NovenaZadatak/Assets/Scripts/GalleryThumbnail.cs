using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GalleryThumbnail : MonoBehaviour
{
    public string Name { get; set; }
    private Sprite _photo;
    private Vector2 _offset;

    [SerializeField] private Image _thumnail;
    [SerializeField] private Button _thumbnailButton;
    [SerializeField] private RectTransform _myRectTransform;

    public void SetupThumbnail(Sprite photo, Vector2 offset)
    {
        _photo = photo;
        _offset = offset;
    }

    private IEnumerator Start()
    {
        yield return null;
        _thumnail.sprite = _photo;
        _thumbnailButton.onClick.AddListener(() => { GalleryInfoPopup.Instance.OpenInfoPopup(Name, _photo, _myRectTransform, _offset); });
    }
}
