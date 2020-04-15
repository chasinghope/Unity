using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class MSettingPanel : UIBase
{
    TMP_Dropdown dropDown;
     List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        // FindChildrenControl<TMP_Dropdown>();
        FindChildrenControl<TMP_Dropdown>();
    }

    private void Start()
    {
        GetControl<Button>("CloseBtn").onClick.AddListener(CloseBtn_click);
        dropDown = GetControl<TMP_Dropdown>("MusicDropdown"); 
        MusicDropdown_AddData();
        dropDown.onValueChanged.AddListener((int v) => MusicDropDown_valueChange(v) );
    }

    private void CloseBtn_click()
    {
        Hide();
    }

    private void MusicDropdown_AddData()
    {
        listOptions.Add(new TMP_Dropdown.OptionData("BGM1"));
        listOptions.Add(new TMP_Dropdown.OptionData("BGM2"));
        listOptions.Add(new TMP_Dropdown.OptionData("BGM3"));
        listOptions.Add(new TMP_Dropdown.OptionData("BGM4"));
        listOptions.Add(new TMP_Dropdown.OptionData("BGM5"));
        dropDown.AddOptions(listOptions);

        // dropDown.captionText.fontSize = 14;
        // dropDown.itemText.fontSize = 15;
    }

    private void MusicDropDown_valueChange(int v)
    {
        GameManager.Instance.BGMName = listOptions[v].text;
    }

}
