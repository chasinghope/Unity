using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserControlPanel : UIBase
{
    private List<GameObject> allControlPanel = new List<GameObject>();
    private RectTransform rectTrans;
    private TextMeshProUGUI selectionText;

    protected override void Awake()
    {
        base.Awake();
        rectTrans = GetComponent<RectTransform>();
        FindChildrenControl<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 设置UI坐标
        rectTrans.sizeDelta = new Vector2(1149.1f, 329.7f);
        rectTrans.localPosition = new Vector3(0, -371f, 0);

        selectionText = GetControl<TextMeshProUGUI>("Selection");

        MakeAllPanel();
        SwitchPanel();
    }

    private void GoControlPanel(int num)
    {
        // TODO 打开name 的panel ，其他panel关闭
        for (int i = 0; i < allControlPanel.Count; i++)
        {
            allControlPanel[i].SetActive(false);
        }
        allControlPanel[num].SetActive(true);
    }

    private void MakeAllPanel()
    {
        GameObject tmpObj = null;

        tmpObj = GetControl<Image>("Castle1").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Castle2").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Castle3").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Castle4").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Castle5").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Towers").gameObject;
        allControlPanel.Add(tmpObj);
        tmpObj = GetControl<Image>("Block").gameObject;
        allControlPanel.Add(tmpObj);

        GoControlPanel(0);
        selectionText.text = "Castle1";
    }

    /// <summary>
    /// 为SwitchPanel 面板 按钮切换ControlPanel 添加 点击事件
    /// </summary>
    private void SwitchPanel()
    {
        GetControl<Button>("C1_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(0);
                selectionText.text = "Castle1";
            });

        GetControl<Button>("C2_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(1);
                selectionText.text = "Castle2";
            });

        GetControl<Button>("C3_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(2);
                selectionText.text = "MainCastle";
            });

        GetControl<Button>("C4_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(3);
                selectionText.text = "Castle4";
            });

        GetControl<Button>("C5_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(4);
                selectionText.text = "Castle5";
            });

        GetControl<Button>("T_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(5);
                selectionText.text = "Tower";
            });

        GetControl<Button>("B_btn").onClick.AddListener(
            () =>
            {
                GoControlPanel(6);
                selectionText.text = "Block";
            });
    }

}
