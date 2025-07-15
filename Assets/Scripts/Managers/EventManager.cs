using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEvent
{
    public Define.EventState EventState { get; set; } = Define.EventState.Exit;

    public Action<Define.EventState> Action = null;
    public Func<bool> Trigger { get; set; } = null;
}

public class EventManager : BaseManager
{
    private Dictionary<Define.Events, AutoEvent> _autoTriggerDict = new Dictionary<Define.Events, AutoEvent>();
    private Dictionary<Define.Events, Action> _manualTriggerDict = new Dictionary<Define.Events, Action>();
    private Dictionary<Define.Events, Delegate> _paramTriggerDict = new Dictionary<Define.Events, Delegate>();

    public void Subscribe(Define.Events eventType, Action listener)
    {
        _manualTriggerDict[eventType] -= listener;
        _manualTriggerDict[eventType] += listener;
    }

    public void Subscribe(Define.Events eventType, Action<Define.EventState> listener)
    {
        _autoTriggerDict[eventType].Action -= listener;
        _autoTriggerDict[eventType].Action += listener;
    }

    public void Subscribe(Define.Events eventType, Delegate listener)
    {
        if (_paramTriggerDict.ContainsKey(eventType))
        {
            _paramTriggerDict[eventType] = Delegate.Combine(_paramTriggerDict[eventType], listener);
        }
        else
        {
            _paramTriggerDict[eventType] = listener;
        }
    }

    public void Unsubscribe(Define.Events eventType, Action listener)
    {
        _manualTriggerDict[eventType] -= listener;
    }

    public void Unsubscribe(Define.Events eventType, Action<Define.EventState> listener)
    {
        _autoTriggerDict[eventType].Action -= listener;
    }

    public void Unsubscribe(Define.Events eventType, Delegate listener)
    {
        Delegate cur = _paramTriggerDict[eventType];
        cur = Delegate.Remove(cur, listener);
        _paramTriggerDict[eventType] = cur;
    }

    public void SetTriggerAuto(Define.Events eventType, Func<bool> trigger)
    {
        _autoTriggerDict[eventType].Trigger = trigger;
    }

    public void TriggerEvent(Define.Events eventType)
    {
        _manualTriggerDict[eventType]?.Invoke();
    }

    public void TriggerEvent<T>(Define.Events eventType, T param)
    {
        if (_paramTriggerDict.TryGetValue(eventType, out Delegate del))
        {
            Action<T> action = del as Action<T>;
            if (action != null)
            {
                action.Invoke(param);
            }
            else
            {
                Debug.LogError($"이벤트 {eventType}의 타입이 일치하지 않습니다! 기대: Action<{typeof(T).Name}>");
            }
        }
    }

    public override void Init()
    {
        base.Init();

        for(int i = 0; i< (int)Define.Events.END; ++i)
        {
            _autoTriggerDict.Add((Define.Events)i, new AutoEvent());
            _manualTriggerDict.Add((Define.Events)i, null);
            _paramTriggerDict.Add((Define.Events)i, null);
        }

        object obj = new AutoEvent();
        Type type = typeof(AutoEvent);

        var info = Convert.ChangeType(obj, type);
    }

    public override void OnUpdate()
    {
        foreach (AutoEvent autoEvent in _autoTriggerDict.Values)
        {
            if(autoEvent.Trigger == null)
            {
                continue;
            }

            if(autoEvent.Trigger())
            {
                if(autoEvent.EventState == Define.EventState.Exit)
                {
                    autoEvent.Action?.Invoke(Define.EventState.Enter);
                    autoEvent.EventState = Define.EventState.Enter;
                }

                else
                {
                    autoEvent.Action?.Invoke(Define.EventState.Stay);
                    autoEvent.EventState = Define.EventState.Stay;
                }
            }

            else
            {
                if (autoEvent.EventState == Define.EventState.Enter ||
                    autoEvent.EventState == Define.EventState.Stay)
                {
                    autoEvent.Action?.Invoke(Define.EventState.Exit);
                    autoEvent.EventState = Define.EventState.Exit;
                }
            }
        }
    }

    public override void Clear()
    {
        base.Clear();

        for(int i = 0; i < (int)Define.Events.END; ++i)
        {
            AutoEvent autoEvent = _autoTriggerDict[(Define.Events)i];
            autoEvent.Action = null;
            autoEvent.Trigger = null;
            autoEvent.EventState = Define.EventState.Exit;

            _manualTriggerDict[(Define.Events)i] = null;
        }
    }
}
