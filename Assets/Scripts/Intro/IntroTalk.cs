using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTalk : MonoBehaviour
{
    public GameObject Panel;
    public Text TalkText;
    public GameObject InputName;
    public float delayTime = 3f; //일단 3초로 
    float timer = 0f;
    public int index = 0;

    public InputField playerNameInput;
    private string playerName = null;

    private string[] talkData = {
        "1번 대사",
        "2번 대사",
        "3번 대사",
        "4번 대사",
        "5번 대사",
        "6번 대사",
        "7번 대사",
        "8번 대사",
        "",//이름 입력
        "9번 대사"
    };

    private void Awake()
    {
        playerName = playerNameInput.GetComponent<InputField>().text;
        TalkText.text = talkData[index];
        index++;
    }

    void Update()
    {
        if(index < 8)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TalkText.text = talkData[index];
                index++;
                timer = 0f;
            }

            else
            {
                timer += Time.deltaTime;
                if (timer >= delayTime)
                {
                    timer = 0f;
                    TalkText.text = talkData[index];
                    index++;
                }
            }
        }

        else if (index == 8)
        {
            InputName.SetActive(true);
            //string str = PlayerPrefs.GetString("Name");
            if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
            {
                ResultInputName();
                
            }
        }

        else 
        {
            TalkText.text = playerName + ", " + talkData[index];
        }
    }

    public void ResultInputName()
    {
        playerName = playerNameInput.text;
        //PlayerPrefs.SetString("Name", playerName);
        //GameManager.instance. ~~ 게임 메니저에 이름 입력하기 (나중에)
        InputName.SetActive(false);
        index++;
    }
}
