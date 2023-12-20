using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
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
        background1,
        Employee,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.background1).SetActive(false);
        Get<Button>((int)Buttons.GoToMain).gameObject.BindEvent(MoveToMain);
        Get<Text>((int)Texts.TalkText).text = "저 정말... 더 이상 못하겠어요.\r\n관둘래요. 죄송합니다.";
    }
    private void Start()
    {
        Init();
    }
    void MoveToMain(PointerEventData data)
    {
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Get<GameObject>((int)GameObjects.background1).SetActive(true);
            Get<Button>((int)Buttons.GoToMain).gameObject.SetActive(true);
            Get<GameObject>((int)GameObjects.Employee).SetActive(false);
            Get<Text>((int)Texts.TalkText).text = "그렇게 난 아르바이트 블랙리스트에 올라버렸고...\r\n다시는 일할 수 없었다...";
        }
    }
}
