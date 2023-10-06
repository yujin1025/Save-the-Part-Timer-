using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> m_object = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        m_object.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            //여기서의 gameObject 는 최상위 GameObject
            if (typeof(T) == typeof(GameObject)) objects[i] = Util.FindChild(gameObject, names[i], true);
            else objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            if (objects[i] == null) Debug.Log($"Failed to bind : {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (m_object.TryGetValue(typeof(T), out objects) == false) return null;
        return objects[idx] as T;
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Defines.UIEvent type = Defines.UIEvent.Click)
    {

        UI_EventHandler uiHendlerScript = Util.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Defines.UIEvent.Click:
                uiHendlerScript.OnClickHandler -= action;
                uiHendlerScript.OnClickHandler += action;
                break;
            
        }
    }
}
