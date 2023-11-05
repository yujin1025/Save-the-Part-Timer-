using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene
{
    enum Images
    {
        BackGround,
    }
    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.BackGround).gameObject.BindEvent(OnBackGroundClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }
    
    void OnBackGroundClicked(PointerEventData data)
    {

    }
}
