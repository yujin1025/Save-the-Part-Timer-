using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Managers : MonoBehaviour
{

    string playerNameKey = "PlayerName";
    string firstGameKey = "IsFirstGame";
    string firstRetireKey = "IsFirstRetire";
    string unlockedRecipesKey = "UnlockedRecipes";
    string availablePizzasKey = "AvailablePizzas";
    string moneyKey = "Money";
    string dDayKey = "DDay";
    string stageNameStrKey = "StageNameStr";


    [System.Serializable]
    public class PizzaRecipe
    {
        public string name;
        public Steps steps;
        public string[] ingredientImagesInOrder;
        public int price;
    }

    [System.Serializable]
    public class Steps
    {
        public string[] STEP1;
        public string[] STEP2;
        public string[] STEP3;
    }

    [System.Serializable]
    public class PizzaRecipeList
    {
        public PizzaRecipe[] recipes;
        public string[] ReturnStepData(string pizzaName, int step)
        {
            foreach (PizzaRecipe recipe in recipes)
            {
                if (recipe.name == pizzaName)
                {
                    if (step == 0) return recipe.steps.STEP1;
                    else if (step == 1) return recipe.steps.STEP2;
                    else if (step == 2) return recipe.steps.STEP3;
                }
            }
            Debug.Log("피자 못찾음");
            return null;
        }
        public string[] ReturnIngredientOrder(string pizzaName)
        {
            foreach (PizzaRecipe recipe in recipes)
            {
                if (recipe.name == pizzaName)
                {
                    return recipe.ingredientImagesInOrder;
                }
            }
            Debug.Log("피자 못찾음");
            return null;
        }
        public int ReturnPriceOrder(string pizzaName)
        {
            foreach (PizzaRecipe recipe in recipes)
            {
                if (recipe.name == pizzaName)
                {
                    return recipe.price;
                }
            }
            Debug.Log("피자 못찾음");
            return 0;
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public string StageNameStr;
        public int CustomerInterval;
        public float GuageSpeed;
        public float Cheese;
        public float Pepperoni;
        public float BaconPotato;
        public float PepperoniAndBaconPotato;
        public float Bulgogi;
        public float BaconPotatoAndBulgogi;
        public float Corn;
        public float BulgogiAndCorn;
        public float Combination;
        public int TotalRatio;
    }

    [System.Serializable]
    public class LevelDataList
    {
        public LevelData[] levels;

        public LevelData GetLevel(string stageNameStr)
        {
            foreach (LevelData level in levels)
            {
                if (level.StageNameStr == stageNameStr)
                    return level;
            }
            return null;
        }
    }

    static List<Sprite> PizzaImageList; 
    public static Dictionary<string, Sprite> PizzaIngredientSpriteDic;
    public static Dictionary<int, string> pizzaDec;

    public string playerNameProperty { get { return PlayerPrefs.GetString(playerNameKey, "Guest"); } set { PlayerPrefs.SetString(playerNameKey, value); } }
    public bool isFirstGameProperty { get { if (PlayerPrefs.GetInt(firstGameKey, 1) == 1) return true; else return false; } set { if (value == true) PlayerPrefs.SetInt(firstGameKey, 1); else PlayerPrefs.SetInt(firstGameKey, 0); } }
    public bool isFirstRetireProperty { get { return PlayerPrefs.GetInt(firstRetireKey, 1) == 1; } set { PlayerPrefs.SetInt(firstRetireKey, value ? 1 : 0); } }
    public List<string> unlockedRecipesProperty { get { return Util.GetListFromString(PlayerPrefs.GetString(unlockedRecipesKey, "")); } set { PlayerPrefs.SetString(unlockedRecipesKey, Util.GetStringFromList(value)); } }
    public List<string> availablePizzasProperty { get { return Util.GetListFromString(PlayerPrefs.GetString(availablePizzasKey, "")); } set { PlayerPrefs.SetString(availablePizzasKey, Util.GetStringFromList(value)); } }
    public int moneyProperty { get { return PlayerPrefs.GetInt(moneyKey, 0); } set { PlayerPrefs.SetInt(moneyKey, value); } }
    //계약 일수 넣어야 함
    public int dDayProperty { get { return PlayerPrefs.GetInt(dDayKey, 1); } set { PlayerPrefs.SetInt(dDayKey, value); } }
    public string stageNameStrProperty { get { return PlayerPrefs.GetString(stageNameStrKey, "stage 1"); } set { PlayerPrefs.SetString(stageNameStrKey, value); } }

    static public PizzaRecipeList pizzaRecipeList;
    static public LevelDataList levelDataList;


    static Managers s_managers;
    public static Managers s_managersProperty { get { Init(); return s_managers; } }

    InputManager inputManager = new InputManager();
    public static InputManager inputManagerProperty { get { return s_managersProperty.inputManager; } }

    ResourceManager resourceManager = new ResourceManager();
    public static ResourceManager resourceManagerProperty { get { return s_managersProperty.resourceManager; } }

    UIManager uiManager = new UIManager();
    public static UIManager uiManagerProperty { get { return s_managersProperty.uiManager; } }

    SceneManagerEX sceneManager = new SceneManagerEX();
    public static SceneManagerEX sceneManagerEXProperty { get { return s_managersProperty.sceneManager; } }

    static SoundManager soundManager = new SoundManager();
    public static SoundManager soundManagerProperty { get { soundManager.Init(); return Managers.soundManager; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.OnUpdate();
    }

    static void Init()
    {
        if (s_managers != null) return;

        GameObject objectManagers = GameObject.Find("@Managers");
        if (objectManagers == null)
        {
            objectManagers = new GameObject { name = "@Managers" };
            objectManagers.AddComponent<Managers>();
        }
        DontDestroyOnLoad(objectManagers);
        s_managers = objectManagers.GetComponent<Managers>(); ;
        LoadPizzaData();
        LoadLevelData();

        PizzaImageList = new List<Sprite>();
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Cheese"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Sauce"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Bacon"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Bulgogi"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Corn"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Mushroom"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Onion"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Pepper"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Pepperoni"));
        PizzaImageList.AddRange(Resources.LoadAll<Sprite>("Images/UI/Game/Pizza/Topping/Potato"));
        PizzaIngredientSpriteDic = new Dictionary<string, Sprite>();
        foreach(Sprite pizzaImage in PizzaImageList)
        {
            PizzaIngredientSpriteDic.Add(pizzaImage.name, pizzaImage);
        }

        pizzaDec = new Dictionary<int, string>();

        pizzaDec.Add(0, "치즈 피자");
        pizzaDec.Add(1, "페퍼로니 피자");
        pizzaDec.Add(2, "베이컨 포테이토 피자");
        pizzaDec.Add(3, "불고기 피자");
        pizzaDec.Add(4, "옥수수 피자");
        pizzaDec.Add(5, "페퍼로니&포테이토 반반 피자");
        pizzaDec.Add(6, "포테이토&불고기 반반 피자");
        pizzaDec.Add(7, "불고기&옥수수 반반 피자");
        pizzaDec.Add(8, "콤비네이션 피자");


        //실험할 난이도
        Managers.s_managersProperty.stageNameStrProperty = "stage 1";
    }

    static void LoadPizzaData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("PizzaRecipe");
        if (jsonFile != null)
        {
            Debug.Log("JSON file loaded successfully.");
            string dataAsJson = jsonFile.text;
            //Debug.Log("JSON data: " + dataAsJson);
            pizzaRecipeList = JsonUtility.FromJson<PizzaRecipeList>(dataAsJson);
            if (pizzaRecipeList != null)
            {
                //Debug.Log("JSON parsed successfully. Number of recipes: " + pizzaRecipeList.recipes.Length);
            }
            else
            {
                //Debug.LogError("Failed to parse JSON data.");
            }
        }
        else
        {
            //Debug.LogError("Cannot find json file!");
        }
    }

    public static void LoadLevelData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("LevelDesign");
        if (jsonFile != null)
        {
            Debug.Log("JSON file loaded successfully.");
            string dataAsJson = jsonFile.text;
            //Debug.Log("JSON data: " + dataAsJson);

            // Replace 'LevelDataList' with the appropriate class that matches your JSON structure
            levelDataList = JsonUtility.FromJson<LevelDataList>(dataAsJson);
            if (levelDataList != null)
            {
                //Debug.Log("JSON parsed successfully. Number of levels: " + levelDataList.levels.Length);
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
        else
        {
            Debug.LogError("Cannot find json file!");
        }
    }

    public static bool[] GetAvaliablePizzaInBool(string stageName)
    {
        bool[] result = new bool[9];
        for (int i = 0; i < 9; i++) result[i] = false;

        LevelData currentLevelData = Managers.levelDataList.GetLevel(stageName);
        if (currentLevelData.Cheese != 0) result[0] = true;
        if (currentLevelData.Pepperoni != 0) result[1] = true;
        if (currentLevelData.BaconPotato != 0) result[2] = true;
        if (currentLevelData.Bulgogi != 0) result[3] = true;
        if (currentLevelData.Corn != 0) result[4] = true;
        if (currentLevelData.PepperoniAndBaconPotato != 0) result[5] = true;
        if (currentLevelData.BaconPotatoAndBulgogi != 0) result[6] = true;
        if (currentLevelData.BulgogiAndCorn != 0) result[7] = true;
        if (currentLevelData.Combination != 0) result[8] = true;

        return result;
    }

    public void ResetGameState()
    {
        playerNameProperty = "Guest";
        isFirstGameProperty = true;
        isFirstRetireProperty = true;
        unlockedRecipesProperty = new List<string>();
        availablePizzasProperty = new List<string>();
        moneyProperty = 0;
        dDayProperty = 1;
        stageNameStrProperty = "stage 1";
    }
}
