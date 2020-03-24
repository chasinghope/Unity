using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum UILayer
{
    bot,
    mid,
    top,
    system
}
/// <summary>
/// UI管理器
/// 1. 管理所有显示的面板
/// 2. 提供给外部  显示和隐藏等等接口
/// </summary>
public class UIManager : InstanceNull<UIManager>
{
    public Dictionary<string, UIBase> panelDic = new Dictionary<string, UIBase>();
    private string uiPath = "UI/";                // UIpanel 在 Resources 目录下的存放目录

    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    public UIManager()
    {
        // 创建Canvas让其在过场景的时候不被移除
        GameObject obj = ResMgr.Instace.Load<GameObject>(uiPath+"Canvas");
        Transform canvas = obj.GetComponent<Transform>();
        GameObject.DontDestroyOnLoad(obj);
        
        // 找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        // 创建EventSystem让其在过场景的时候不被移除
        obj = ResMgr.Instace.Load<GameObject>(uiPath + "EventSystem");
        GameObject.DontDestroyOnLoad(obj);


    }


    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪儿一层   UILayer</param>
    /// <param name="callback">当面板创建成功后，你想做的事</param>
    public void ShowPanel<T>(string panelName, UILayer layer = UILayer.bot, UnityAction<T> callback = null) where T : UIBase
    {
        if( IsHavePanel(panelName) )
        {
            panelDic[panelName].Show();
        }
        else
        {
            Transform parent = null;
            ResMgr.Instace.LoadAsync<GameObject>(uiPath + panelName, (obj) =>
            {
                // 把他作为Canvas 的子对象
                // 并且要设置它的相对位置
                switch (layer)
                {
                    case UILayer.bot:
                        parent = bot;
                        break;
                    case UILayer.mid:
                        parent = mid;
                        break;
                    case UILayer.top:
                        parent = top;
                        break;
                    case UILayer.system:
                        parent = system;
                        break;
                }

                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;

                // 得到预设体身上的面板脚本
                T panel = obj.GetComponent<T>();
                
                // 处理面板创建完成后的逻辑
                if (callback != null)
                    callback(panel);

                panel.Show();

                // 把面板存起来
                panelDic.Add(panelName, panel);
            });
        }

    }


    /// <summary>
    /// 隐藏面板   
    /// </summary>
    /// <param name="panelName">要隐藏的panel</param>
    public void HidePanel(string panelName)
    {
        if( IsHavePanel(panelName))
        {
            panelDic[panelName].Hide();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 清空所有panel
    /// </summary>
    public void DestoryAllPanel()
    {
        panelDic.Clear();
    }


    /// <summary>
    /// 是否有对应名字的面板
    /// </summary>
    /// <param name="panelName"></param>
    private bool IsHavePanel(string panelName)
    {
        return panelDic.ContainsKey(panelName);
    }

    
}
