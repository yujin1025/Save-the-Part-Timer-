using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Retire : UI_Popup
{
    enum Buttons
    {
        RetireButton,
        CloseButton
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.RetireButton).gameObject.BindEvent(OnRetireBtnClicked);
        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseBtnClicked);
    }

    void Start()
    {
        Init();
    }

    void OnRetireBtnClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_RetireTalk>();
    }

    void OnCloseBtnClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }

}
