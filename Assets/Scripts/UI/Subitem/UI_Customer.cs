using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Customer : UI_Base
{
    enum Texts
    {
        Order,
        Deadline
    }

    enum Buttons
    {
        AcceptButton
    }

    enum Images
    {
        CustomerBackGround
    }

    public int deadLine;
    public int time;
    public string orderName;
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.AcceptButton).gameObject.BindEvent(OnButtonClicked);
        time = 0;

    }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        
    }

    void OnButtonClicked(PointerEventData data)
    {
        Managers.uiManagerProperty.ShowPopupUIUnderParent<UI_Step1>(this.transform.parent.parent.gameObject);

        //피자 완성시 false 이게끔 수정 필요
        //gameObject.SetActive(false);

        //임의로 무슨 버튼 클릭했는지 알기위해 색상 변경
        Get<Image>((int)Images.CustomerBackGround).color = new Color(1, 0, 0);

        if (time >= deadLine) transform.parent.parent.Find("StressBar").GetComponent<UI_GaugeBar>().GaugeSpeedDown(1);
    }

    IEnumerator CountDeadLine()
    {
        while (true)
        {
            Get<Text>((int)Texts.Deadline).text = ConvertSecondsToMinutesAndSeconds(time);
            time++;
            if (time == deadLine) transform.parent.parent.Find("StressBar").GetComponent<UI_GaugeBar>().GaugeSpeedUp(1);
            yield return new WaitForSeconds(1);
        }

    }

    public string ConvertSecondsToMinutesAndSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void OnEnable()
    {
        Get<Text>((int)Texts.Order).text = orderName;
        StartCoroutine(CountDeadLine());
    }
}
