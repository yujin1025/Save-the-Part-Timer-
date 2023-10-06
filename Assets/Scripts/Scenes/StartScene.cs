using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Managers mag = Managers.s_managersProperty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
