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
        Pizza,
        SauceLayer,
        TomatoSauceLayer,
        MayonnaiseSauceLayer,
        OnionSauceLayer,
        CheeseLayer,
        Ingredients,
    }
    enum Texts
    {
        RemainingTime,
        RemainingMoney,
    }
    public enum Buttons
    {
        SettingButton,
        NextButton
    }
    public enum Images
    {
        NextStepReadyImage
    }

    public string stageNameStr;
    public UI_Step1 ui_step1;
    public UI_Step3 ui_step3;
    public UI_Customer selectedCustomer;

    public bool pizzaMakingOnGoing;
    public bool alreadyRested;

    public string orderName;
    
    public bool[] sauceAnswer;

    public string[] ingredientOrder;

    public int earnedMoney = 0;

    void Start()
    {
        Init();
    }

    
    public override void Init()
    {
        base.Init();
        sauceAnswer = new bool[3];
        pizzaMakingOnGoing = false;
        alreadyRested = false;

        ui_step1 = Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step1>(gameObject);

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Text>((int)Texts.RemainingMoney).text = $"잔고 : {Managers.s_managersProperty.moneyProperty.ToString()}";
        Get<Button>((int)Buttons.NextButton).gameObject.BindEvent(OnNextButtonClicked);
        orderName = "None";

        Get<GameObject>((int)GameObjects.TomatoSauceLayer).SetActive(false);
        Get<GameObject>((int)GameObjects.MayonnaiseSauceLayer).SetActive(false);
        Get<GameObject>((int)GameObjects.OnionSauceLayer).SetActive(false);
        Get<GameObject>((int)GameObjects.CheeseLayer).SetActive(false);

        CountDown countDown = Get<Text>((int)Texts.RemainingTime).gameObject.GetComponent<CountDown>();
        countDown.CountdownFinished -= HandleCountdownFinished;
        countDown.CountdownFinished += HandleCountdownFinished;

        UI_GaugeBar gaugeBar = Get<GameObject>((int)GameObjects.StressBar).GetComponent<UI_GaugeBar>();
        gaugeBar.GaugeIsMax -= HandleStressGauge;
        gaugeBar.GaugeIsMax += HandleStressGauge;

        Get<Image>((int)Images.NextStepReadyImage).gameObject.SetActive(false);
        Get<Button>((int)Buttons.NextButton).gameObject.SetActive(false);
    }


    void HandleCountdownFinished()
    {
        if(alreadyRested) Managers.uiManagerProperty.ShowPopupUI<UI_GameResult>();
        else
        {
            this.alreadyRested = true;
            Managers.uiManagerProperty.ShowPopupUI<UI_BreakTime>();
        }
    }
    public void UpdateMoney()
    {
        Get<Text>((int)Texts.RemainingMoney).text = $"잔고 : {Managers.s_managersProperty.moneyProperty.ToString()}";
    }


    void HandleStressGauge()
    {
        Time.timeScale = 0.0f; //stop game going
        Managers.uiManagerProperty.ShowPopupUI<UI_GameOver>();
    }

    void OnNextButtonClicked(PointerEventData data)
    {
        if (ui_step1 != null)
        {
            if (ui_step1.isCheeseSelcetDone && ui_step1.isSauceSelectDone)
            {
                Managers.uiManagerProperty.SafeClosePopupUIOnTop(ui_step1);
                ui_step1 = null;
                ui_step3 = Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step3>(gameObject);
                Get<Image>((int)Images.NextStepReadyImage).gameObject.SetActive(false);
                Get<Button>((int)Buttons.NextButton).gameObject.SetActive(false);
            }
        }
        else if(ui_step3 != null)
        {
            ui_step3.BackToStep1();
            ui_step3 = null;
            Get<Image>((int)Images.NextStepReadyImage).gameObject.SetActive(false);
            Get<Button>((int)Buttons.NextButton).gameObject.SetActive(false);
        }
    }
}
