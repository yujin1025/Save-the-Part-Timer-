using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GaugeBar : UI_Base
{
    public event Action GaugeIsMax;

    private Image gaugeBar;
    public float current = 0;
    public bool autoIncrese = true;
    public float autoIncreseValue;
    enum Images
    {
        Background,
        Gauge
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        gaugeBar = Get<Image>((int)Images.Gauge);
        gaugeBar.fillAmount = current / 100;

        autoIncreseValue = Managers.levelDataList.GetLevel(Managers.s_managersProperty.stageNameStrProperty).GuageSpeed;
    }

    void Start()
    {
        Init();
    }
    private void Update()
    {
        GaugeBarUpdate();
        if (autoIncrese)
        {
            current += Time.deltaTime * autoIncreseValue;
        }
        if (current > 100)
        {
            autoIncrese = false;
            current = 100f;
            Debug.Log("stress gauge is full");
            GaugeMax();
        }
    }
    private void GaugeBarUpdate()
    {
        gaugeBar.fillAmount = current / 100;
    }

    /// <summary>
    /// 스트레스 상승 속도를 value 만큼 증가
    /// </summary>
    /// <param name="value"></param>
    public void GaugeSpeedUp(float value)
    {
        autoIncreseValue += value;
    }
    /// <summary>
    /// 스트레스 상승 속도를 value 만큼 감소
    /// </summary>
    /// <param name="value"></param>
    public void GaugeSpeedDown(float value)
    {
        autoIncreseValue -= value;
    }

    public void GaugeCurrentUp(float value)
    {
        current += value;
    }

    public void GaugeCurrentDown(float value)
    {
        current -= value;
    }

    protected virtual void GaugeMax()
    {
        GaugeIsMax?.Invoke();
    }
}
