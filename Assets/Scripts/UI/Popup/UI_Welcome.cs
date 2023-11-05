using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Welcome : UI_Popup
{
    enum Texts
    {
        WelcomeText,
        UserNameText
    }
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Get<Text>((int)Texts.UserNameText).text = $"{Managers.s_managersProperty.playerNameProperty} ดิ";
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
}
