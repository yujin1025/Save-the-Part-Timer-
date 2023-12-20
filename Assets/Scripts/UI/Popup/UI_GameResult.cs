using System.Collections;
using System.Collections.Generic;
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

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Manager).SetActive(false);
        Get<Button>((int)Buttons.GoToMain).gameObject.BindEvent(MoveToMain);
        Get<Text>((int)Texts.TalkText).text = "드디어... 퇴근이다!\r\n오늘도 잘 버텼다!";

        Managers.s_managersProperty.dDayProperty++;
    }

    void MoveToMain(PointerEventData data)
    {
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
            Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(true);
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<GameObject>((int)GameObjects.Manager).SetActive(true);
            Get<Text>((int)Texts.TalkText).text = "오늘 매출은 X원이네요. 여기 인센티브(X*0.01)원입니다. " +
                "\r\n수고했어요. 그럼 다음 근무 때 뵙죠.";
            //아직 돈 못넣음!
        }
    }
}
