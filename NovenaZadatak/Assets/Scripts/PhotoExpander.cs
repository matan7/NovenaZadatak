using UnityEngine;
using UnityEngine.UI;

public class PhotoExpander : PageOpener
{
    public static PhotoExpander Instance;
    [SerializeField] private Image _picture;

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

    public void ExpandPhoto(Sprite sprite)
    {
        _picture.sprite = sprite;
        OpenPage();
    }
}
