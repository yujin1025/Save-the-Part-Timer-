using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    public override void Clear()
    {
        //Clear 함수 구현해야 함
    }

    protected override void Init()
    {
        base.Init();
        Managers mag = Managers.s_managersProperty;
        Managers.uiManagerProperty.ShowSceneUI<UI_Start>();

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
