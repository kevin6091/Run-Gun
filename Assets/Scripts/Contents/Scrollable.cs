using System;
using UnityEngine;

public class Scrollable : MonoBehaviour
{
    public Action<Scrollable> removeEvent = null;

    private void OnEnable()
    {
        removeEvent = null;
        ScrollSystem.Instance.AddScrollable(this);
    }

    private void OnDisable()
    {
        removeEvent?.Invoke(this);
    }
}
