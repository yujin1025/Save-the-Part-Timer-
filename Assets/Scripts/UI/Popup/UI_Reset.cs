using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Reset : UI_Popup
{
    enum Buttons
    {
        ResetButton,
        CloseButton
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.ResetButton).gameObject.BindEvent(OnResetBtnClicked);
        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseBtnClicked);
    }

    void Start()
    {
        Init();
    }

    void OnResetBtnClicked(PointerEventData data)
    {
        //게임 초기화
        Managers.s_managersProperty.ResetGameState();
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Lobby);
    }

    void OnCloseBtnClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }
}
