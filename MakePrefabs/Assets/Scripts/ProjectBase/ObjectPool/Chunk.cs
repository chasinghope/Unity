using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    /// <summary>
    /// 池子容器
    /// </summary>
    private List<GameObject> objectList;

    /// <summary>
    /// 父层级
    /// </summary>
    private GameObject fatherObj;

    /// <summary>
    /// 初始化容器
    /// </summary>
    public Chunk(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        objectList = new List<GameObject>();
        RevertObj(obj);
    }

    /// <summary>
    /// 是否存在对象
    /// </summary>
    public bool IsHave => objectList.Count > 0;

    /// <summary>
    /// 从池子里取出对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        // 取出最后一个
        GameObject obj = objectList[0];
        // 从池子中删除
        objectList.RemoveAt(0);

        // 激活让其显示并断开父子关系
        if(obj != null)
        {
            obj.SetActive(true);
            obj.transform.parent = null;
        }


        return obj;
    }


    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj">要回收的对象</param>
    public void RevertObj(GameObject obj)
    {
        // 失活  让其显示
        obj.SetActive(false);
        // 设置父对象
        obj.transform.parent = fatherObj.transform;
        objectList.Add(obj);
    }
}
