using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditController : MonoBehaviour
{
    public float ItemDelay = 1f;
    public float ItemSpeed = 1f;
    public bool Pause = false;
    public float ItemOffset = 50f;

    private Queue<GameObject> _creditItems = new Queue<GameObject>();
    private List<GameObject> _activeItems = new List<GameObject>();
    private float _currentItemDelay = 0f;
    private RectTransform _rect;
    private GameObject _lastItem;
    private float _lastItemSize = 0f;
    private float _stoppoint = 0f;

    void Start()
    {
        _rect = GetComponent<RectTransform>();

        for (var i = 0; i < transform.childCount; i++)
        {
            var item = transform.GetChild(i).gameObject;
            _creditItems.Enqueue(item);
            item.SetActive(false);
        }

        _stoppoint = _rect.rect.height/2*-1;
    }

    void Update()
    {
        if (Pause)
            return;

        if (_lastItemSize <= 0)
        {
            _currentItemDelay -= Time.deltaTime;
        }
        else
        {
            _lastItemSize -= Time.deltaTime * ItemSpeed;
        }
        

        foreach (var item in _activeItems)
        {
            var rect = item.GetComponent<RectTransform>();
            var newy = rect.anchoredPosition.y + (Time.deltaTime*ItemSpeed);

            if (item == _lastItem && newy > _stoppoint)
            {
                newy = _stoppoint;
                Pause = true;
            }

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newy);
        }

        if (_currentItemDelay <= 0 && _creditItems.Count > 0)
        {
            _currentItemDelay = ItemDelay;
            var item = _creditItems.Dequeue();
            item.SetActive(true);
            var rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, _rect.rect.height * -1 - ItemOffset);
            _activeItems.Add(item);

            _lastItemSize = rect.rect.height;

            if (_creditItems.Count == 0)
                _lastItem = item;
        }
    }

    public void OnReturnToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
