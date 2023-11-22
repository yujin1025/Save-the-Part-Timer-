using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject Settings;
    public Button SettingBtn;

    void Start()
    {
        SettingBtn.onClick.AddListener(ShowSetting);
    }

    void ShowSetting()
    {
        Settings.SetActive(!Settings.activeSelf);
    }
}
