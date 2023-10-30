using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Step3 : UI_Popup
{
    enum Buttons
    {
        Ingredient1,
        Ingredient2,
        Ingredient3,
        Ingredient4,
        Ingredient5,
        Ingredient6,
        Ingredient7,
        Ingredient8,
        Ingredient9

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
