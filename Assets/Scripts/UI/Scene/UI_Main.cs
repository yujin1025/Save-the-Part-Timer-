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
        Get<Button>((int)Buttons.Start).gameObject.BindEvent(OnStartButtonClicked);
        Get<Button>((int)Buttons.Settings).gameObject.BindEvent(OnSettingButtonClicked);
        Get<Button>((int)Buttons.RecipeBook).gameObject.BindEvent(OnRecipeButtonClicked);
        Get<GameObject>((int)GameObjects.MainCharacter).gameObject.BindEvent(CharacterClicked);

    }

    void OnResetButtonClicked(PointerEventData data)
    {
        //재개약 Day 정해야함
        Managers.s_managersProperty.dDayProperty = 30;
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
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.RecipeBook);
    }

    void CharacterClicked(PointerEventData data)
    {
        RandomInt = Random.Range(0, 6);

        if (RandomInt == 0)
            Get<Text>((int)Texts.SpeechText).text = "1번 대사";
        else if (RandomInt == 1)
            Get<Text>((int)Texts.SpeechText).text = "2번 대사";
        else if (RandomInt == 2)
            Get<Text>((int)Texts.SpeechText).text = "3번 대사";
        else if (RandomInt == 3)
            Get<Text>((int)Texts.SpeechText).text = "4번 대사";
        else if (RandomInt == 4)
            Get<Text>((int)Texts.SpeechText).text = "5번 대사";
        else if (RandomInt == 5)
            Get<Text>((int)Texts.SpeechText).text = "6번 대사";

    }
}

