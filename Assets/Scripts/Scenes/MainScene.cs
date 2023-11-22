using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public override void Clear()
    {
        //Clear 구현해야 함
    }

    protected override void Init()
    {
        base.Init();
        Managers managers = Managers.s_managersProperty;
        Time.timeScale = 1.0f;
        Managers.uiManagerProperty.ShowSceneUI<UI_Main>();
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
