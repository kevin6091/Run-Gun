using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 2f;
    [SerializeField] Vector3 _scrollDir = Vector3.forward * -1f;

    private List<Scrollable> _scrollObjects = null;

    static private ScrollManager _instance = null;
    static public ScrollManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@ScrollManager");
                if (go == null)
                {
                    go = new GameObject { name = "@ScrollManager" };
                }

                _instance = go.GetOrAddComponent<ScrollManager>();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        _instance = this;
        _scrollObjects = new List<Scrollable>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Scrollable scrollable in _scrollObjects)
        {
            scrollable.transform.position += _scrollSpeed * Time.deltaTime * _scrollDir;   
        }
    }

    public void AddScrollable(Scrollable scroll)
    {
        _scrollObjects.Add(scroll);
    }

    public void RemoveScorllable(Scrollable scrollable)
    {
        _scrollObjects.Remove(scrollable);
    }
}
