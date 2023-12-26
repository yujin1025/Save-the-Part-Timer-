using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ending : UI_Popup
{
    float delayTime = 3f; 
    float timer = 0f;
    int index = 0;

    private string[] talkData = {
        "그리고 이번 달 월급 20만 원입니다.",
        "오늘이 마지막 근무일이네요. 그동안 수고 많았어요.",
        "아... 그러게요... 벌써 3개월이...",
        "(그동안의 일들이 주마등처럼 머릿속을 스친다)",
        "“드디어..! 대학생이다!”",
        "“이 거지같은 자본주의 세상…. 돈을 쓰고 싶다면 돈을 벌어야 하는 거겠지…. 그래, 알바! 알바를 하자!”",
        "“당장 이번주부터 나오면 됩니다. 대신 최소 3개월은 일해야 합니다. 가능하겠어요?”",
        "(내 빈 통장 잔액을 채우기 위해서라면 못할 게 없다!) 네, 네...!",
        "(도망쳐...!)",
        "“드디어.. 퇴근이다! 오늘도 잘 버텼다!”",
        "수고했어요. 그럼 다음 근무 때 뵙죠",
        "저 못하겠어요. 관둘게요. 안녕히 계세요.",
        "(맞아... 당장이라도 관두고 싶을 만큼 힘든 날도 있었지...)",
        "쉬는 시간 동안 뭘 하지?",
        "(쉬는 시간의 지출도 소소한 행복이었는데)",
        "00씨? 그래서 어떻게 하실 건가요?",
        "네? 뭘... 뭐를요?",
        "재계약말입니다. 전 00씨가 계속 근무해줬으면 좋겠는데",
        "(네????????????)"
    };

    enum Texts
    {
        TalkText,
    }
    enum GameObjects
    {
        background1,
        background2,
        Manager,
        Employee,
        Employee_casual
    }

    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.Employee).SetActive(false);
        Get<GameObject>((int)GameObjects.Employee_casual).SetActive(false);

        Get<Text>((int)Texts.TalkText).text = talkData[index];
        index++;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (index < talkData.Length)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowCharacterImg();
                ShowBackgroundImg();
                Get<Text>((int)Texts.TalkText).text = talkData[index];
                index++;
                timer = 0f;
            }

            else
            {
                timer += Time.deltaTime;
                if (timer >= delayTime)
                {
                    timer = 0f;
                    ShowCharacterImg();
                    ShowBackgroundImg();
                    Get<Text>((int)Texts.TalkText).text = talkData[index];
                    index++;
                }
            }
        }

        else
        {
            Invoke("MoveToMain", 1.5f);
        }
    }

    void ShowCharacterImg()
    {
        if (index >= 0 && index <= 1 || index == 6 || index == 10 || index == 15 || index == 18)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
        }
        else if (index == 2 || index == 9 || index >= 13 && index <= 14)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(true);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        }
        else if (index == 3 || index == 8 || index == 12)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        }
        else if (index == 4 || index == 5)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(true);
            Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        }
        else if (index == 7)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(true);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
        }
        else if (index == 11 || index >= 16 && index <= 17)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(true);
            Get<GameObject>((int)GameObjects.Employee_casual).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
        }
    }

    void ShowBackgroundImg()
    {
        if (index == 3 || index == 8 || index == 12 || index == 14)
        {
            Get<GameObject>((int)GameObjects.background1).SetActive(false);
            Get<GameObject>((int)GameObjects.background2).SetActive(false);
        }
        else if (index >= 4 && index <= 5)
        {
            Get<GameObject>((int)GameObjects.background1).SetActive(true);
            Get<GameObject>((int)GameObjects.background2).SetActive(false);
        }
        else if (index >= 0 && index <= 2 || index >= 6 && index <= 7 || index >= 9 && index <= 11 || index == 13 || index >= 15 && index <= 18)
        {
            Get<GameObject>((int)GameObjects.background1).SetActive(true);
            Get<GameObject>((int)GameObjects.background2).SetActive(true);
        }
    }

    void MoveToMain()
    {
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);
    }
}
