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
    public Text _timerText;
    public float time = 10f;
    public bool isCounting;
    void Start()
    {
        _timerText = gameObject.GetComponent<Text>();
        StartCoroutine(Timer(time));
    }

    private IEnumerator Timer(float _time)
    {
        isCounting = true;
        while (_time > 0 && isCounting)
        {
            _time -= Time.deltaTime;
            string minutes = Mathf.Floor(_time / 60).ToString("00");
            string seconds = (_time % 60).ToString("00");
            _timerText.text = string.Format("{0}:{1}", minutes, seconds);
            yield return null;

            if (_time <= 0)
            {
                isCounting = false;
                _timerText.text = "Times Up";
            }
        }
    }
    void Update()
    {
        
    }
}
