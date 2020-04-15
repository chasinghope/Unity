using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverPanel : UIBase
{
    public List<GameObject> starList;

    private TextMeshProUGUI timerText;
    private TextMeshProUGUI billsText;
    private RectTransform rectTrans;
    protected override void Awake()
    {
        base.Awake();
        rectTrans = GetComponent<RectTransform>();
        FindChildrenControl<TextMeshProUGUI>();

        for (int i = 0; i < starList.Count; i++)
        {
            starList[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // 设置面板宽高
        rectTrans.sizeDelta = new Vector2(736,752);
        rectTrans.localPosition = new Vector3(0, 100, 0);

        //暂停游戏
        Time.timeScale = 0f;
        MusicMgr.Instace.StopBGM();

        // 获取子控件
        timerText = GetControl<TextMeshProUGUI>("Timer");
        billsText = GetControl<TextMeshProUGUI>("Bills");
        GetControl<Button>("NextBtn").onClick.AddListener(NextBtn_onClick);
        GetControl<Button>("RePlayBtn").onClick.AddListener(RePlayBtn_onClick);
        GetControl<Button>("MenuBtn").onClick.AddListener(MenuBtn_onClick);

        timerText.text = LevelManager.Instance.LevelTimer;
        billsText.text = BillMgr.Instace.GetBill().ToString();
        ShowStars();
    }


    private void NextBtn_onClick()
    {
        if( LevelManager.Instance.StarCount == 0)
        {
            return;
        }
        Debug.Log("TODO item  --> go next level");
        MusicMgr.Instace.StopBGM();
        
    }

    private void RePlayBtn_onClick()
    {
        MusicMgr.Instace.StopBGM();
        //TODO 隐藏Level关卡的所有Panel
        LevelManager.Instance.HideLevelUI();
        // 开启主菜单那加载面板
        UIManager.Instace.ShowPanel<LoadingPanel>("LoadingPanel", UILayer.top);
        //TODO 记载关卡界面
        SceneMgr.Instace.LoadSceneAsyn("Level" + LevelManager.Instance.LevelID, () => { Debug.Log("WeGo"); });
    }

    private void MenuBtn_onClick()
    {
        MusicMgr.Instace.StopBGM();
        //TODO 隐藏Level关卡的所有Panel
        LevelManager.Instance.HideLevelUI();

        // 同步加载至主菜单
        SceneMgr.Instace.LoadScene("MainMenu");
    }

    private void ShowStars()
    {
        for (int i = 0; i < LevelManager.Instance.StarCount; i++)
        {
            starList[i].gameObject.SetActive(true);
        }
    }


}
