using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShowInfoPanel : UIBase
{

    private TextMeshProUGUI timerText;
    private TextMeshProUGUI billsText;

    #region Unity Mono
    protected override void Awake()
    {
        base.Awake();
        FindChildrenControl<TextMeshProUGUI>();
    }

    private void Start()
    {
        GetControl<Button>("SettingBtn").onClick.AddListener( SettingBtn_onClick );
        timerText = GetControl<TextMeshProUGUI>("Timer");
        billsText = GetControl<TextMeshProUGUI>("Bills");
    }

    private void Update()
    {
        TextGetFormLevelMgr();
    }

    #endregion

    private void SettingBtn_onClick()
    {
        // TODO open SettingLevelPanel
        // 暂停游戏
        Time.timeScale = 0;
        UIManager.Instace.ShowPanel<SettingLevelPanel>("SettingLevelPanel", UILayer.top);
        MusicMgr.Instace.PauseBGM();
    }

    private void TextGetFormLevelMgr()
    {
        billsText.text = BillMgr.Instace.GetBill().ToString();
        timerText.text = LevelManager.Instance.LevelTimer;
    }



}
