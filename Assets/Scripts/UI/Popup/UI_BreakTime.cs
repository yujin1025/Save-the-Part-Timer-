using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using static UI_BreakTime;

public class UI_BreakTime : UI_Popup
{
    enum GameObjects
    {
        background2,
        Employee,
        Manager,
        Panel,
        Options,
    }

    enum Texts
    {
        TalkText,
        Option1Text,
        Option2Text,
        Option3Text,
    }

    enum Buttons
    {
        GoToBreak,
        GoToWork,
        Option1,
        Option2,
        Option3,
        GoToLounge,
    }

    [System.Serializable]
    public class BreakOptions
    {
        public List<BreakData> breaks;
    }

    [System.Serializable]
    public class BreakData
    {
        public int breakTime;
        public string option;
        public string dialog;
        public int price;
        public int stress;
    }

    //moneyProperty 전체 가격

    //BreakOption의 데이터들
    List<BreakData> BreakDatas = new List<BreakData>();

    List<BreakData> ChosenData;

    UI_Game ui_game;


    public override void Init()
    {
        Time.timeScale = 0.0f; //stop game going

        ui_game = FindObjectOfType<UI_Game>();

        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<GameObject>((int)GameObjects.background2).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Employee).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToWork).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToLounge).gameObject.SetActive(false);
        Get<Text>((int)Texts.TalkText).text = Managers.s_managersProperty.playerNameProperty + "씨는 쉬고 오세요";

        Get<Button>((int)Buttons.GoToBreak).gameObject.BindEvent(MoveToBreak);
        Get<Button>((int)Buttons.GoToWork).gameObject.BindEvent(MoveToWork);

        Get<Button>((int)Buttons.Option1).gameObject.BindEvent(Option1Clicked);
        Get<Button>((int)Buttons.Option2).gameObject.BindEvent(Option2Clicked);
        Get<Button>((int)Buttons.Option3).gameObject.BindEvent(Option3Clicked);
        Get<Button>((int)Buttons.GoToLounge).gameObject.BindEvent(GoToLoungeClicked);


        // Resources 폴더에 있는 BreakOption.json 파일 로드
        TextAsset jsonFile = Resources.Load<TextAsset>("BreakOption");

        if (jsonFile != null)
        {
            string jsonContent = jsonFile.text;
            // JSON을 BreakData 리스트로 변환
            BreakOptions breakOptions = JsonUtility.FromJson<BreakOptions>(jsonContent);
            BreakDatas = breakOptions.breaks;
        }

        ChosenData = RandomOptions(3);

        Get<Text>((int)Texts.Option1Text).text = ChosenData[0].option +
            "\n(가격: "+ ChosenData[0].price + "원, 스트레스 감소도: " + ChosenData[0].stress + ")";
        Get<Text>((int)Texts.Option2Text).text = ChosenData[1].option +
            "\n(가격: " + ChosenData[1].price + "원, 스트레스 감소도: " + ChosenData[1].stress + ")";
        Get<Text>((int)Texts.Option3Text).text = ChosenData[2].option +
            "\n(가격: "+ ChosenData[2].price + "원, 스트레스 감소도: " + ChosenData[2].stress + ")";

        
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    
    //선택지 20개 중 3개 중복 없이 배열에 넣음
    List<BreakData> RandomOptions(int count)
    {
        List<BreakData> AllOptions = new List<BreakData>(BreakDatas);
        List<BreakData> ChosenOptions = new List<BreakData>();

        while (ChosenOptions.Count < count)
        {
            int randomIndex = Random.Range(0, AllOptions.Count);
            ChosenOptions.Add(AllOptions[randomIndex]);
            AllOptions.RemoveAt(randomIndex);
            Debug.Log($"AllOptions.Count: {AllOptions.Count}");
        }

        return ChosenOptions;
    }

    void MoveToBreak(PointerEventData data)
    {
        Get<GameObject>((int)GameObjects.background2).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Employee).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Manager).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToBreak).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToLounge).gameObject.SetActive(true);
        Get<Text>((int)Texts.TalkText).text =
            "쉬는 시간 동안 뭘 하지? \n" +
            "- 직원 휴게실에서 쉬기 \n" +
            "(가격 : 0원, 스트레스 감소도 : 0)";

        Debug.Log("현재 돈" + Managers.s_managersProperty.moneyProperty);
    }

    void MoveToWork(PointerEventData data)
    {
        ui_game.gameObject.GetComponentInChildren<CountDown>().Init();
        ui_game.UpdateMoney();
        Time.timeScale = 1.0f; // game going
        Managers.uiManagerProperty.ClosePopupUIOnTop();

    }

    void Option1Clicked(PointerEventData data)
    {
        //잔액 부족시 대사만 바뀜
        Get<Text>((int)Texts.TalkText).text =
            "돈이 부족해... \n" +
            "- 직원 휴게실에서 쉬기 \n" +
            "(가격 : 0원, 스트레스 감소도 : 0)";

        // 잔액이 충분한경우 금액 차감, 스트레스 감소
        if (Managers.s_managersProperty.moneyProperty >= ChosenData[0].price)
        {
            GoToWork(0);
            Managers.s_managersProperty.moneyProperty -= ChosenData[0].price;
            UI_Game.FindObjectOfType<UI_GaugeBar>().GaugeCurrentDown(ChosenData[0].price);
            Debug.Log("현재 돈" + Managers.s_managersProperty.moneyProperty);
        }
    }

    void Option2Clicked(PointerEventData data)
    {
        //잔액 부족시 대사만 바뀜
        Get<Text>((int)Texts.TalkText).text =
            "돈이 부족해... \n" +
            "- 직원 휴게실에서 쉬기 \n" +
            "(가격 : 0원, 스트레스 감소도 : 0)";

        // 잔액이 충분한경우 금액 차감, 스트레스 감소
        if (Managers.s_managersProperty.moneyProperty >= ChosenData[1].price)
        {
            GoToWork(1);
            Managers.s_managersProperty.moneyProperty -= ChosenData[1].price;
            ui_game.GetComponentInChildren<UI_GaugeBar>().GaugeCurrentDown(ChosenData[1].price);
            Debug.Log("현재 돈" + Managers.s_managersProperty.moneyProperty);
        }
    }

    void Option3Clicked(PointerEventData data)
    {
        //잔액 부족시 대사만 바뀜
        Get<Text>((int)Texts.TalkText).text =
            "돈이 부족해... \n" +
            "- 직원 휴게실에서 쉬기 \n" +
            "(가격 : 0원, 스트레스 감소도 : 0)";

        // 잔액이 충분한경우 금액 차감, 스트레스 감소
        if (Managers.s_managersProperty.moneyProperty >= ChosenData[2].price)
        {
            GoToWork(2);
            Managers.s_managersProperty.moneyProperty -= ChosenData[2].price;
            ui_game.GetComponentInChildren<UI_GaugeBar>().GaugeCurrentDown(ChosenData[2].price);
            Debug.Log("현재 돈" + Managers.s_managersProperty.moneyProperty);
        }
    }

    void GoToLoungeClicked(PointerEventData data)
    {
        GoToWork(3);
    }

    
    void GoToWork(int index)
    {
        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(false);
        if (index < 3)
            Get<Text>((int)Texts.TalkText).text = ChosenData[index].dialog;
        else
            Get<Text>((int)Texts.TalkText).text = " "; //대사 아직 안나옴
        Get<Button>((int)Buttons.GoToWork).gameObject.SetActive(true);
        Get<Button>((int)Buttons.GoToLounge).gameObject.SetActive(false);
    }

}