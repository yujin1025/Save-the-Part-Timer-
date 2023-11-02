using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Step1 : UI_Popup
{
    enum Buttons
    {
        Sauce1,
        Sauce2,
        Sauce3,
        Cheese
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
    }
    void Start()
    {
        Init();

    }
    public void OnCloseButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
