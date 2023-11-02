using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    enum GameObjects
    {
        Background,
        CustomerPanel,
        StressBar,
    }
    enum Texts
    {
        RemainingTime,
        RemainingMoney,
    }
    enum Buttons
    {
        Settings,
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

        Managers.uiManagerProperty.ShowPopupUI<UI_Step1>();
    }
}
