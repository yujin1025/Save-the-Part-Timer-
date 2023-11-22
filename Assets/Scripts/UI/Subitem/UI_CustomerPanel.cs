using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Managers;

public class UI_CustomerPanel : UI_Base
{
    GameObject[] customers;
    Sprite[] customersIcon;
    float customerInterval;
    float[] ratioList;
    
    public override void Init()
    {
        customers = new GameObject[4];
        customersIcon = new Sprite[5];
        ratioList = new float[9];

        for (int i = 0; i < customersIcon.Length; i++)
        {
            customersIcon[i] = Managers.resourceManagerProperty.Load<Sprite>($"Images/UI/Game/Customer/Customer{i + 1}");
        }

        foreach (Transform child in transform)
            Managers.resourceManagerProperty.Destroy(child.gameObject);

        for (int i = 0; i < 4; i++)
        {
            GameObject pizzaIndex = Managers.uiManagerProperty.MakeSubItem<UI_Customer>(transform).gameObject;
            pizzaIndex.SetActive(false);
            pizzaIndex.name = $"CustomerInfo{i}";
            customers[i] = pizzaIndex;
        }

        LevelData currentLevelData = Managers.levelDataList.GetLevel(Managers.s_managersProperty.stageNameStrProperty);
        customerInterval = currentLevelData.CustomerInterval;

        ratioList[0] = currentLevelData.Cheese;
        ratioList[1] = ratioList[0] + currentLevelData.Pepperoni;
        ratioList[2] = ratioList[1] + currentLevelData.BaconPotato;
        ratioList[3] = ratioList[2] + currentLevelData.PepperoniAndBaconPotato;
        ratioList[4] = ratioList[3] + currentLevelData.Bulgogi;
        ratioList[5] = ratioList[4] + currentLevelData.BaconPotatoAndBulgogi;
        ratioList[6] = ratioList[5] + currentLevelData.Corn;
        ratioList[7] = ratioList[6] + currentLevelData.BulgogiAndCorn;
        ratioList[8] = ratioList[7] + currentLevelData.Combination;
        
        StartCoroutine(CustomerTest());
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    IEnumerator CustomerTest()
    {
        while(true)
        {
            MakeOrder(ratioList);
            yield return new WaitForSeconds(customerInterval);
        }
    }
    void MakeOrder(float[] raito = null, int deadLine = 10)
    {
        float randomNumber = Random.Range(0f, 100f);
        GameObject firstEmpty = null;

        if (raito == null) raito = new float[] { 15, 30, 45, 60, 70, 80, 90, 95, 100 };

        for (int i = 0; i < 4; i++)
        {
            if (customers[i].activeSelf == false)
            {
                firstEmpty = customers[i];
            }
        }

        if (firstEmpty == null)
        {
            //Debug.Log("CustomerFull");
            return;
        }
        
        int selectedCustomerNum = Random.Range(0, 5);
        firstEmpty.GetComponent<UI_Customer>().customerIconNumber = selectedCustomerNum;
        firstEmpty.GetComponent<UI_Customer>().Get<Image>((int)UI_Customer.Images.CustomerIcon).sprite = customersIcon[selectedCustomerNum];
        firstEmpty.GetComponent<UI_Customer>().deadLine = deadLine;

        if (randomNumber < raito[0])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "치즈 피자";
        }
        else if (randomNumber < raito[1])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "페퍼로니 피자";
        }
        else if (randomNumber < raito[2])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "베이컨 포테이토 피자";
        }
        else if (randomNumber < raito[3])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "불고기 피자";
        }
        else if (randomNumber < raito[4])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "옥수수 피자";
        }
        else if (randomNumber < raito[5])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "페퍼로니&포테이토 반반 피자";
        }
        else if (randomNumber < raito[6])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "포테이토&불고기 반반 피자";
        }
        else if (randomNumber < raito[7])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "불고기&옥수수 반반 피자";
        }
        else if (randomNumber < raito[8])
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "콤비네이션 피자";
        }
        else
        {
            firstEmpty.GetComponent<UI_Customer>().orderName = "??? 피자";
        }
        firstEmpty.SetActive(true);
        firstEmpty.transform.SetAsLastSibling();
    }
}
