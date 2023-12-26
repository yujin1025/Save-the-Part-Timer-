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
    enum Texts
    {
        PizzaName
    }
    enum Images
    {
        BackGround
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        UI_PizzaIndex tmp = transform.parent.GetComponent<UI_PizzaIndex>();

        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        Get<Text>((int)Texts.PizzaName).text = tmp.pizzaName;
        Get<Image>((int)Images.BackGround).sprite = tmp.pizzaRecipeSprite;


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
