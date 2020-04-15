using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : UIBase
{
    private RectTransform rectTrans;

    protected override void Awake()
    {
        base.Awake();
        rectTrans = GetComponent<RectTransform>();
        // rectTrans.sizeDelta = new Vector2(1176, 811);
    }

    private void Start()
    {
        GetControl<Button>("CloseBtn").onClick.AddListener(CloseBtn_click);
        rectTrans.sizeDelta = new Vector2(1176, 811);


    }

    private void CloseBtn_click()
    {
        Hide();
    }
}
