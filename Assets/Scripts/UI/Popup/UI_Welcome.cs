using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Welcome : UI_Popup
{
    enum Texts
    {
        WelcomeText,
        UserNameText
    }
    enum Buttons
    {
        ResetButton,
        GameStartButton
    }
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Text>((int)Texts.UserNameText).text = $"{Managers.s_managersProperty.playerNameProperty} 님";
        Get<Button>((int)Buttons.ResetButton).gameObject.BindEvent(OnResetButtonClicked);
        Get<Button>((int)Buttons.GameStartButton).gameObject.BindEvent(OnGameStartButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //로비 인트로를 테스트하기 위한 함수
    void OnResetButtonClicked(PointerEventData data)
    {
        Managers.s_managersProperty.playerNameProperty = "Guest";
        Managers.s_managersProperty.isFirstGameProperty = true;
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Start);
    }

    void OnGameStartButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }
}
