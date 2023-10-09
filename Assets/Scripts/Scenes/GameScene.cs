using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        Managers managers = Managers.s_managersProperty;
        Managers.uiManagerProperty.ShowSceneUI<UI_Game>();
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
    public override void Clear()
    {
        
    }
}
