using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// isMoving이 참일 경우 게이지를 minValue와 maxValue사이를 이동시킨다.
/// </summary>
public class UI_StopGaugeBar : UI_Base
{
    private Image gaugeBar;

    public bool isMoving = true; //게이지 이동 bool값
    public float current = 0; //게이지 현재값
    public float minValue = 0; //게이지 최소값
    public float maxValue = 100f; //게이지 최대값
    public float autoMovingValue = 50f; //게이지 움직이는 속도

    private int direction = 1;
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
        if (isMoving)
        {
            GaugeBarUpdate();//test용
        }
        else return;
    }
    private void GaugeBarUpdate()
    {
        gaugeBar.fillAmount = current / 100;
        if (isMoving)
        {
            current = Mathf.Clamp(current + autoMovingValue * direction * Time.deltaTime, minValue, maxValue);
            if (current >= maxValue || current <= minValue)
            {
                direction *= -1; // 방향 변경
            }
        }
    }
    /// <summary>
    /// 움직이던 게이지를 멈추고 현재값 반환
    /// </summary>
    /// <returns>float UI_StopGaugeBar.current</returns>
    public float GetCurrentGauge()
    {
        isMoving = false;
        return current;
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
