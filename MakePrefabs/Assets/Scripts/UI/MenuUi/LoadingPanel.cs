using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LoadingPanel : UIBase
{
    private string startString = "Loading    ";
    private string endString = "%";
    private TextMeshProUGUI progressText;
    private Slider progressSlider;
    protected override void Awake()
    {
        base.Awake();

        FindChildrenControl<TextMeshProUGUI>();
    }

    private void Start()
    {
        progressSlider = GetControl<Slider>("ProgressSlider");
        progressText = GetControl<TextMeshProUGUI>("Progress");
        if(progressText == null)
        {
            Debug.LogError("TextMeshPro error");
        }
        EventManager.Instace.AddEventListener<float>("LoadingSliderUpdate", LoadingLevel );

        progressText.text = "why ";

    }

    private void LoadingLevel(float progress)
    {
        // TODO 加载到新场景后 退出LoadingPanel  -->  交给LevelMgr 处理；
        progressSlider.value = progress;
        progressText.text = startString + ((int)(progress * 100)).ToString() + endString;
        if (progress >= 0.87)
        {
            progressText.text = "Press any key";
            
        }




    }


}
