using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadgeStateManager : MonoBehaviour
{
    public static PadgeStateManager Instance { get; private set; }

    private PageOpener _currentPage;

    [Header("UI")]
    [SerializeField] private PageOpener _home;
    [SerializeField] private PageOpener _page1;
    [SerializeField] private PageOpener _page2;

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

        _currentPage = _home;
    }

    public void OpenHome()
    {
        ChangePadge(PageState.Home);
    }

    public void OpenPage1()
    {
        ChangePadge(PageState.Padge1);
    }

    public void OpenPage2()
    {
        ChangePadge(PageState.Padge2);
    }

    private void ChangePadge(PageState padge)
    {
        _currentPage.ClosePage();
        switch (padge)
        {
            case PageState.Home:
                _home.OpenPage();
                _currentPage = _home;
                break;
            case PageState.Padge1:
                _page1.OpenPage();
                _currentPage = _page1;
                break;
            case PageState.Padge2:
                _page2.OpenPage();
                _currentPage = _page2;
                break;
        }
    }
}

public enum PageState
{
    Home,
    Padge1,
    Padge2
}