using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class UI_Step3 : UI_Popup
{
    enum Buttons
    {
        Ingredient1,
        Ingredient2,
        Ingredient3,
        Ingredient4,
        Ingredient5,
        Ingredient6,
        Ingredient7,
        Ingredient8,
        Ingredient9
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        //9개 중 뭐를 선택해야되는지 구현되어있는지 몰라서 일단 이런식으로 구현
        Get<Button>((int)Buttons.Ingredient1).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient2).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient3).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient4).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient5).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient6).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient7).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient8).gameObject.BindEvent(OnButtonClicked);
        Get<Button>((int)Buttons.Ingredient9).gameObject.BindEvent(OnButtonClicked);
    }
    void Start()
    {
        Init();

    }

    void OnButtonClicked(PointerEventData data)
    {
        //해당 customer이 삭제되어야됨

        ClosePopupUI();
    }

    public void OnCloseButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
