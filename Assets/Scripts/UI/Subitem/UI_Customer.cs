using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

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

    public enum Images
    {
        CustomerBackGround,
        CustomerIcon
    }

    public int deadLine;
    public int time;
    public string orderName;
    public int customerIconNumber;
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
        UI_Game ui_game = transform.parent.parent.gameObject.GetComponent<UI_Game>();
        Get<Image>((int)Images.CustomerBackGround).color = new Color(1, 0, 0);

        //이름을 전달
        ui_game.orderName = orderName;

        //소스 정답 전달
        ui_game.sauceAnswer[0] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("토마토 소스");
        ui_game.sauceAnswer[1] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("마요네즈 소스");
        ui_game.sauceAnswer[2] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("구운 양파 소스");

        //테이블을 활성화
        UI_Step1 ui_step1 = ui_game.Get<GameObject>((int)UI_Game.GameObjects.UI_Step1).GetComponent<UI_Step1>();
        ui_step1.Get<GameObject>((int)UI_Step1.GameObjects.TableBlocker).SetActive(false);

        if (time >= deadLine)
        {
            ui_game.Get<GameObject>((int)UI_Game.GameObjects.StressBar).GetComponent<UI_GaugeBar>().GaugeSpeedDown(1);
        }
    }

    IEnumerator CountDeadLine()
    {
        while (true)
        {
            Get<Text>((int)Texts.Deadline).text = ConvertSecondsToMinutesAndSeconds(time);
            time++;
            if (time == deadLine)
            {
                UI_Game ui_game = transform.parent.parent.gameObject.GetComponent<UI_Game>();
                ui_game.Get<GameObject>((int)UI_Game.GameObjects.StressBar).GetComponent<UI_GaugeBar>().GaugeSpeedUp(1);
            }
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
