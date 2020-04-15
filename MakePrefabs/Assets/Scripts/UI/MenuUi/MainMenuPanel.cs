using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIBase
{
    private void Start()
    {
        GetControl<Button>("StartBtn").onClick.AddListener(StartBtn_click);
        GetControl<Button>("QuitBtn").onClick.AddListener(QuitBtn_click);
        GetControl<Button>("SettingBtn").onClick.AddListener(SettingBtn_click);
        GetControl<Button>("InfoBtn").onClick.AddListener(InfoBtn_click);

    }

    private void StartBtn_click()
    {
        // Debug.Log("点击开始按钮");
        // Debug.Log(GameManager.Instance.BGMName);
        UIManager.Instace.ShowPanel<LevelPanel>("LevelPanel", UILayer.mid);
    }

    private void QuitBtn_click()
    {
        // Debug.Log("点击退出按钮");
        Application.Quit();
    }

    /// <summary>
    /// 打开MSettingPanel  ->  主菜单设置面板
    /// </summary>
    private void SettingBtn_click()                
    {
        // Debug.Log("点击设置按钮");
        UIManager.Instace.ShowPanel<MSettingPanel>("MSettingPanel", UILayer.mid);
    }


    /// <summary>
    /// 打开MInfoPanel  ->   主菜单信息面板
    /// </summary>
    private void InfoBtn_click()
    {
        // Debug.Log("点击信息按钮");
        UIManager.Instace.ShowPanel<MInfoPanel>("MInfoPanel",UILayer.mid);
    }
}
