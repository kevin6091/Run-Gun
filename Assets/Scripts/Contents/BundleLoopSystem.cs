using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;

public class BundleLoopSystem : MonoBehaviour
{
    List<Bundlable> _bundles = new List<Bundlable>();

    [SerializeField] GameObject _rootObject = null;
    [SerializeField] int _numBundle = 3;
    [SerializeField] Vector3 _startPosition = Vector3.zero;
    [SerializeField] GameObject _bundlePrefab = null;
    [SerializeField] float _bundleOffsetZ = 179.9f;

    static private BundleLoopSystem _instance = null;
    static public BundleLoopSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@BundleLoopSystem");
                if (go == null)
                {
                    go = new GameObject { name = "@BundleLoopSystem" };
                }

                _instance = go.GetOrAddComponent<BundleLoopSystem>();
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
        if(_instance == null)
        {
            _instance = this;
        }

        for(int i = 0; i < _numBundle; i++)
        {            
            GameObject go = Managers.Resource.Instantiate("Bundle", _rootObject.transform);
            go.transform.position = _startPosition + (_bundleOffsetZ * Vector3.forward) * i;
            Bundlable bundle = go.GetOrAddComponent<Bundlable>();
            Managers.Event.TriggerEvent(Define.Events.SpawnMonster);
        }
    }

    private void Update()
    {
        Bundlable bundle = _bundles.First<Bundlable>();
        if (bundle == null)
        {
            return;
        }

        Vector3 firstBundlePos = bundle.transform.position;
        float movedDist = (firstBundlePos - _startPosition).magnitude;

        if(movedDist > _bundleOffsetZ)
        {
            bundle.transform.position += (_bundles.Count * _bundleOffsetZ) * Vector3.forward;
            _bundles.Remove(bundle);
            _bundles.Add(bundle);

            Managers.Event.TriggerEvent(Define.Events.SpawnMonster);
        }
    }

    public void AddBundlable(Bundlable bundle)
    {
        if (bundle == null ||
            _bundles.Contains(bundle))
        {
            return;
        }

        _bundles.Add(bundle);
        bundle.removeEvent += RemoveBundlable;
    }

    public void RemoveBundlable(Bundlable bundle)
    {
        if (bundle == null ||
            !_bundles.Contains(bundle))
        {
            return;
        }

        _bundles.Remove(bundle);
    }

    public Vector3 GetLastBundlePosition()
    {
        return _bundles[_bundles.Count - 1].transform.position;
    }
}
