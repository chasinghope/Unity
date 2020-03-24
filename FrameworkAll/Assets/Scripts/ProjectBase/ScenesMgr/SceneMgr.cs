﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// 1. 场景异步加载
/// 2. 协程
/// 3. 委托
/// </summary>
public class SceneMgr : InstanceNull<SceneMgr>
{

    /// <summary>
    /// 切换场景  同步
    /// </summary>
    /// <param name="name">场景名字</param>
    /// <param name="action"></param>
    public void LoadScene(string name, UnityAction fun)
    {
        // 场景同步加载
        SceneManager.LoadScene(name);

        // 场景加载完成之后才会执行
        fun();
    }

    /// <summary>
    /// 切换场景  异步加载
    /// </summary>
    /// <param name="name">场景名字</param>
    /// <param name="action"></param>
    public void LoadSceneAsyn(string name, UnityAction fun)
    {
        MonoManager.Instace.StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }


    /// <summary>
    /// 协程异步加载
    /// </summary>
    /// <param name="name">场景名字</param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        // 可以得到场景加载的一个进度
        while (!ao.isDone)
        {
            // 事件中心向外分发  进度情况  外面就想用就用
            EventManager.Instace.EventTrigger("LoadingSliderUpdate", ao.progress);
            // 这里面更新进度条
            yield return ao.progress;
        }

        fun();
    }
}