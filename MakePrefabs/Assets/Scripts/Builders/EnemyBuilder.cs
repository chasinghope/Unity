using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilder : BaseBuilder
{
	private List<string> enemyNameList;
	private int BuildCount = 0;

	[SerializeField]
	private bool isProducing = false;

	#region Init the type Builder
	private void Initial()
	{
		// 初始化参数
		// Name = "EnemyBuilder";
		HP = 1000;
		DEF = 20;
		BT = 7f;
		TeamType = EnemyTeam.enemy;
		gameObject.layer = (int)TeamType;
		gameObject.tag = selfTag;

	     
		IsAuto = true;

		// 属性应用
		HPSlider.maxValue = HP;
		HPSlider.value = HP;

		enemyNameList = new List<string>() { "Enemy1", "Enemy2", "Enemy3" };
	}

	#endregion

	

	#region Unity Mono

	protected override void Start()
	{
		base.Start();
		Initial();
		DealEvent();


		//TODO
		// InvokeRepeating("ToBuild", 0f, BT) ;
		StartCoroutine( ToBuild() ); 
	}
	protected override void Update()
	{
		base.Update();

	}

	#endregion


	#region TestArea

	public void Test()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			MakeUnit("Enemy1", transform, BornPoint.position, TeamType);
		}

		if (Input.GetKeyDown(KeyCode.O))
		{
			MakeUnit("Enemy2", transform, BornPoint.position, TeamType);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			MakeUnit("Enemy3", transform, BornPoint.position, TeamType);
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			MakeUnit("Enemy1", transform, BornPoint.position, EnemyTeam.friend);
		}
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




	private IEnumerator ToBuild()
	{
		while (true)
		{
			yield return new WaitForSeconds(BT);
			if (isProducing)
			{
				ProduceUnit(GetEnemyType());
			}
			//   ProduceUnit(GetEnemyType());
		}		
	}

	private void DealEvent()
	{
		EventManager.Instace.AddEventListener<string>("BuildAlive", (tmpName)=> {
			// Debug.Log(Name);
			// Debug.Log(tmpName);
			if (WhetherMatch(tmpName))
			{
				Debug.Log("serive build alive event");
				isProducing = true;
			}
				
		});


		EventManager.Instace.AddEventListener<string>("BuildDead", (tmpName) =>
		{
			if (WhetherMatch(tmpName))
				
				isProducing = false;
		});

		EventManager.Instace.AddEventListener<int>("TimeEvent", (min) =>
		{
			if (min == 5)
				BT -= 1f;
			if (min == 10)
				BT -= 1f;
			if (min == 15)
				BT -= 1f;

		});
	}


	/// <summary>
	/// 检测敌我城堡是否匹配
	/// </summary>
	/// <param name="CastleName"></param>
	/// <returns></returns>
	private bool WhetherMatch(string CastleName)
	{
		bool isMatch = false;
		//  Debug.Log("敌人建筑物名称"+ Name);
		//  Debug.Log("存活建筑事件触发者" + CastleName);
		switch (CastleName)
		{
			case "Castle1":
				
				if (Name == "EnemyBuilder1")
					isMatch = true;
				break;

			case "Castle2":
				if (Name == "EnemyBuilder2")
					isMatch = true;
				break;

			case "Castle3":
				if (Name == "EnemyBuilder3")
					isMatch = true;
				break;

			case "Castle4":
				if (Name == "EnemyBuilder4")
					isMatch = true;
				break;

			case "Castle5":
				if (Name == "EnemyBuilder5")
					isMatch = true;
				break;

			default: Debug.Log("no case match.");
				break;
		}
		//  Debug.Log(isMatch + ":  isMatch");
		return isMatch;
	}
	#endregion




}
