using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public enum State
    {
    }

    public enum Faction
    {
        Player,
        Enemy,
    }

    public enum EventState
    {
        Enter,
        Stay,
        Exit,
    }

    public enum Sequence
    {
        END,
    }

    public enum Scene
    {
        Unknown,
        Game,
        END,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Down,
        Up,
    }

    public enum InputEvent
    {
        KeyEvent,
        MouseEvent,
        TouchEvent,
        END,
    }

    public enum InputType
    { 
        Down,
        Press,
        Up,
        Drag,
        END,
    }

    public enum CameraMode
    {
        QuarterView_Idle,
        QuarterView_Focus,
        TopView,
        Free,
    }

    public enum Events
    {
        SpawnMonster,
        END,
    }
}

public interface IKeyHandler
{
    public void OnKeyboard();
}