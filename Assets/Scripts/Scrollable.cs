using System;
using UnityEngine;

public class Scrollable : MonoBehaviour
{
    private void OnEnable()
    {
        ScrollManager.Instance.AddScrollable(this);
    }

    private void OnDestroy()
    {
        ScrollManager.Instance.RemoveScorllable(this);
    }

    private void OnDisable()
    {
        ScrollManager.Instance.RemoveScorllable(this);
    }
}
