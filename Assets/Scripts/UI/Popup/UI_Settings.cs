using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Settings : UI_Popup
{
    Scrollbar sfxBar;
    Scrollbar bgmBar;

    Image sfxGreenBar;
    Image bgmGreenBar;


    AudioMixer audioMixer;

    enum Buttons
    {
        CloseButton,
        ManualButon
    }

    enum Scrollbars
    {
        SFXBar,
        BGMBar
    }

    enum Images
    {
        SFXGreenBar,
        BGMGreenBar
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Scrollbar>(typeof(Scrollbars));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
        Get<Button>((int)Buttons.ManualButon).gameObject.BindEvent(OnManualButtonClicked);


        sfxBar = Get<Scrollbar>((int)Scrollbars.SFXBar);
        sfxBar.gameObject.BindEvent(OnSFXBarMove, Defines.UIEvent.Drag);

        bgmBar = Get<Scrollbar>((int)Scrollbars.BGMBar);
        bgmBar.gameObject.BindEvent(OnBGMBarMove, Defines.UIEvent.Drag);

        sfxGreenBar = Get<Image>((int)Images.SFXGreenBar);
        bgmGreenBar = Get<Image>((int)Images.BGMGreenBar);

        audioMixer = Managers.soundManagerProperty.audioMixer;

        float effectVolume;
        if(audioMixer.GetFloat("Effect", out effectVolume))
        {
            effectVolume = Mathf.Pow(10, effectVolume / 20);
            sfxBar.value = effectVolume;
            sfxGreenBar.fillAmount = effectVolume;
        }

        float bgmVolume;
        if(audioMixer.GetFloat("BGM", out bgmVolume))
        {
            bgmVolume = Mathf.Pow(10, bgmVolume / 20);
            bgmBar.value = bgmVolume;
            bgmGreenBar.fillAmount = bgmVolume;
        }
        
    }
    void OnCloseButtonClicked(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        Managers.uiManagerProperty.SafeClosePopupUIOnTop(this);
    }    
    void OnManualButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUI<UI_GameDescription>();
    }

    void OnSFXBarMove(PointerEventData data)
    {
        if (sfxBar.value > 0.001)
        {
            audioMixer.SetFloat("Effect", Mathf.Log10(sfxBar.value) * 20);
        }
        else
        {
            audioMixer.SetFloat("Effect", -80);
        }
        sfxGreenBar.fillAmount = sfxBar.value;
    }
    void OnBGMBarMove(PointerEventData data)
    {
        if (bgmBar.value > 0.001)
        {
            audioMixer.SetFloat("BGM", Mathf.Log10(bgmBar.value) * 20);
        }
        else
        {
            audioMixer.SetFloat("BGM", -80);
        }
        bgmGreenBar.fillAmount = bgmBar.value;
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
