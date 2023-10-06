using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
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
    }
}
