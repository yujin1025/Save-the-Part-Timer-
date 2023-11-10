using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


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
        Get<Button>((int)Buttons.Cheese).gameObject.BindEvent(OnCheeseClicked);

        Get<Button>((int)Buttons.TomatoSauce).gameObject.BindEvent(OnTomatoClicked);
        Get<Button>((int)Buttons.MayonnaiseSauce).gameObject.BindEvent(OnMayonnaiseClicked);
        Get<Button>((int)Buttons.OnionSauce).gameObject.BindEvent(OnOnionClicked);

    }
    
    void Start()
    {
        Init();

    }

    void ChangeStep3()
    {
        Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step3>(transform.parent.gameObject);
    }

    void OnTomatoClicked(PointerEventData data)
    {
        //피자도우에 소스 이미지추가
        Debug.Log("토마토 소스");
        //소스 클릭x, 치즈 클릭o, 게이지 생성
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
        if (chosenSauce[index] == false) chosenSauce[index] = true;
        else chosenSauce[index] = false;

        if (chosenSauce.SequenceEqual<bool>(ui_game.sauceAnswer))
        {
            Debug.Log("소스 정답");
            isSauceSelectDone = true;
            Get<GameObject>((int)GameObjects.SauceBlocker).SetActive(true);
            Get<GameObject>((int)GameObjects.CheeseBlocker).SetActive(false);
        }

    }
    public void OnCheeseClicked(PointerEventData data)
    {
        //치즈 이미지 올라간 후 0.5초뒤 자동으로 3단계로 전환
        Debug.Log("치즈 추가");
        isCheeseSelcetDone = true;
        Invoke("ChangeStep3", 0.5f);
    }
}
