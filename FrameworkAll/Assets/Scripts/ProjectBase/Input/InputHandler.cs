using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler 
{
	private bool isRegister;
	private UnityAction<KeyCode> actionDown;
	private UnityAction<KeyCode> actionUp;
	private UnityAction<KeyCode> actionHold;
	
	public InputHandler(UnityAction<KeyCode> actionHold = null, UnityAction<KeyCode> actionDown = null, UnityAction<KeyCode> actionUp = null)
	{
		InputMgr.Instace.RegisterOpen();
		isRegister = true;

		this.actionHold = actionHold;
		this.actionDown = actionDown;
		this.actionUp = actionUp;

		BindInputHold(this.actionHold);
		BindInputDown(this.actionDown);
		BindInputUp(this.actionUp);
	}

	/// <summary>
	/// 绑定按下键  功能
	/// </summary>
	/// <param name="fun">按下绑定函数</param>
	private void BindInputDown(UnityAction<KeyCode> fun)
	{
		EventManager.Instace.AddEventListener<KeyCode>("某键按下", fun);
	}

	/// <summary>
	/// 绑定抬起键  功能 
	/// </summary>
	/// <param name="fun"></param>
	private void BindInputUp(UnityAction<KeyCode> fun)
	{
		EventManager.Instace.AddEventListener<KeyCode>("某键抬起", fun);
	}

	/// <summary>
	/// 绑定按住键  功能 
	/// </summary>
	/// <param name="fun"></param>
	private void BindInputHold(UnityAction<KeyCode> fun)
	{
		EventManager.Instace.AddEventListener<KeyCode>("某键按住", fun);
	}

	/// <summary>
	/// 取消输入控制功能
	/// </summary>
	public void CancelHandler()
	{
		if (isRegister)
		{
			isRegister = false;
			EventManager.Instace.RemoveEventListener("某键按住", this.actionHold);
			EventManager.Instace.RemoveEventListener("某键按下", this.actionDown);
			EventManager.Instace.RemoveEventListener("某键抬起", this.actionUp);
			InputMgr.Instace.RegisterClose();
		}
	}

	/// <summary>
	/// 恢复输入控制功能
	/// </summary>
	public void RevertHandler()
	{
		if (!isRegister)
		{
			isRegister = true;
			InputMgr.Instace.RegisterOpen();
			BindInputHold(this.actionHold);
			BindInputDown(this.actionDown);
			BindInputUp(this.actionUp);
		}
	}


	/*   Demo
	public  void WASD(object key)
	{
		KeyCode code = (KeyCode)key;
		switch (code)
		{
			case KeyCode.W:                                           // 向上
				break;
			case KeyCode.A:                                           // 向左
				break;
			case KeyCode.S:                                          // 向下
				break;
			case KeyCode.D:                                         // 向右
				break;
			case KeyCode.Space:                                // 跳跃 
				break;
		}
	}
	*/
}
