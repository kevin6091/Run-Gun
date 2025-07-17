using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : BaseManager
{
    public Dictionary<Define.InputType, Action<Vector2>> Actions { get; set; } = new Dictionary<Define.InputType, Action<Vector2>>();

    public Action<Vector3> MyJoyStickHandler = null;

    public override void Init()
    {
        for (int i = 0; i < (int)Define.InputType.END; i++)
        {
            Actions.Add((Define.InputType)i, null);
        }
    }

    public override void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Actions[Define.InputType.Down] != null)
                {
                    Actions[Define.InputType.Down]?.Invoke(Vector2.zero);
                }
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (Actions[Define.InputType.Press] != null)
                {
                    Actions[Define.InputType.Press]?.Invoke(touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (Actions[Define.InputType.Up] != null)
                {
                    Actions[Define.InputType.Up]?.Invoke(Vector2.zero);
                }
            }
        }
    }

    public override void Clear()
    {
        foreach (var key in Actions.Keys.ToList())
        {
            Actions[key] = null;
        }
        Actions.Clear();
    }
}
