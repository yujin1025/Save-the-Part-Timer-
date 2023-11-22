using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BreakTime : UI_Popup
{
    enum GameObjects
    {
        Employee,
        Manager,
        Panel,
        Options,
    }

    enum Texts
    {
        TalkText,
    }

    enum Buttons
    {
        GoToBreak,
        GoToWork,
        Option1,
        Option2,
        Option3,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Employee).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToWork).gameObject.SetActive(false);
        Get<Text>((int)Texts.TalkText).text = Managers.s_managersProperty.playerNameProperty + "씨는 쉬고 오세요";

        Get<Button>((int)Buttons.GoToBreak).gameObject.BindEvent(MoveToBreak);
        Get<Button>((int)Buttons.GoToWork).gameObject.BindEvent(MoveToWork);

        Get<Button>((int)Buttons.Option1).gameObject.BindEvent(Option1Clicked);
        Get<Button>((int)Buttons.Option2).gameObject.BindEvent(Option2Clicked);
        Get<Button>((int)Buttons.Option3).gameObject.BindEvent(Option3Clicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void MoveToBreak(PointerEventData data)
    {
        //Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.BreakTime);
        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Employee).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Manager).gameObject.SetActive(false);
        Get<Button>((int)Buttons.GoToBreak).gameObject.SetActive(false);
        Get<Text>((int)Texts.TalkText).text = "쉬는 시간 동안 뭘 하지?";
    }

    void MoveToWork(PointerEventData data)
    {
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Game);
    }

    void Option1Clicked(PointerEventData data)
    {
        //기획 나오면 금액 차감, 스트레스 감소
        //잔액 부족시
        GoToWork(0);
    }

    void Option2Clicked(PointerEventData data)
    {
        GoToWork(1);
    }

    void Option3Clicked(PointerEventData data)
    {
        GoToWork(2);
    }

    void GoToWork(int index)
    {
        Get<GameObject>((int)GameObjects.Options).gameObject.SetActive(false);
        Get<Text>((int)Texts.TalkText).text = "선택지에 대응되는 대사";
        Get<Button>((int)Buttons.GoToWork).gameObject.SetActive(true);
    }
}
