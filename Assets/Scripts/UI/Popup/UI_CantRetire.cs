using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CantRetire : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseBtnClicked);
    }

    void Start()
    {
        Init();
    }

    void OnCloseBtnClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }
}
