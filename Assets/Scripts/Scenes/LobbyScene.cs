using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override void Clear()
    {
        
        ;
    }

    protected override void Init()
    {
        base.Init();
        Managers managers = Managers.s_managersProperty;
        Time.timeScale = 0.0f;
        Managers.uiManagerProperty.ShowSceneUI<UI_Lobby>();
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
