using System;
using TMPro;
using UnityEngine;

public class UI_Itemtext : UI_World
{
    enum TMPs
    {
        Text,
    }

    public string text
    {
        get { return GetTMP((int)TMPs.Text).text; }
        set { GetTMP((int)TMPs.Text).text = value; }
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(TMPs));
    }

    public override void Init()
    {
        
    }
}
