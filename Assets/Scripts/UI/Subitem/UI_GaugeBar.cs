using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GaugeBar : UI_Base
{
    private Image gaugeBar;
    public float current = 0;
    public bool autoIncrese = true;
    public float autoIncreseValue = 5f;
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
    }

    void Start()
    {
        Init();
    }
    private void Update()
    {
        GaugeBarUpdate();//test¿ë
        if (autoIncrese)
        {
            current += Time.deltaTime * autoIncreseValue;
        }
        if (current > 100)
        {
            autoIncrese = false;
            current = 100f;
            Debug.Log("stress gauge is full");
        }
    }
    private void GaugeBarUpdate()
    {
        gaugeBar.fillAmount = current / 100;
    }

    public void GaugeUp(float value)
    {
        current += value;
    }

    public void GaugeDown(float value)
    {
        current -= value;
    }

}
