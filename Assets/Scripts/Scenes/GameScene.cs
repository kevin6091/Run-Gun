using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameScene : BaseScene
{
    //PlayerController _gamePlayer;
    //public PlayerController GamePlayer 
    //{ 
    //    get 
    //    { 
    //        if (_gamePlayer == null)
    //        {
    //            _gamePlayer = GameObject.Find("Player").GetComponent<PlayerController>();
    //        }

    //        return _gamePlayer; 
    //    }
    //} 

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
    }

    public override void Clear()
    {

    }

    private void Update() // Test Update
    {
    }
}
