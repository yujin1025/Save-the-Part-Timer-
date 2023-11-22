using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Recipe : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
    }
    void Start()
    {
        Init();
        
    }
    public void OnCloseButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
