using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RetireTalk : UI_Popup
{
    float delayTime = 3f; //일단 3초로 
    float timer = 0f;
    int index = 0;

    private string[] talkData = {
        "“점장님, 저 오늘부로 관두겠습니다.”",
        "“아직 3개월이 지나지 않았는데요?“",
        "“네.“",
        "“근로계약서에 분명 '3개월 이전에 관둘 시 위약금 99만원을 지불한다'... 아시죠?“",
        "“물론 압니다. 그깟 99만원!“",
        "“지금... 뭐라고...”",
        "“노동으로부터의 해방, 진정한 자유의 달성. 그리고 무엇보다 스트레스 조절 장애... \n그 모든 것을 99만원에 이룰 수 있다면! 저는 기꺼이 지불하고 떠나겠습니다.”",
        "“...그러세요.“",
        "그렇게... 나는 이 거지같은 노동시장으로부터 탈출했다."
    };

    enum GameObjects
    {
        background,
        background1,
        Panel,
        Employee,
        Manager,
    }

    enum Texts
    {
        TalkText
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

        Get<GameObject>((int)GameObjects.background1).SetActive(false);

        Get<GameObject>((int)GameObjects.Employee).SetActive(true);
        Get<GameObject>((int)GameObjects.Manager).SetActive(false);

        Get<Text>((int)Texts.TalkText).text = talkData[index];
        index++;
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
            Invoke("Retire", 1.5f);
        }
    }


    void ShowCharacterImg()
    {
        if (index == 0 || index == 6)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(true);
            Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        }
        else if (index == 7)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
        }
        else if (index >= 1 && index <= 5)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(true);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
        }
        else if (index == 8)
        {
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        }
    }

    void ShowBackgroundImg()
    {
        if (index == 8)
        {
            Get<GameObject>((int)GameObjects.background1).SetActive(true);
        }
    }

    void Retire()
    {
        if (Managers.s_managersProperty.isFirstRetireProperty)
        {
            Debug.Log("퇴사 처리 완료");
            Managers.s_managersProperty.moneyProperty -= 990000;
            Managers.s_managersProperty.isFirstRetireProperty = false;
        }

        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);
    }
}
