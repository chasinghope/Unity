using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool <T> where T: new()
{
	/// <summary>
	/// 泛型池子容器
	/// </summary>
    private List<T> objectList;

	/// <summary>
	/// 初始化容器池
	/// </summary>
	public ObjectPool()
	{
		objectList = new List<T>();
	}

	/// <summary>
	/// 是否存在对象
	/// </summary>
	public bool IsHave => objectList.Count > 0;

	/// <summary>
	/// 从池子中取出对象
	/// </summary>
	/// <returns></returns>
	public T GetObj()
	{

		// 取出第一个
		T obj = objectList[0];
		// 从池子中移除

		return obj;
	}

	/// <summary>
	/// 回收对象
	/// </summary>
	/// <param name="obj">要回收的对象</param>
	public void RevertObj(T obj)
	{
		objectList.Add(obj);
	}


}
