using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 카운트 다운 컴포넌트 : 
/// time 에 카운트할 초 입력
/// 카운트 다운이 종료되면 isCouning이 false가 된다
/// </summary>

public class CountDown : MonoBehaviour
{
    public event Action CountdownFinished;
    private Text _timerText;
    public float time = 90f;
    public bool isCounting;

    public void Init()
    {
        isCounting = true;
        _timerText = gameObject.GetComponent<Text>();
        StartCoroutine(Timer(10));//제한시간 바꾸고 싶으면 time 대신 숫자 집어넣기
    }
    void Start()
    {
        Init();
    }

    private IEnumerator Timer(float _time)
    {
        isCounting = true;
        while (_time > 0 && isCounting)
        {
            _time -= Time.deltaTime;

            // Ensure that minutes and seconds are formatted correctly
            int minutesInt = Mathf.FloorToInt(_time / 60);
            int secondsInt = Mathf.FloorToInt(_time % 60);

            string minutes = minutesInt.ToString("00");
            string seconds = secondsInt.ToString("00");

            _timerText.text = string.Format("{0}:{1}", minutes, seconds);
            yield return null;

            if (_time <= 0)
            {
                isCounting = false;
                _timerText.text = "Time's Up";
                OnCountdownFinished();
            }
        }
    }

    /// <summary>
    /// 카운트다운 종료 이벤트
    /// </summary>
    protected virtual void OnCountdownFinished()
    {
        CountdownFinished?.Invoke();
    }
    void Update()
    {
        
    }
}
