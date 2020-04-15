using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入管理
/// 1. Input类
/// 2. 事件中心管理
/// 3. 公共Mono模块
/// </summary>
public class InputMgr : InstanceNull<InputMgr>
{
	private int isOpen = 0;
	public InputMgr()
	{
		MonoManager.Instace.AddUpdateListener(InputUpdate);
	}

	private void InputUpdate()
	{
		if (isOpen <= 0)
			return;

		foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
		{
			CheckKeyCode(code);
		}
		// CheckKeyCode(KeyCode.W);
		// CheckKeyCode(KeyCode.A);
		// CheckKeyCode(KeyCode.S);
		// CheckKeyCode(KeyCode.D);

	}

	public void RegisterOpen()
	{
		this.isOpen += 1;
	}

	public void RegisterClose()
	{
		this.isOpen -= 1;
	}

	private void CheckKeyCode(KeyCode key)
	{
		// 检测某键是否按下
		if( Input.GetKeyDown(key))
		{
			EventManager.Instace.EventTrigger<KeyCode>("某键按下", key);
		}
		// 检测某键是否抬起
		if (Input.GetKeyUp(key))
		{
			EventManager.Instace.EventTrigger<KeyCode>("某键抬起", key);
		}

		if (Input.GetKey(key))
		{
			EventManager.Instace.EventTrigger<KeyCode>("某键按住", key);
		}
	}

}
