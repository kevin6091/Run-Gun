using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundInfo : MonoBehaviour
{
    private Vector3 _bound = Vector3.zero;
    public Vector3 Bound { get { return _bound; } set { _bound = value; } }
}
