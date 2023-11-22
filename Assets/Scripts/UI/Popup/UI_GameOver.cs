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
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.gotoMain).gameObject.BindEvent(MoveToMain);
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
