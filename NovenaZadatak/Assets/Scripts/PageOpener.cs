using System.Collections;
using UnityEngine;

public class PageOpener : MonoBehaviour
{
    [SerializeField] private CanvasGroup _myCanvasGroup;

    private Coroutine _transitionCRT;

    public void OpenPage()
    {
        if (_transitionCRT is null)
            _transitionCRT = StartCoroutine(FadeInOut(true));
        else
        {
            StopCoroutine(_transitionCRT);
            _transitionCRT = StartCoroutine(FadeInOut(true));
        }
    }

    public void ClosePage()
    {
        if (_transitionCRT is null)
            _transitionCRT = StartCoroutine(FadeInOut(false));
        else
        {
            StopCoroutine(_transitionCRT);
            _transitionCRT = StartCoroutine(FadeInOut(false));
        }
    }

    private IEnumerator FadeInOut(bool isFadeIn)
    {
        float transitionSec = 1f;
        if (isFadeIn)
        {
            var t = transitionSec;
            while (t > 0)
            {
                t -= Time.deltaTime;
                _myCanvasGroup.alpha = 1f - (t / transitionSec);
                yield return null;
            }
            _myCanvasGroup.alpha = 1;
            _myCanvasGroup.interactable = true;
            _myCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            var t = transitionSec;
            while (t > 0)
            {
                t -= Time.deltaTime;
                _myCanvasGroup.alpha = t / transitionSec;
                yield return null;
            }
            _myCanvasGroup.alpha = 0;
            _myCanvasGroup.interactable = false;
            _myCanvasGroup.blocksRaycasts = false;
        }
        _transitionCRT = null;
    }
}