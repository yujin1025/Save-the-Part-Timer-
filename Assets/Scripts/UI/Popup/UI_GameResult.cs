using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameResult : UI_Popup
{

    enum Buttons
    {
        GoToMain,
    }
    enum Texts
    {
        TalkText,
    }
    enum GameObjects
    {
        Manager,
        Employee,
    }

    UI_Game ui_game;

    bool hasClicked = false;
    int incentive = 0;
    int salary = 200000;

    public override void Init()
    {
        base.Init();

        ui_game = transform.parent.gameObject.GetComponentInChildren<UI_Game>();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        Get<Button>((int)Buttons.GoToMain).gameObject.BindEvent(MoveToMain);
        Get<Text>((int)Texts.TalkText).text = "드디어... 퇴근이다!\r\n오늘도 잘 버텼다!";

        string stageNameNow = Managers.s_managersProperty.stageNameStrProperty;
        int stageNumNow;
        string pattern = @"\d+";

        Match match = Regex.Match(stageNameNow, pattern);
        if (match.Success)
        {
            stageNumNow = int.Parse(match.Value);
        }
        else stageNumNow = 1;


        bool[] avaliableNow = Managers.GetAvaliablePizzaInBool(stageNameNow);
        if (stageNameNow != "stage 30")
        {
            bool[] abaliableNext = Managers.GetAvaliablePizzaInBool($"stage {stageNumNow + 1}");

            bool[] newAvailablePizza = new bool[9];

            for (int i = 0; i < 9; i++) newAvailablePizza[i] = false;

            for (int i = 0; i < 9; i++)
            {
                if ((avaliableNow[i] == false) && (abaliableNext[i] == true)) newAvailablePizza[i] = true;
            }

            Debug.Log("신규 피자");
            for (int i = 0; i < 9; i++)
            {
                if (newAvailablePizza[i] == true) Debug.Log(Managers.pizzaDec[i]);
            }
        }
    }

    void MoveToMain(PointerEventData data)
    {
        Managers.s_managersProperty.moneyProperty += incentive;
        Managers.s_managersProperty.dDayProperty++;
        if (Managers.s_managersProperty.dDayProperty <= 30) Managers.s_managersProperty.stageNameStrProperty = $"stage {Managers.s_managersProperty.dDayProperty}";
        else Managers.s_managersProperty.stageNameStrProperty = $"stage 30";

        Debug.Log($"PlayerName: {Managers.s_managersProperty.playerNameProperty}, " +
            $"Money: {Managers.s_managersProperty.moneyProperty}, " +
            $"DDay: {Managers.s_managersProperty.dDayProperty}, " +
            $"StageNameStr: {Managers.s_managersProperty.stageNameStrProperty}");

        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);

    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int money = ui_game.earnedMoney;
            incentive = (int)(money * 0.1f);

            
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
            

            if (Managers.s_managersProperty.dDayProperty == 1 || Managers.s_managersProperty.dDayProperty == 6 ||
                Managers.s_managersProperty.dDayProperty == 11 || Managers.s_managersProperty.dDayProperty == 16 ||
                Managers.s_managersProperty.dDayProperty == 5 || Managers.s_managersProperty.dDayProperty == 10 ||
                Managers.s_managersProperty.dDayProperty == 15 || Managers.s_managersProperty.dDayProperty == 20 ||
                Managers.s_managersProperty.dDayProperty == 25 || Managers.s_managersProperty.dDayProperty == 30)
            {
                if(hasClicked)
                {
                    if (Managers.s_managersProperty.dDayProperty == 5 || Managers.s_managersProperty.dDayProperty == 10 ||
                  Managers.s_managersProperty.dDayProperty == 15 || Managers.s_managersProperty.dDayProperty == 20 ||
                  Managers.s_managersProperty.dDayProperty == 25 || Managers.s_managersProperty.dDayProperty == 30)
                    {
                        Get<Text>((int)Texts.TalkText).text = "그리고 이번 달 월급 20만 원입니다.";
                        Managers.s_managersProperty.moneyProperty +=salary;

                        if(Managers.s_managersProperty.dDayProperty == 30)
                        {
                            Managers.s_managersProperty.moneyProperty += incentive;
                            Managers.s_managersProperty.dDayProperty++;
                            Managers.s_managersProperty.stageNameStrProperty = $"stage 30";

                            Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
                            Managers.uiManagerProperty.ShowPopupUI<UI_Ending>();
                        }
                            
                    }

                    else if (Managers.s_managersProperty.dDayProperty == 1)
                    {
                        Get<Text>((int)Texts.TalkText).text = "다음부턴 '베이컨포테이토', '페퍼로니&베이컨포테이토' 피자를 만들어보죠. 레시피 외워오세요.";
                    }

                    else if (Managers.s_managersProperty.dDayProperty == 6)
                    {
                        Get<Text>((int)Texts.TalkText).text = "다음부턴 '불고기', '베이컨포테이토&불고기' 피자를 만들어보죠. 레시피 외워오세요.";
                    }

                    else if (Managers.s_managersProperty.dDayProperty == 11)
                    {
                        Get<Text>((int)Texts.TalkText).text = "다음부턴 '옥수수', '불고기&옥수수' 피자를 만들어보죠. 레시피 외워오세요.";
                    }

                    else if (Managers.s_managersProperty.dDayProperty == 16)
                    {
                        Get<Text>((int)Texts.TalkText).text = "마지막으로 '콤비네이션' 피자를 만들어보죠. 레시피 외워오세요.";
                    }
                    Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(true);
                }
                else
                {
                    Get<Text>((int)Texts.TalkText).text = $"오늘 매출은 {money}원이네요. 여기 인센티브 {incentive}원입니다. " +
                        "\r\n수고했어요. 그럼 다음 근무 때 뵙죠.";
                    hasClicked = true;
                }
            }

            else
            {
                Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(true);
                Get<Text>((int)Texts.TalkText).text = $"오늘 매출은 {money}원이네요. 여기 인센티브 {incentive}원입니다. " +
                "\r\n수고했어요. 그럼 다음 근무 때 뵙죠.";
            }
        }
    }
}
