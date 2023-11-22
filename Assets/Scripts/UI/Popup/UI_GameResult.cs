using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameResult : UI_Popup
{
    enum Buttons
    {
        gotoMain,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.gotoMain).gameObject.BindEvent(MoveToMain);
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
        
    }
}
