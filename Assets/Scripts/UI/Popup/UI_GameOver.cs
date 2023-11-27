using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Buttons
    {
        gotoMain,
    }
    enum Texts
    {
        TalkText,
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        Get<Button>((int)Buttons.gotoMain).gameObject.BindEvent(MoveToMain);
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
        
    }
}
