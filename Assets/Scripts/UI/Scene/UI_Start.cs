using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene
{
    
    public override void Init()
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_PressToStart>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }
}
