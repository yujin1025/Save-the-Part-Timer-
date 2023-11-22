using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PressToStart : UI_Popup
{
    // Start is called before the first frame update
    enum Images
    {
        BackGround,
    }
    enum Texts
    {
        PressToStart,
    }
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Get<Image>((int)Images.BackGround).gameObject.BindEvent(OnBackGroundClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void OnBackGroundClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
        if (Managers.s_managersProperty.isFirstGameProperty == false)
        {
            UI_Popup tmp = Managers.uiManagerProperty.ShowPopupUI<UI_Welcome>();
        }
        else
        {
            Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Lobby);
        }
    }
}
