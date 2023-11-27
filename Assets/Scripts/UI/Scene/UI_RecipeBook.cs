using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RecipeBook : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }
    enum Buttons
    {
        CloseButton
    }
    enum Texts
    {
        Title
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);


        foreach (Transform child in gridPanel.transform)
            Managers.resourceManagerProperty.Destroy(child.gameObject);

        for (int i = 0; i < 8; i++)
        {
            GameObject pizzaIndex = Managers.uiManagerProperty.MakeSubItem<UI_PizzaIndex>(gridPanel.transform).gameObject;
            pizzaIndex.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void OnCloseButtonClicked(PointerEventData data)
    {
        Managers.sceneManagerEXProperty.LoadScene(Defines.Scene.Main);
    }
}
