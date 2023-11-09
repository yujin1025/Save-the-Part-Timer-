using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    public enum GameObjects
    {
        Background,
        CustomerPanel,
        StressBar,
        UI_Step1,
    }
    enum Texts
    {
        RemainingTime,
        RemainingMoney,
    }
    enum Buttons
    {
        Settings,
        NextButton
    }

    UI_Step1 ui_step1;

    public string orderName;
    
    public bool[] sauceAnswer;


    void Start()
    {
        Init();
    }

    
    public override void Init()
    {
        base.Init();
        sauceAnswer = new bool[3];

        ui_step1 = Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step1>(gameObject);
        ui_step1.gameObject.name = "UI_Step1";

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Text>((int)Texts.RemainingMoney).text = $"잔고 : {Managers.s_managersProperty.moneyProperty.ToString()}";


        Get<Button>((int)Buttons.NextButton).gameObject.BindEvent(OnNextButtonClicked);
        orderName = "None";
    }

    void OnNextButtonClicked(PointerEventData data)
    {
        if (ui_step1.isCheeseSelcetDone && ui_step1.isSauceSelectDone)
        {
            ui_step1.Get<GameObject>((int)UI_Step1.GameObjects.CheeseBlocker).gameObject.SetActive(false);
            ui_step1.Get<GameObject>((int)UI_Step1.GameObjects.SauceBlocker).gameObject.SetActive(true);
            ui_step1.Get<GameObject>((int)UI_Step1.GameObjects.SauceBar).gameObject.SetActive(true);
        }
        else Debug.Log("소스와 치즈를 뿌려주세요");
    }
}
