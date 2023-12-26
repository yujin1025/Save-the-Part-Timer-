using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    //UI_Popup에서 직접 관리할 것
    int m_order = 10;

    //GameObject 보다 실질적인 Popup 역할을 하는 건 UI_Popup
    Stack<UI_Popup> m_popupStack = new Stack<UI_Popup>();
    UI_Scene m_sceneUI = null;

    public GameObject RootProperty
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null) root = new GameObject { name = "@UI_Root" };
            return root;

        }
    }
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = m_order;
            m_order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.resourceManagerProperty.Instantate($"UI/SubItem/{name}");

        if (parent != null) go.transform.SetParent(parent);
        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        //Prefab 이름을 넣어주지 않았다면 Script 이름을 그대로 가져옴
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.resourceManagerProperty.Instantate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        m_sceneUI = sceneUI;

        go.transform.SetParent(RootProperty.transform);
        return sceneUI;
    }

    //T는 스크립트 이름, name 은 Prefab 이름 
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        //Prefab 이름을 넣어주지 않았다면 Script 이름을 그대로 가져옴
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.resourceManagerProperty.Instantate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        m_popupStack.Push(popup);

        go.transform.SetParent(RootProperty.transform);
        return popup;
    }

    public T ShowPopupUIUnderParent<T>(GameObject parantObject, string name = null) where T : UI_Popup
    {
        //Prefab 이름을 넣어주지 않았다면 Script 이름을 그대로 가져옴
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.resourceManagerProperty.Instantate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        m_popupStack.Push(popup);

        go.transform.SetParent(parantObject.transform);
        return popup;
    }

    public bool IsPopupOpen<T>() where T : UI_Popup
    {
        foreach (var popup in m_popupStack)
        {
            if (popup is T)
                return true;
        }
        return false;
    }


    public void SafeClosePopupUIOnTop(UI_Popup popup)
    {
        if (m_popupStack.Count == 0) return;

        if (m_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Faild");
            return;
        }
        ClosePopupUIOnTop();
    }

    public void ClosePopupUIOnTop()
    {
        //Stack 에서는 Count check 를 생활화 할 필요 있음
        if (m_popupStack.Count == 0) return;
        UI_Popup popup = m_popupStack.Pop();
        if (popup != null) Managers.resourceManagerProperty.Destroy(popup.gameObject);
        popup = null;
    }

    public void CloseAllPopupUI()
    {
        while (m_popupStack.Count > 0)
            ClosePopupUIOnTop();
    }

}
