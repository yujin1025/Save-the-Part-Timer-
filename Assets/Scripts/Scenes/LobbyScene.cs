using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        base.Init();
        Managers managers = Managers.s_managersProperty;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
