using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameDescription : UI_Popup
{
    enum Buttons
    {
        CloseButton1,
        CloseButton2,
        move1,
        move2
    }

    enum GameObjects
    {
        Ex1,
        Ex2
    }

    GameObject Ex1;
    GameObject Ex2;

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Ex1 = Get<GameObject>((int)GameObjects.Ex1).gameObject;
        Ex2 = Get<GameObject>((int)GameObjects.Ex2).gameObject;

        Ex2.SetActive(false);

        Get<Button>((int)Buttons.CloseButton1).gameObject.BindEvent(OnCloseBtnClicked);
        Get<Button>((int)Buttons.CloseButton2).gameObject.BindEvent(OnCloseBtnClicked);
        Get<Button>((int)Buttons.move1).gameObject.BindEvent(ShowEx2);
        Get<Button>((int)Buttons.move2).gameObject.BindEvent(ShowEx1);
    }

    void Start()
    {
        Init();
    }

    void ShowEx1(PointerEventData data)
    {
        Ex1.SetActive(true);
        Ex2.SetActive(false);
    }

    void ShowEx2(PointerEventData data)
    {
        Ex2.SetActive(true);
        Ex1.SetActive(false);
    }

    void OnCloseBtnClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }
}
