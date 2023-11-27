using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;


public class UI_Step1 : UI_Popup
{
    enum Buttons
    {
        TomatoSauce,
        MayonnaiseSauce,
        OnionSauce,
        Cheese,
    }

    public enum GameObjects
    {
        CheeseBlocker,
        SauceBlocker,
        TableBlocker,
        SauceBar
    }

    public bool isSauceSelectDone;
    public bool isCheeseSelcetDone;

    public bool[] chosenSauce;
    
    UI_Game ui_game;

    public override void Init()
    {
        base.Init();

        isSauceSelectDone = false;
        isCheeseSelcetDone = false;

        chosenSauce = new bool[3];
        chosenSauce[0] = false;
        chosenSauce[1] = false;
        chosenSauce[2] = false;

        ui_game = transform.parent.gameObject.GetComponent<UI_Game>();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Get<GameObject>((int)GameObjects.SauceBlocker).SetActive(false);
        Get<GameObject>((int)GameObjects.CheeseBlocker).SetActive(true);
        Get<Button>((int)Buttons.Cheese).gameObject.BindEvent(OnCheeseClicked);

        Get<Button>((int)Buttons.TomatoSauce).gameObject.BindEvent(OnTomatoClicked);
        Get<Button>((int)Buttons.MayonnaiseSauce).gameObject.BindEvent(OnMayonnaiseClicked);
        Get<Button>((int)Buttons.OnionSauce).gameObject.BindEvent(OnOnionClicked);


        ui_game.Get<GameObject>((int)UI_Game.GameObjects.Ingredients).SetActive(true);

        Transform ingredientsTransform = ui_game.Get<GameObject>((int)UI_Game.GameObjects.Ingredients).transform;
        foreach (Transform child in ingredientsTransform)
        {
            child.gameObject.SetActive(false);
        }

        
    }


    void Start()
    {
        Init();

    }

    void SetNextButtonActive()
    {
        ui_game.Get<Image>((int)UI_Game.Images.NextStepReadyImage).gameObject.SetActive(true);
        ui_game.Get<Button>((int)UI_Game.Buttons.NextButton).gameObject.SetActive(true);
    }

    void OnTomatoClicked(PointerEventData data)
    {
        Debug.Log("토마토 소스");          
        ChooseSauceAndCheck(0);
    }

    void OnMayonnaiseClicked(PointerEventData data)
    {
        Debug.Log("마요네즈 소스");
        ChooseSauceAndCheck(1);
    }

    void OnOnionClicked(PointerEventData data)
    {
        Debug.Log("구운 양파 소스");
        ChooseSauceAndCheck(2);
    }

    void ChooseSauceAndCheck(int index)
    {
        // ѷ       ҽ         ϸ   ҽ     Ѹ 
        if(ui_game.sauceAnswer[index])
        {
            if (chosenSauce[index] == false) chosenSauce[index] = true;
            if (index == 0)
            {
                if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_tomato half"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.TomatoSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_tomato half"];
                }
                else if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_tomato"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.TomatoSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_tomato"];
                }
                ui_game.Get<GameObject>((int)UI_Game.GameObjects.TomatoSauceLayer).SetActive(true);
            }
            else if (index == 1)
            {
                if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_mayo half"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.MayonnaiseSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_mayo half"];
                }
                else if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_mayo"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.MayonnaiseSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_mayo"];
                }
                ui_game.Get<GameObject>((int)UI_Game.GameObjects.MayonnaiseSauceLayer).SetActive(true);
            }
            else if (index == 2)
            {
                if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_baked onion half"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.OnionSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_baked onion half"];
                }
                else if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_baked onion half2"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.OnionSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_baked onion half2"];
                }
                else if (Array.Exists(ui_game.ingredientOrder, element => element == "layer_baked onion"))
                {
                    ui_game.Get<GameObject>((int)UI_Game.GameObjects.OnionSauceLayer).GetComponent<Image>().sprite = Managers.PizzaIngredientSpriteDic["layer_baked onion"];
                }
                ui_game.Get<GameObject>((int)UI_Game.GameObjects.OnionSauceLayer).SetActive(true);
            }
        }
        else
        {
            Debug.Log("Ʋ    ҽ  Դϴ ");
        }

        if (chosenSauce.SequenceEqual<bool>(ui_game.sauceAnswer))
        {
            Debug.Log(" ҽ      ");
            isSauceSelectDone = true;
            Get<GameObject>((int)GameObjects.SauceBlocker).SetActive(true);
            Get<GameObject>((int)GameObjects.CheeseBlocker).SetActive(false);
            Get<GameObject>((int)GameObjects.SauceBar).GetComponent<UI_StopGaugeBar>().isMoving = true;
        }

    }
    public void OnCheeseClicked(PointerEventData data)
    {
        //ġ    ̹     ö     0.5 ʵ   ڵ      3 ܰ     ȯ
        ui_game.Get<GameObject>((int)UI_Game.GameObjects.CheeseLayer).SetActive(true);
        Get<GameObject>((int)GameObjects.CheeseBlocker).SetActive(true);
        Debug.Log("ġ    ߰ ");

        UI_StopGaugeBar stopGaugeBar = transform.parent.GetComponentInChildren<UI_StopGaugeBar>();

        if (stopGaugeBar != null)
        {
            float currentGaugeValue = stopGaugeBar.GetCurrentGauge();
            Debug.Log("current gauge : " + currentGaugeValue);
            //기획나오면 수정하기
        }

        isCheeseSelcetDone = true;
        Invoke("SetNextButtonActive", 0.5f);
    }
}
