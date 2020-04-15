using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingLevelPanel : UIBase
{
    private Slider bgmSlider;
    private Slider soundSlider;
    private RectTransform rectTrans;
    private float bgmVolume = 1 ;
    private float soundVolume = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        rectTrans = GetComponent<RectTransform>();
        
        
    }

    private void Start()
    {


        // 设置面板宽度和高度
        rectTrans.sizeDelta = new Vector2(1175, 660);

        // 子控件事件绑定
        bgmSlider = GetControl<Slider>("BGMSlider");
        soundSlider = GetControl<Slider>("SoundSlider");

        GetControl<Button>("ReturnBtn").onClick.AddListener(  ReturnBtn_onClick );
        GetControl<Button>("BackBtn").onClick.AddListener( BackBtn_onClick );

        bgmSlider.value = bgmVolume;
        soundSlider.value = soundVolume;

        bgmSlider.onValueChanged.AddListener(BGMSlider_onValueChanged);
        soundSlider.onValueChanged.AddListener(SoundSlider_onValueChanged);
    }

    private void BGMSlider_onValueChanged(float volume)
    {
        bgmVolume = volume;
        MusicMgr.Instace.ChangeBGMVolume(volume);
    }

    private void SoundSlider_onValueChanged(float volume)
    {
        soundVolume = volume;
        MusicMgr.Instace.ChangeSoundVolume(volume);
    }

    private void ReturnBtn_onClick()
    {
        // 游戏状态从   暂停-> 恢复
        Time.timeScale = 1;
        UIManager.Instace.HidePanel("SettingLevelPanel");
        MusicMgr.Instace.PlayBGM(GameManager.Instance.BGMName);
    }

    private void BackBtn_onClick()
    {
        MusicMgr.Instace.StopBGM();
        //TODO 隐藏Level关卡的所有Panel
        LevelManager.Instance.HideLevelUI();

        SceneMgr.Instace.LoadScene("MainMenu");
    }
}
