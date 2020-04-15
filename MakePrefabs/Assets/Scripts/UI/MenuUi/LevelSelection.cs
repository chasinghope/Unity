using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;      // Default value is false. 解锁

    // private bool isLock = true;

    [Header("关卡ID")]
    public int levelID;

    [Header("解锁面板")]
    public GameObject unlockedImage;
    public GameObject[] stars;
    public TextMeshProUGUI levelNum;

    [Header("锁定面板")]
    public GameObject lockedImage;

    #region Mono
    private void Start()
    {
        StarsInital();
        unlockedImage.GetComponent<Button>().onClick.AddListener( StartLevelBtn_onClick );
        levelNum.text = levelID.ToString();
    }


    private void Update()
    {
        // 更新LevelSelection UI
        UpdateLevelImage();
        UpdateLevelStatus();
    }
    #endregion



    #region 私有方法

    /// <summary>
    /// 更新LevelSelection UI
    /// </summary>
    private void UpdateLevelImage()
    {
        if( unlocked )               //  解锁状态
        {
            unlockedImage.SetActive(true);
            lockedImage.SetActive(false);

            // 获取改关卡星星数量    
            int starNum = PlayerPrefs.GetInt("LvStar" + levelID);
            // 在UI中更新星星数量
            for (int i = 0; i < starNum; i++)
            {
                stars[i].SetActive(false);
            }

        }
        else
        {
            lockedImage.SetActive(true);
            unlockedImage.SetActive(false);
        }
    }


    /// <summary>
    /// 更新关卡状态
    /// </summary>
    private void UpdateLevelStatus()
    {
        int beforeID = levelID - 1;
        if (PlayerPrefs.GetInt("LvStar" + beforeID) > 0)    // if the first level's star num >0 , then the second level you can play
        {
            unlocked = true;
        }
    }


    /// <summary>
    /// 初始星星，关闭所有星星
    /// </summary>
    private void StarsInital()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
    }

    private void StartLevelBtn_onClick()
    {
        // Debug.Log("开始加载界面" + levelID);
        // 开启主菜单那加载面板
        UIManager.Instace.ShowPanel<LoadingPanel>("LoadingPanel", UILayer.top);


        //TODO 记载关卡界面
        SceneMgr.Instace.LoadSceneAsyn("Level" + levelID, ()=>{ Debug.Log("WeGo");  });



        // 关闭主菜单所有面板   先关闭其他面板再关闭自己
        UIManager.Instace.HidePanel("MainMenuPanel");
        UIManager.Instace.HidePanel("MInfoPanel");
        UIManager.Instace.HidePanel("MSettingPanel");
        UIManager.Instace.HidePanel("LevelPanel");

        
    }

    #endregion

    

}
