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
    enum Images
    {
        MainCharacter,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Get<Text>((int)Texts.MoneyText).text = $"{Managers.s_managersProperty.moneyProperty.ToString()} 원";
        Get<Text>((int)Texts.DDayText).text = $"D-{Managers.s_managersProperty.dDayProperty.ToString()}";
        Get<Button>((int)Buttons.Reset).gameObject.BindEvent(OnResetButtonClicked);
    }
    void OnResetButtonClicked(PointerEventData data)
    {
        //재개약 Day 정해야함
        Managers.s_managersProperty.dDayProperty = 30;
    }
}
