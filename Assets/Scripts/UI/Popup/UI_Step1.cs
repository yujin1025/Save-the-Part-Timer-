using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Step1 : UI_Popup
{
    enum Buttons
    {
        Sauce1,
        Sauce2,
        Sauce3,
        Cheese
    }

    enum GameObjects
    {
        Blocker1,
        Blocker2,
        SauceBar
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Get<GameObject>((int)GameObjects.Blocker2).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.SauceBar).gameObject.SetActive(false);

        //아직 customer 피자 데이터 없어서 이런식으로 구현
        Get<Button>((int)Buttons.Sauce1).gameObject.BindEvent(OnButtonClicked1);
        Get<Button>((int)Buttons.Sauce2).gameObject.BindEvent(OnButtonClicked2);
        Get<Button>((int)Buttons.Sauce3).gameObject.BindEvent(OnButtonClicked3);
        
    }
    void Start()
    {
        Init();

    }

    void ChangeStep3()
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_Step3>();
    }

    void OnButtonClicked1(PointerEventData data)
    {
        //피자도우에 소스 이미지추가
        Debug.Log("토마토 소스");
        //소스 클릭x, 치즈 클릭o, 게이지 생성
        Get<GameObject>((int)GameObjects.Blocker1).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Blocker2).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.SauceBar).gameObject.SetActive(true);

        //치즈 클릭 + 클릭시 도우에 치즈 이미지 추가
        Get<Button>((int)Buttons.Cheese).gameObject.BindEvent(OnButtonClicked);
    }

    void OnButtonClicked2(PointerEventData data)
    {
        Debug.Log("마요네즈 소스");
        Get<GameObject>((int)GameObjects.Blocker1).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Blocker2).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.SauceBar).gameObject.SetActive(true);

        Get<Button>((int)Buttons.Cheese).gameObject.BindEvent(OnButtonClicked);
    }

    void OnButtonClicked3(PointerEventData data)
    {
        Debug.Log("구운 양파 소스");
        Get<GameObject>((int)GameObjects.Blocker1).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.Blocker2).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.SauceBar).gameObject.SetActive(true);

        Get<Button>((int)Buttons.Cheese).gameObject.BindEvent(OnButtonClicked);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        //치즈 이미지 올라간 후 0.5초뒤 자동으로 3단계로 전환
        Debug.Log("치즈 추가");
        Invoke("ChangeStep3", 0.5f);
    }

    public void OnCloseButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
