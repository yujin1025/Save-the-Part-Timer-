using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Settings : UI_Popup
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
    void OnCloseButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
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
