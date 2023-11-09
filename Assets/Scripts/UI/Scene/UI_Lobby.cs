using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    public float delayTime = 3f; //일단 3초로 
    float timer = 0f;
    public int index = 0;
    public bool test = true;

    private string[] talkData = {
        "“드디어…! 대학생이다!”",
        "남들보다 길었던 수험생활 탓일까, 오랜만에 맛본 속세의 생활에 나는 그만….",
        "이성을 잃어버리고 말았다.",
        "통장 잔액 23,911원",
        "그렇다. 나는 어느새 거지가 되어있었다.",
        "“이 거지 같은 자본주의 세상…. 돈을 쓰고 싶다면 돈을 벌어야 하는 거겠지….   그래, 알바! 알바를 하자!”",
        "“아쉽지만 경력이 없어서 뽑기 어렵겠네요.”",
        "하지만 알바 경력이 없는 나를 뽑아줄 곳을 찾기란 어려운 일이었다.",
        "“이번엔 여기나 지원해볼까?” 간단한 신상정보로만 지원한 백화점 식품관의 피자가게.",
        "“당장 이번 주부터 나오면 됩니다. 대신 최소 3개월은 일해야 합니다. 가능하겠어요?”",
        "(내 빈 통장 잔액을 채우기 위해서라면 못할 게 없다!) 네, 네…!",
        "“그럼 오늘 근로계약서부터 쓰고 가세요.”",
        "<< 이름을 입력해주세요 >>",  //12
        "씨? 그럼 앞으로 잘해봅시다.",   //13
        "저도 앞으로 잘 부탁드립니다!",
        "과연 3개월을 버틸 수 있을까?"
    };

    enum GameObjects
    {
        Background,
        Panel,
    }

    enum Buttons
    {
        Clicked
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
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Clicked).gameObject.BindEvent(OnButtonClicked);
        Get<Text>((int)Texts.TalkText).text = talkData[index];
        index++;
    }
    public void OnButtonClicked(PointerEventData data)
    {

    }

    
    
    void Update()
    {
        
        if(test)
        {
            if (index < 12)
            {
                if (Input.GetMouseButtonDown(0))
                {
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
                        Get<Text>((int)Texts.TalkText).text = talkData[index];
                        index++;
                    }
                }
            }

            else if (index == 12)
            {
                ResultInputName();
                test = false;
            }

            //update문에 빠져나왔을때 어떻게 해야될지, doinput 어떻게 쓸지 등 생각하기
            /*
            else if (index == 13)
            {
                Get<Text>((int)Texts.TalkText).text = Managers.s_managersProperty.playerNameProperty + ", " + talkData[index];
                index++;
            }

            else
            {
                Get<Text>((int)Texts.TalkText).text = talkData[index];
                index++;
            }*/
        }

        /*
        if (!test)
        {
            if (index == 13)
            {
                Debug.Log("출력");
                Get<Text>((int)Texts.TalkText).text = Managers.s_managersProperty.playerNameProperty + ", " + talkData[index];
                index++;
            }

            else if (index == 14)
            {
                Get<Text>((int)Texts.TalkText).text = talkData[index];
                index++;
            }
        }*/

    }

    public void ResultInputName()
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_DoInput>();
        index++;
    }
    
}
