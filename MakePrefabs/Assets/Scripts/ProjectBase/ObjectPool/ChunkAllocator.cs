using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChunkAllocator : InstanceNull<ChunkAllocator>
{
    /// <summary>
    /// 缓存池子容器
    /// </summary>
    private Dictionary<string, Chunk> chunkList;

    /// <summary>
    /// 缓存池名字
    /// </summary>
    private GameObject poolObj;
    public GameObject PoolObj 
    {
        get
        {
            if (poolObj == null)
                poolObj = new GameObject("Pool");
            return poolObj;
        }
     }


    public ChunkAllocator()
    {
        chunkList = new Dictionary<string, Chunk>();
    }



    /// <summary>
    /// 获取预设体对象
    /// </summary>
    /// <param name="poolName">缓存池名需与预制体名保持一致</param>
    /// <param name=""></param>
    /// <returns></returns>
    public void GetPrefab(string poolName, UnityAction<GameObject> callback, Transform parent = null)
    {

        if(IsHavePool(poolName) && chunkList[poolName].IsHave)
        {
            // go = chunkList[poolName].GetObj() as GameObject;
            callback(chunkList[poolName].GetObj());
        }
        else
        {
            ///   go = GameObject.Instantiate(Resources.Load<GameObject>(poolName));       同步加载改为异步加载
            ResMgr.Instace.LoadAsync<GameObject>(poolName, (o) =>
            {
                o.name = poolName;
                if (parent != null)
                    o.transform.SetParent(parent);
                callback(o);
            });
        }
    }

    /// <summary>
    /// 回收游戏对象
    /// </summary>
    /// <param name="poolName">缓存池名</param>
    /// <param name="obj">回收的对象</param>
    public void PushPrefab(string poolName, GameObject obj)
    {
        if (IsHavePool(poolName))
        {
            chunkList[poolName].RevertObj(obj);
        }
        else
        {
            Chunk chunk = new Chunk(obj, PoolObj);
            chunk.RevertObj(obj);
            chunkList.Add(poolName, chunk);
        }

    }


    /// <summary>
    /// 清空缓存池
    /// </summary>
    /// <param name="poolName">要清空的缓存名字，若不传参数，则全部清空</param>
    public void ClearPool(string poolName = "")
    {
        if(poolName == "")
        {
            chunkList.Clear();
            return;
        }
        if (IsHavePool(poolName))
            chunkList.Remove(poolName);
    }

    /// <summary>
    /// 判断是否有该缓冲池
    /// </summary>
    /// <param name="poolName">缓冲池名字</param>
    /// <returns></returns>
    private bool IsHavePool(string poolName)
    {
        return chunkList.ContainsKey(poolName);
    }
}
