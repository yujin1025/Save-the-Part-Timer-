using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PizzaIndex : UI_Base
{
    public int pizzaNum;
    public string pizzaName;
    public bool isPizzaLocked;
    public Sprite pizzaSprite;
    public Sprite pizzaRecipeSprite;

    enum Texts
    {
        PizzaNameText
    }

    enum Images
    {
        PizzaIcon,
        PizzaInfo,
        PizzaLocked
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Get<Text>((int)Texts.PizzaNameText).text = pizzaName;
        Get<Image>((int)Images.PizzaIcon).sprite = pizzaSprite;

        if (isPizzaLocked)
        {
            Get<Image>((int)Images.PizzaLocked).gameObject.SetActive(true);
        }
        else
        {
            Get<Image>((int)Images.PizzaLocked).gameObject.SetActive(false);
            Get<Image>((int)Images.PizzaInfo).gameObject.BindEvent(OnPizzaInfoClicked);
        }
    }

    
    void Start()
    {
        Init();
    }

    public void OnPizzaInfoClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Recipe>(gameObject);
    }
}
