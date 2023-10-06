using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Defines.Scene sceneTypeProperty { get; protected set; } = Defines.Scene.Unknown;

    //여기에 Awake 함수를 만들고 Init 하면 확정적 실행 가능
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null) Managers.resourceManagerProperty.Instantate("UI/EventSystem").name = "@EventSystem";

    }

    public abstract void Clear();
}
