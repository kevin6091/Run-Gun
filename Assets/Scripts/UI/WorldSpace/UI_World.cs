using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_World : UI_Base
{
    public void Billboard()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
