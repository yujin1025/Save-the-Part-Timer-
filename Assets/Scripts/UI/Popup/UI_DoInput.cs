using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DoInput : UI_Popup
{
    enum InputFields
    {
        NameInputField,
    }

    enum Texts
    {
        NameFieldTitle,
        NameInput
    }

    enum Buttons
    {
        SaveButton,
    }
    public override void Init()
    {
        base.Init();
        Bind<InputField>(typeof(InputFields));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.SaveButton).gameObject.BindEvent(OnSaveButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    void OnSaveButtonClicked(PointerEventData data)
    {
        if (Get<Text>((int)Texts.NameInput).text == "Guest")
        {
            Debug.Log("Guest 로는 이름을 설정 불가");
            return;
        }
        Managers.s_managersProperty.playerNameProperty = Get<Text>((int)Texts.NameInput).text;
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
        Managers.uiManagerProperty.ShowPopupUI<UI_Welcome>();
    }
}
