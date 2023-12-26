using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RecipeBook : UI_Popup
{
    string[] pizzaNameArr;
    Sprite[] pizzaSpriteArr;
    public List<Sprite> pizzaRecipeList;

    enum GameObjects
    {
        GridPanel
    }
    enum Buttons
    {
        CloseButton
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);

        pizzaNameArr = new string[9];
        pizzaSpriteArr = new Sprite[9];
        pizzaRecipeList = new List<Sprite>();

        pizzaNameArr[0] = "치즈 피자";
        pizzaNameArr[1] = "페퍼로니 피자";
        pizzaNameArr[2] = "베이컨 포테이토 피자";
        pizzaNameArr[3] = "불고기 피자";
        pizzaNameArr[4] = "옥수수 피자";
        pizzaNameArr[5] = "페퍼로니&포테이토\n반반 피자";
        pizzaNameArr[6] = "포테이토&불고기\n반반 피자";
        pizzaNameArr[7] = "불고기&옥수수\n반반 피자";
        pizzaNameArr[8] = "콤비네이션 피자";

        pizzaSpriteArr[0] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/cheesePizza");
        pizzaSpriteArr[1] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/pepperoniPizza");
        pizzaSpriteArr[2] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/potatobaconPizza");
        pizzaSpriteArr[3] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/bulgogiPizza");
        pizzaSpriteArr[4] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/cornPizza");
        pizzaSpriteArr[5] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/pepperoni_potatoPizza");
        pizzaSpriteArr[6] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/potato_bulgogiPizza");
        pizzaSpriteArr[7] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/bulgogi_cornPizza");
        pizzaSpriteArr[8] = Managers.resourceManagerProperty.Load<Sprite>("Images/UI/Recipe/pizza_icon/combinationPizza");

        pizzaRecipeList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Recipe/pizza_recipe"));

        foreach (Transform child in gridPanel.transform)
            Managers.resourceManagerProperty.Destroy(child.gameObject);

        bool[] avaliblePizzaList = Managers.GetAvaliablePizzaInBool(Managers.s_managersProperty.stageNameStrProperty);

        for (int i = 0; i < 9; i++)
        {
            GameObject pizzaIndex = Managers.uiManagerProperty.MakeSubItem<UI_PizzaIndex>(gridPanel.transform).gameObject;
            pizzaIndex.GetComponent<RectTransform>().localScale = Vector3.one;
            pizzaIndex.GetComponent<UI_PizzaIndex>().pizzaNum = i;
            pizzaIndex.GetComponent<UI_PizzaIndex>().pizzaName = pizzaNameArr[i];
            pizzaIndex.GetComponent<UI_PizzaIndex>().pizzaSprite = pizzaSpriteArr[i];
            pizzaIndex.GetComponent<UI_PizzaIndex>().pizzaRecipeSprite = pizzaRecipeList[i];

            if (avaliblePizzaList[i] == true)
            {
                pizzaIndex.GetComponent<UI_PizzaIndex>().isPizzaLocked = false;
            }
            else
            {
                pizzaIndex.GetComponent <UI_PizzaIndex>().isPizzaLocked = true;
            }

        }
    }

    void OnCloseButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.CloseAllPopupUI();
    }
}
