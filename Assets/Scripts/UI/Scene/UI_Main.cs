using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum GameObjects
    {
        Background,
        MainCharacter,
    }
    enum Texts
    {
        SpeechText,
        MoneyText,
        DDayText,
    }
    enum Buttons
    {
        Settings,
        Reset,
        Retire,
        Start,
        RecipeBook,
    }

    void Start()
    {
        Init();
        CharacterClicked(null);
    }

    private int RandomInt;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<Text>((int)Texts.MoneyText).text = $"{Managers.s_managersProperty.moneyProperty.ToString()} 원";
        Get<Text>((int)Texts.DDayText).text = $"D+{Managers.s_managersProperty.dDayProperty.ToString()}";
        Get<Button>((int)Buttons.Reset).gameObject.BindEvent(OnResetButtonClicked);
        Get<Button>((int)Buttons.Retire).gameObject.BindEvent(OnRetireButtonClicked);
        Get<Button>((int)Buttons.Start).gameObject.BindEvent(OnStartButtonClicked);
        Get<Button>((int)Buttons.Settings).gameObject.BindEvent(OnSettingButtonClicked);
        Get<Button>((int)Buttons.RecipeBook).gameObject.BindEvent(OnRecipeButtonClicked);
        Get<GameObject>((int)GameObjects.MainCharacter).gameObject.BindEvent(CharacterClicked);
    }

    void OnResetButtonClicked(PointerEventData data)
    {
        if (Managers.s_managersProperty.dDayProperty >= 30)
            Managers.uiManagerProperty.ShowPopupUI<UI_Reset>();
        else
            Managers.uiManagerProperty.ShowPopupUI<UI_CantReset>();
    }

    void OnRetireButtonClicked(PointerEventData data)
    {
        //퇴사하기 최초 열람시
        if (Managers.s_managersProperty.isFirstRetireProperty)
        {
            if (Managers.s_managersProperty.moneyProperty >= 990000)
                Managers.uiManagerProperty.ShowPopupUI<UI_Retire>();
            else
                Managers.uiManagerProperty.ShowPopupUI<UI_CantRetire>();
        }
        
        else
            Managers.uiManagerProperty.ShowPopupUI<UI_RetireTalk>();
    }

    void OnStartButtonClicked(PointerEventData data)
    {
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Game);
    }
    void OnSettingButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_Settings>();
    }

    void OnRecipeButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_RecipeBook>();
    }

    void CharacterClicked(PointerEventData data)
    {
        RandomInt = UnityEngine.Random.Range(0, 6);

        if (RandomInt == 0)
            Get<Text>((int)Texts.SpeechText).text = "거친 노동의 세계...";
        else if (RandomInt == 1)
            Get<Text>((int)Texts.SpeechText).text = "세상아 망해라";
        else if (RandomInt == 2)
            Get<Text>((int)Texts.SpeechText).text = "일하기 싫어!";
        else if (RandomInt == 3)
            Get<Text>((int)Texts.SpeechText).text = "자본주의로부터의 \n자유를 꿈꾸다";
        else if (RandomInt == 4)
            Get<Text>((int)Texts.SpeechText).text = "어제 산 복권도 꽝이야";
        else if (RandomInt == 5)
            Get<Text>((int)Texts.SpeechText).text = "출근은 무슨...";

    }
}

