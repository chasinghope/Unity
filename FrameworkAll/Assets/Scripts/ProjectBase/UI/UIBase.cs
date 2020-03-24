using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 面板基类，帮助我们通过代码，快速找到所有子控件
/// 方便我们在子类中处理逻辑
/// 节约我们找控件的工作量
/// </summary>
public class UIBase : MonoBehaviour
{
    // 通过里式转换原则 来存储所有控件       
    // string  -->  gameObject.name
    // List<UIBehaviour>     -->   该gameObject 所拥有的UI组件
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
    public bool IsShow => gameObject.activeSelf;

    // Start is called before the first frame update
    void Awake()
    {
        // 获取panel下的ui控件， 并存储在字典controlDic 中
        FindChildrenControl<Button>();
        FindChildrenControl<Text>();
        FindChildrenControl<Image>();
        FindChildrenControl<Slider>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Dropdown>();
        FindChildrenControl<InputField>();
        FindChildrenControl<RawImage>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 获取类型为 <T> 的子ui控件，存入到controlDic 中
    /// </summary>
    /// <typeparam name="T">找什么UI控件 demo  Button  Slider  Text...</typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        string objName;
        T[] control = gameObject.GetComponentsInChildren<T>();
        for (int i = 0; i < control.Length; i++)
        {
            objName = control[i].gameObject.name;
            if( controlDic.ContainsKey(objName) )
            {
                controlDic[objName].Add( control[i] );
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { control[i] });
            }
        }
    }



    /// <summary>
    /// 得到对应名字和对应控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    /// <param name="controlName">对象名字</param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if( controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if( controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            }
        }
        return null;
    }

    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void Show()
    {
        if (!IsShow)
        {
            gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void Hide()
    {
        if (IsShow)
        {
            gameObject.SetActive(false);
        }
    }



}
