using System;
using UnityEngine;

public class Scrollable : MonoBehaviour
{
    public Action<Scrollable> removeEvent = null;

    private void OnEnable()
    {
        ScrollSystem.Instance.AddScrollable(this);
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
