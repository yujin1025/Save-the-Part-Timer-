using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GaugeBar : UI_Base
{
    private Image gaugeBar;
    public float current = 10f;
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
    }

    public void GaugeBarUpdate()
    {
        gaugeBar.fillAmount = current / 100;
    }

    public void GaugeUp(float temp)
    {
        current += temp;
        GaugeBarUpdate();
    }

    public void GaugeDown(float temp)
    {
        current -= temp;
        GaugeBarUpdate();
    }

}
