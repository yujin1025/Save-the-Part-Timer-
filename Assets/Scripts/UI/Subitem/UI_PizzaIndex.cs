using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PizzaIndex : UI_Base
{
    enum Texts
    {
        PizzaName
    }

    enum Images
    {
        PizzaIcon
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.PizzaIcon).gameObject.BindEvent(OnPizzaImageClicked);
    }

    
    void Start()
    {
        Init();
    }

    public void OnPizzaImageClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_Recipe>();
    }
}
