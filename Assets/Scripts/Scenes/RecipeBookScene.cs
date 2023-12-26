using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookScene : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        Managers managers = Managers.s_managersProperty;
        Managers.uiManagerProperty.ShowPopupUI<UI_RecipeBook>();
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
