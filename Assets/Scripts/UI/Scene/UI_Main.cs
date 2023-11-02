using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum GameObjects
    {
        Background,
    }
    enum Texts
    {
        RemainingTime,
        RemainingMoney,
        SpeechText,
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
    }
}
