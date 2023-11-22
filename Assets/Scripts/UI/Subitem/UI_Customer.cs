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
        Get<Image>((int)Images.CustomerBackGround).gameObject.SetActive(false);
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
        if (ui_game.pizzaMakingOnGoing == false)
        {
            //선택되었음을 표시
            Get<Image>((int)Images.CustomerBackGround).gameObject.SetActive(true);

            //피자를 만들기 시작했다고 체크
            ui_game.pizzaMakingOnGoing = true;

            //이름을 전달
            ui_game.orderName = orderName;

            ui_game.ingredientOrder = Managers.pizzaRecipeList.ReturnIngredientOrder(ui_game.orderName);

            //소스 정답 전달
            ui_game.sauceAnswer[0] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("토마토 소스");
            ui_game.sauceAnswer[1] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("마요네즈");
            ui_game.sauceAnswer[2] = Managers.pizzaRecipeList.ReturnStepData(orderName, 0).Contains<string>("구운 양파 소스");

            Debug.Log($"{ui_game.sauceAnswer[0]}, {ui_game.sauceAnswer[1]}, {ui_game.sauceAnswer[2]}");

            //테이블을 활성화
            UI_Step1 ui_step1 = ui_game.ui_step1;

            ui_step1.Get<GameObject>((int)UI_Step1.GameObjects.TableBlocker).SetActive(false);

            ui_game.selectedCustomer = this;
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
        time = 0;
        Get<Image>((int)Images.CustomerBackGround).gameObject.SetActive(false);
        Get<Text>((int)Texts.Order).text = orderName;
        StartCoroutine(CountDeadLine());
    }
}
