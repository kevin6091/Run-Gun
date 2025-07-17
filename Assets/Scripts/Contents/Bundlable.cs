using System;
using UnityEngine;

public class Bundlable : MonoBehaviour
{
    public Action<Bundlable> removeEvent = null;
    private void OnEnable()
    {
        BundleLoopSystem.Instance.AddBundlable(this);
        removeEvent = null;
    }

    private void OnDestroy()
    {
        removeEvent?.Invoke(this);
    }

    private void OnDisable()
    {
        removeEvent?.Invoke(this);
    }
}
