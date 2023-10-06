using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene currentSceneProperty
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }
    public void LoadScene(Defines.Scene type)
    {
        currentSceneProperty.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Defines.Scene type)
    {
        string name = System.Enum.GetName(typeof(Defines.Scene), type);
        return name;
    }
}
