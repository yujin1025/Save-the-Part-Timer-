using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene
{

    enum Buttons
    {
        StartBtn
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.StartBtn).gameObject.BindEvent(OnStartBtnClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    void OnStartBtnClicked(PointerEventData data)
    {
        if (Managers.s_managersProperty.isFirstGameProperty == true)
            Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Lobby);
        else
            Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);
    }
}
