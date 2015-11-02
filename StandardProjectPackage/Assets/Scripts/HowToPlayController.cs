using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    public Image ImageHolder;
    public GameObject OnCloseObject;
    public Button NextButton;
    public Button BackButton;

    private int _index = 0;
    private GameObject[] _pages;

    void Start()
    {
        var children = new List<GameObject>();

        for (var i = 0; i < ImageHolder.transform.childCount; i++)
        {
            children.Add(ImageHolder.transform.GetChild(i).gameObject);
        }

        _pages = children.ToArray();

        RefreshView();
    }

    public void RefreshView()
    {
        NextButton.enabled = _index < _pages.Length - 1;
        BackButton.enabled = _index > 0;

        for (var p = 0; p < _pages.Length; p++)
        {
            _pages[p].SetActive(p == _index);
        }
    }

    public void OnClose()
    {
        _index = 0;
        RefreshView();
        gameObject.SetActive(false);
        OnCloseObject.SetActive(true);
    }

    public void OnNext()
    {
        _index++;
        RefreshView();
    }

    public void OnBack()
    {
        _index--;
        RefreshView();
    }
}
