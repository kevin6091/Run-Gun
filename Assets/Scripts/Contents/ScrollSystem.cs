using NUnit.Framework;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class ScrollSystem : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 2f;
    [SerializeField] Vector3 _scrollDir = Vector3.forward * -1f;

    private List<Scrollable> _scrolls = new List<Scrollable>();

    static private ScrollSystem _instance = null;
    static public ScrollSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@ScrollSystem");
                if (go == null)
                {
                    go = new GameObject { name = "@ScrollSystem" };
                }

                _instance = go.GetOrAddComponent<ScrollSystem>();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    void Update()
    {
        foreach (Scrollable scrollable in _scrolls)
        {
            scrollable.transform.position += _scrollSpeed * Time.deltaTime * _scrollDir;
        }
    }

    public void AddScrollable(Scrollable scroll)
    {
        if (scroll == null ||
            _scrolls.Contains(scroll))
            return;

        _scrolls.Add(scroll);
        scroll.removeEvent += RemoveScorllable;
    }

    public void RemoveScorllable(Scrollable scrollable)
    {
        if (scrollable == null ||
            !_scrolls.Contains(scrollable))
            return;

        _scrolls.Remove(scrollable);
    }
}
