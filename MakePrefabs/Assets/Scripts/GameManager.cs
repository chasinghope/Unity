using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public string BGMName { get; set; }


	#region Unity Mono

	public static GameManager Instance;

	private  void Awake()
	{
		if( Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		
	}


	private void Start()
	{
		GameInit();

	}





	#endregion




	private void GameInit()
	{
		// Debug.Log("游戏初始化");
		// 加载主菜单背景
		ResMgr.Instace.LoadAsync<GameObject>("Menu/MainMenuBG", (obj) => { });
		// 加载主菜单UI
		UIManager.Instace.ShowPanel<MainMenuPanel>("MainMenuPanel");
		// 初始化参数
		BGMName = "BGM1";
	
		
		// Debug.Log("游戏初始化完成");
	}
}
