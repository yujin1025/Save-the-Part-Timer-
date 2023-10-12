using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    public Text ScriptTxt;
    private int RandomInt;
    private string script;

    void Start()
    {
        ScriptTxt.text = "기본 대사";
    }

    public void CharacterText()
    {
        ScriptTxt.text = RandomScript();
    }

    
    public string RandomScript()
    {
        RandomInt = Random.Range(0, 6);

        if (RandomInt == 0)
            script = "1번 대사";
        else if (RandomInt == 1)
            script = "2번 대사";
        else if (RandomInt == 2)
            script = "3번 대사";
        else if (RandomInt == 3)
            script = "4번 대사";
        else if (RandomInt == 4)
            script = "5번 대사";
        else if (RandomInt == 5)
            script = "6번 대사";

        return script;
    }
}
