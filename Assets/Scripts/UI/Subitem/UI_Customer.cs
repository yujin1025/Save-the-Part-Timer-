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
    public int timeLeft;
    public string orderName;
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.AcceptButton).gameObject.BindEvent(OnButtonClicked);

    }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        
    }

    void OnButtonClicked(PointerEventData data)
    {
        //피자 완성시 false 이게끔 수정 필요
        gameObject.SetActive(false);
    }

    IEnumerator CountDeadLine()
    {
        while (timeLeft > 0)
        {
            Get<Text>((int)Texts.Deadline).text = ConvertSecondsToMinutesAndSeconds(timeLeft);
            timeLeft--;
            yield return new WaitForSeconds(1);
        }
        gameObject.SetActive(false);

    }

    public string ConvertSecondsToMinutesAndSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void OnEnable()
    {
        timeLeft = deadLine;
        Get<Text>((int)Texts.Order).text = orderName;
        StartCoroutine(CountDeadLine());
    }
}
