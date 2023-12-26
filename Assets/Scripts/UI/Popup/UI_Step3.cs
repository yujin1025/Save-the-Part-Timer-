using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class UI_Step3 : UI_Popup
{
    enum Buttons
    {
        Pepperoni,
        Onion,
        Corn,
        Mushroom,
        Bacon,
        BellPepper,
        Potato,
        Olive,
        Bulgogi,
    }

    public enum GameObjects
    {
        IngreBlock
    }

    public string orderName;

    private List<Buttons> selectedIngredients = new List<Buttons>();
    private List<Buttons> correctIngredients;

    private int order = 0;
    

    UI_Game ui_game;

    public bool isIngreSelectDone = false;

    public Sprite[] pizzaImages;

    public class PizzaData
    {
        public List<int> ImageNumbers { get; set; }
    }

    private Dictionary<string, PizzaData> pizzaDataMap = new Dictionary<string, PizzaData>();

    public override void Init()
    {
        base.Init();

        ui_game = transform.parent.gameObject.GetComponent<UI_Game>();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        orderName = transform.parent.gameObject.GetComponent<UI_Game>().orderName;
        correctIngredients = GetIngredientsOrder(orderName);

        Get<GameObject>((int)GameObjects.IngreBlock).SetActive(false);

        foreach (Buttons ingredient in Enum.GetValues(typeof(Buttons)))
        {
            Get<Button>((int)ingredient).gameObject.BindEvent((data) => OnIngredientClicked(ingredient, data));
        }

        InitPizzaData();
    }

    void Start()
    {
        Init();

    }

    //피자 데이터를 초기화하고 딕셔너리에 추가
    void InitPizzaData()
    {
        PizzaData PepperoniPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {0},
        };

        PizzaData BaconPotatoPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {1, 2, 3},
        };

        PizzaData BulgogiPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {4, 5, 6, 7},
        };

        PizzaData CornPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {8, 9},
        };

        PizzaData PepperoniPotatoPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {10, 11, 12, 13},
        };

        PizzaData PotatoBulgogiPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {14, 15, 16, 17, 18, 19},
        };

        PizzaData BulgogiCornPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {20, 21, 22, 23, 24},
        };

        PizzaData CombinationPizzaData = new PizzaData
        {
            ImageNumbers = new List<int> {25, 26, 27, 28, 29, 30, 31},
        };

        pizzaDataMap.Add("페퍼로니 피자", PepperoniPizzaData);
        pizzaDataMap.Add("베이컨 포테이토 피자", BaconPotatoPizzaData);
        pizzaDataMap.Add("불고기 피자", BulgogiPizzaData);
        pizzaDataMap.Add("옥수수 피자", CornPizzaData);
        pizzaDataMap.Add("페퍼로니&포테이토 반반 피자", PepperoniPotatoPizzaData);
        pizzaDataMap.Add("포테이토&불고기 반반 피자", PotatoBulgogiPizzaData);
        pizzaDataMap.Add("불고기&옥수수 반반 피자", BulgogiCornPizzaData);
        pizzaDataMap.Add("콤비네이션 피자", CombinationPizzaData);
    }

    //orderName에 해당하는 PizzaRecipe에서 step3의 재료를 가져오기
    List<Buttons> GetIngredientsOrder(string pizzaName)
    {
        List<Buttons> order = new List<Buttons>();
        string[] step3Ingredients = Managers.pizzaRecipeList.ReturnStepData(pizzaName, 2);

        if (step3Ingredients != null)
        {
            foreach (string ingredient in step3Ingredients)
            {
                UI_Step3.Buttons ingredientEnum;
                if (Enum.TryParse(ingredient, out ingredientEnum)) order.Add(ingredientEnum);
            }
            if (step3Ingredients.Length == 0)
            {
                BackToStep1();
                Debug.Log("치즈 피자");
                ui_game.earnedMoney += Managers.pizzaRecipeList.ReturnPriceOrder(ui_game.orderName);
                Debug.Log("지금까지 번 돈 :" + ui_game.earnedMoney);
            }
            Debug.Log(step3Ingredients);
        }

        string test1 = string.Join(", ", order);
        Debug.Log("order: " + test1);

        return order;
    }

    void OnIngredientClicked(Buttons ingredient, PointerEventData data)
    {
        selectedIngredients.Add(ingredient);

        string test2 = string.Join(", ", selectedIngredients);
        Debug.Log("선택 현황: " + test2);

        //클릭한 재료가 순서에 맞는지, 모든 재료가 선택되었는지 확인
        if (CheckCorrectSequence())
        {
            Debug.Log("맞음");

            // 피자 데이터 가져오기
            PizzaData pizzaData;

            if (pizzaDataMap.TryGetValue(orderName, out pizzaData))
                AddPizzaImage(pizzaData.ImageNumbers[order]);

            order++;
        }
        else
        {
            Debug.Log("틀림");

            int lastIndex = selectedIngredients.LastIndexOf(ingredient);

            if (lastIndex != -1)
            {
                selectedIngredients.RemoveAt(lastIndex);
            }

            UI_Game.FindObjectOfType<UI_GaugeBar>().GaugeCurrentUp(5);
        }
    }


    void AddPizzaImage(int imageNumber)
    {
        GameObject newImageObject = new GameObject("PizzaImage");
        Image newImage = newImageObject.AddComponent<Image>();

        newImage.sprite = pizzaImages[imageNumber];

        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1060, 630);

        newImage.transform.SetParent(ui_game.Get<GameObject>((int)UI_Game.GameObjects.Ingredients).transform, false);
    }


    bool CheckCorrectSequence()
    {
        for (int i = 0; i < selectedIngredients.Count; i++)
            if (selectedIngredients[i] != correctIngredients[i])
                return false;

        if (selectedIngredients.SequenceEqual(correctIngredients))
        {
            isIngreSelectDone = true;
            Debug.Log("피자 완성되었음");
            //Debug.Log(Managers.s_managersProperty.moneyProperty + Managers.pizzaRecipeList.ReturnPriceOrder(ui_game.orderName));
            ui_game.earnedMoney += Managers.pizzaRecipeList.ReturnPriceOrder(ui_game.orderName);
            Debug.Log("지금까지 번 돈 :"+ ui_game.earnedMoney);


            ui_game.Get<Image>((int)UI_Game.Images.NextStepReadyImage).gameObject.SetActive(true);
            ui_game.Get<Button>((int)UI_Game.Buttons.NextButton).gameObject.SetActive(true);
            Get<GameObject>((int)GameObjects.IngreBlock).SetActive(true);
        }
            
        return true; 
    }

    public void BackToStep1()
    {
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);

        //피자 만들기가 끝났다고 체트
        ui_game.pizzaMakingOnGoing = false;

        //데드라인을 넘겼던 고객일 경우 스트레스 상승량을 내림
        if (ui_game.selectedCustomer.time >= ui_game.selectedCustomer.deadLine)
        {
            ui_game.Get<GameObject>((int)UI_Game.GameObjects.StressBar).GetComponent<UI_GaugeBar>().GaugeSpeedDown(0.2f);
        }

        //주문이 완료된 고객을 지움
        ui_game.selectedCustomer.gameObject.SetActive(false);
        //step1을 다시 띄우며 처음으로 돌아감
        transform.parent.gameObject.GetComponent<UI_Game>().ui_step1 = Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step1>(transform.parent.gameObject);

        RectTransform[] pizzaIngredients = ui_game.Get<GameObject>((int)UI_Game.GameObjects.Pizza).GetComponentsInChildren<RectTransform>();

        // Pizza 오브젝트의 RectTransform을 참조합니다.
        RectTransform pizzaRectTransform = ui_game.Get<GameObject>((int)UI_Game.GameObjects.Pizza).GetComponent<RectTransform>();
        RectTransform pizzaSauceLayer = ui_game.Get<GameObject>((int)UI_Game.GameObjects.SauceLayer).GetComponent<RectTransform>();

        foreach (RectTransform rectTransform in pizzaIngredients)
        {
            // 부모 오브젝트가 아닌 경우에만 비활성화
            if (rectTransform != pizzaRectTransform && rectTransform != pizzaSauceLayer)
            {
                rectTransform.gameObject.SetActive(false);
            }
        }
    }
}
