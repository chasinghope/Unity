using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBuilder : BaseBuilder
{

	private List<string> enemyNameList;
	private int BuildCount = 0;

	private List<string> buildList;
	private int enemy1Num => GetEnemyNum("Enemy1");
	private int enemy2Num => GetEnemyNum("Enemy2");
	private int enemy3Num => GetEnemyNum("Enemy3");


	// private bool controlLock = false;
	#region Init the type Builder
	private void Initial()
	{
		// 初始化参数
		// Name = "SoilderBuilder";
		HP = 1200;
		DEF = 20;
		BT = 5f;
		TeamType = EnemyTeam.friend;
		gameObject.layer = (int)TeamType;
		gameObject.tag = selfTag;


		IsAuto = true;

		// 属性应用
		HPSlider.maxValue = HP;
		HPSlider.value = HP;

		enemyNameList = new List<string>() { "Enemy1", "Enemy2", "Enemy3" };
		buildList = new List<string>();

		DealEvent();
	}

	#endregion



	#region Unity Mono

	protected override void Start()
	{
		base.Start();
		Initial();

		StartCoroutine(WaitBT());

		// InvokeRepeating("ToBuild", 0f, BT);
	}
	protected override void Update()
	{
		base.Update();
		// Test();
		//InvokeRepeating("");
	}

	#endregion




	#region 私有方法
	private void ProduceUnit(string enemyName)
	{
		MakeUnit(enemyName, transform, BornPoint.position, TeamType);
	}

	private string GetEnemyType()
	{
		BuildCount++;
		if (BuildCount < 4)
		{
			return enemyNameList[0];
		}
		int rand = (int)Random.Range(0f, 2.9f);

		return enemyNameList[rand];
	}


	/// <summary>
	/// 获取buildList内的敌人名字对应的数量
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	private int GetEnemyNum(string name)
	{
		int num = 0;
		if (buildList.Count == 0)
			return num;
		for (int i = 0; i < buildList.Count; i++)
		{
			if (buildList[i] == name)
				num++;
		}
		return num;
	}

	/// <summary>
	/// 自动模式
	/// </summary>
	private void ToBuild()
	{
		ProduceUnit(GetEnemyType());
	}

	private int ReturnEnemyNum(string name)
	{
		int amout = 0;
		switch (name)
		{
			case "Enemy1":
				amout = enemy1Num;
				break;
			case "Enemy2":
				amout = enemy2Num;
				break;
			case "Enemy3":
				amout = enemy3Num;
				break;
		}
		return amout;
	}

	/// <summary>
	/// 进生产池    从后面进
	/// </summary>
	/// <param name="name">要进的对象名</param>
	private void InBuildList(string name)
	{
		buildList.Add(name);
		int amout = ReturnEnemyNum(name);
		// 触发UI 界面增加该士兵数量
		EventManager.Instace.EventTrigger<int>(Name + "_" + name + "_Num", amout);
	}

	/// <summary>
	/// 出生产池   从后面出
	/// </summary>
	/// <param name="name">要进的对象名</param>
	private void OutBuildList(string name)
	{
		int tmp = 0;
		if (buildList.Count == 0)
			return;
		for (int i = buildList.Count - 1; i >= 0; i--)
		{
			if (buildList[i] == name)
				tmp = i;
		}
		buildList.RemoveAt(tmp);

		int amout = ReturnEnemyNum(name);
		// 触发UI 界面减少该士兵数量
		EventManager.Instace.EventTrigger<int>(Name + "_" + name + "_Num", amout);
	}


	/// <summary>
	/// 生产士兵，从第一个生产
	/// </summary>
	private void ProduceSoilder()
	{
		string target = null;
		if (buildList.Count == 0)
		{
			return;
		}
		target = buildList[0];
		buildList.RemoveAt(0);
		if (target == null)
		{
			return;
		}
		ProduceUnit(target);

		// 触发UI 界面减少该士兵数量
		int amout = ReturnEnemyNum(target);
		EventManager.Instace.EventTrigger<int>(Name + "_" + target + "_Num", amout);
	}


	/// <summary>
	/// 开启协程， 每个BT时间生产一个 士兵
	/// </summary>
	/// <returns></returns>
	IEnumerator WaitBT()
	{
		while (true)
		{
			yield return new WaitForSeconds(BT);
			ProduceSoilder();
		}
	}

	private void DealEvent()
	{
		// 绑点与UI界面交互的 增加士兵事件
		EventManager.Instace.AddEventListener(Name + "_Enemy1_Add", () => { InBuildList("Enemy1"); });
		EventManager.Instace.AddEventListener(Name + "_Enemy2_Add", () => { InBuildList("Enemy2"); });
		EventManager.Instace.AddEventListener(Name + "_Enemy3_Add", () => { InBuildList("Enemy3"); });

		// 绑点与UI界面交互的 移除士兵事件
		EventManager.Instace.AddEventListener(Name + "_Enemy1_Remove", () => { OutBuildList("Enemy1"); });
		EventManager.Instace.AddEventListener(Name + "_Enemy2_Remove", () => { OutBuildList("Enemy2"); });
		EventManager.Instace.AddEventListener(Name + "_Enemy3_Remove", () => { OutBuildList("Enemy3"); });


		// 触发事件城堡激活 通知对面敌人建筑物出兵
		EventManager.Instace.EventTrigger<string>("BuildAlive", Name);

	}


	#endregion
}
