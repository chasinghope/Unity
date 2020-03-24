using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{ }



public class EventInfo<T>:IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        this.actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        this.actions += action;
    }
}

/// <summary>
/// 事件中心   单例模式对象
/// 字典/委托/观察者模式
/// </summary>
public class EventManager : InstanceNull<EventManager>
{
    /// <summary>
    /// string —— 事件名
    /// UnityAction —— 对应监听事件的委托函数们
    /// </summary>
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    #region 泛型有参类型事件

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">准备处理事件的委托函数</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (IsHaveEvent(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            // TODO
            if (action != null)
            {
                eventDic.Add(name, new EventInfo<T>(action));
            }

        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">移除事件的委托函数</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (IsHaveEvent(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }


    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪儿个名字的事件触发</param>
    public void EventTrigger<T>(string name, T info)
    {
        if (IsHaveEvent(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
                // eventDic[name].Invoke(info);
            }

        }
    }
    #endregion

    #region 非泛型无参类型事件
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">准备处理事件的委托函数</param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (IsHaveEvent(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            // TODO
            if (action != null)
            {
                eventDic.Add(name, new EventInfo(action));
            }

        }
    }


    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">移除事件的委托函数</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (IsHaveEvent(name))
        {
            ( eventDic[name] as EventInfo ).actions -= action;
        }
    }


    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪儿个名字的事件触发</param>
    public void EventTrigger(string name)
    {
        if (IsHaveEvent(name))
        {
            if ((eventDic[name] as EventInfo ).actions != null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();
                // eventDic[name].Invoke(info);
            }

        }
    }



    #endregion


    /// <summary>
    /// 清空事件中心，
    /// 主要用在场景切换时
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }

    private bool IsHaveEvent(string name)
    {
        return eventDic.ContainsKey(name);
    } 
}
