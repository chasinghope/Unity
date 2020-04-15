using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyTeam
{
	friend = 11,                             // 数字表示了 Team 所在 layer
	enemy = 9,
	block = 12
}

public struct EnemyInfo
{
	public string enemyType;
	public EnemyTeam enemyTeam;
}

public class BaseEnemy : MonoBehaviour
{


	#region UI
	protected Slider HPSlider;
	#endregion
	
	#region 组件
	private Rigidbody2D rb;
	private Transform tran;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	// private AudioSource audioSource;

	#endregion

	#region 敌人属性
	///    [SerializeField]
	///    private bool autoMode;
	///    protected bool AutoMode
	///    {
	///    	get { return autoMode; }
	///    	set { autoMode = value; }
	///    }
	private EnemyInfo enemyInfo;

	private EnemyTeam team;
	public EnemyTeam Team 
	{
		get
		{
			return team;
		}

		set
		{
			// 设置敌人所处阵营后， 自动更新其图层，标签和SortingLayer
			team = value;

			// 设置enemyInfo 信息
			enemyInfo.enemyTeam = team;
			enemyInfo.enemyType = enemyType;


			if (team == EnemyTeam.enemy)
			{
				gameObject.tag = "Enemy";
				enemyTag = "Friend";

				selfLayer = (int)EnemyTeam.enemy;
				enemyLayer = "Friend";
				blockLayer = "Block";

				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
				fsmController.EnterState(StateProcess.Walk);
				GoRight();
			}
			if (team == EnemyTeam.friend)
			{
				gameObject.tag = "Friend";
				enemyTag = "Enemy";

				selfLayer = (int)EnemyTeam.friend;
				enemyLayer = "Enemy";
				blockLayer = "Block";

				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Friend";
				fsmController.EnterState(StateProcess.Walk);
				GoLeft();
			}

			gameObject.layer = selfLayer;
		}
	}

	protected string enemyType;           // 敌人类型   Enemy1   Enemy2   Enemy3
	protected string enemyTag;
	protected string enemyLayer;           // 敌人所在Layer
	protected string blockLayer;             // 石头所在图层
	protected int selfLayer;                 // 自己所在 Layer


	protected int Hp;                             // 生命值
	protected int ATK;                           // 攻击力
	protected int DEF;                           //  防御力
	protected float ATKdistance;             //  攻击距离
	protected float APSD;                     // 攻速              本质是计时器
	protected float speed;        //  角色速度

	public bool IsAlive { get; set; }
	#endregion

	public virtual void Init() { }

	#region 状态机

	public  StateHandler fsmController;

	#endregion

	#region 划分阵营

	
	/// <summary>
	/// 默认为敌人阵营
	/// </summary>
	/// <param name="tmpTeam"></param>
	public void BelongToWhichTeam(EnemyTeam tmpTeam = EnemyTeam.enemy)
	{
		team = tmpTeam;
		// Debug.Log(team);
		if( team == EnemyTeam.enemy)                                   
		{
			gameObject.tag = "Enemy";
			enemyTag = "Friend";

			selfLayer = (int)EnemyTeam.enemy;
			enemyLayer = "Friend";

			gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
		}
		if( team == EnemyTeam.friend)
		{
			gameObject.tag = "Friend";
			enemyTag = "Enemy";

			selfLayer = (int)EnemyTeam.friend;
			enemyLayer = "Enemy";

			gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Friend";
		}
		// Debug.Log(enemyLayer);
		// gameObject.layer = selfLayer;
		gameObject.layer = selfLayer;


	}

	#endregion

	#region 移动接口

	
	public bool forwardLeft;       //   向左
	public bool forwardRight;     //   向右


	private void Movement()
	{
		if(forwardLeft)
		{
			tran.localPosition += Vector3.left * speed * Time.deltaTime;
			spriteRenderer.flipX = true;
			SetAnim_Walking();
		}

		if (forwardRight)
		{
			tran.localPosition += Vector3.right * speed * Time.deltaTime;
			spriteRenderer.flipX = false;
			SetAnim_Walking();
		}
	}

	public void GoLeft()
	{
		forwardLeft = true;
		forwardRight = false;

	}

	public void GoRight()
	{
		forwardLeft = false;
		forwardRight = true;
	}

	public void StopMove()
	{
		forwardLeft = false;
		forwardRight = false;
	}

	#endregion

	#region 动画接口
	private void SetAnim_Idle()
	{
		anim.SetFloat("anim_Speed", 0);
	}

	private void SetAnim_Walking()
	{
		anim.SetFloat("anim_Speed", 1f);
	}

	private void SetAnim_Attacking()
	{
		anim.SetBool("anim_Attacking", true);
	}

	public void Making_Attackting()
	{
		if(isAttack == false)
		{
			return;
		}

		if( hitInfoList.Length > 0)
		{
			for (int i = 0; i < hitInfoList.Length; i++)
			{
				if (hitInfoList[i].collider.tag == "Builder")
				{
					attackBuilderTarget = hitInfoList[i].collider.gameObject.GetComponent<BaseBuilder>();
					attackBuilderTarget.Do_Hurt(ATK);
				}

				if (hitInfoList[i].collider.tag == enemyTag)
				{
					attackEnemyTarget = hitInfoList[i].collider.gameObject.GetComponent<BaseEnemy>();
					attackEnemyTarget.fsmController.EnterState(StateProcess.Hurt);
					if (attackEnemyTarget.IsAlive)
						attackEnemyTarget.Do_Hurt(ATK);
				}
			}
		}
		
		
		if( hitInfo.collider != null)
		{
			if (hitInfo.collider.tag == "Block")
			{
				attackBlockTarget = hitInfo.collider.gameObject.GetComponent<BaseBlock>();
				if (attackBlockTarget.IsAlive)
					attackBlockTarget.Do_Hurt(ATK);
			}
		}


	}

	// 用在 Attacking 动画的最后一帧中
	public void CancelAnim_Attacking()
	{
		anim.SetBool("anim_Attacking", false);
	}

	private void SetAnim_Hurt()
	{
		anim.SetBool("anim_Hurt", true);
	}

	// 用在 Hurt 动画的最后一帧中
	public void CancelAnim_Hurt()
	{
		anim.SetBool("anim_Hurt", false);

		//TODO
		fsmController.EnterState(StateProcess.Walk);
	}

	private void SetAnim_Dying()
	{
		anim.SetTrigger("anim_Dying");
	}

	// 用在 Dying 动画的最后一帧中
	private void FinishAnim_Dying()
	{
		//TODO send error info
		EventManager.Instace.EventTrigger<EnemyInfo>("enemyDead", enemyInfo);

		IsAlive = false;
		ChunkAllocator.Instace.PushPrefab(this.gameObject.name, this.gameObject);

	}
	#endregion

	#region 音效接口


	private string enemySoundPath = "Enemy/";

	private void Play_hitClip()
	{
		MusicMgr.Instace.PlaySound(enemySoundPath + "hit");
	}

	public void Play_deathClip()
	{
		MusicMgr.Instace.PlaySound(enemySoundPath + "death");
	}


	#endregion

	#region 状态接口
	private bool isAttack;
	public bool IsAttack
	{
		get
		{
			return isAttack;
		}
		set
		{
			isAttack = value;
		}
	}

	private bool lockCheckAttack = false;


	private RaycastHit2D[] hitInfoList;
	private RaycastHit2D hitInfo;                   // 检测是否碰撞到石头
	// private RaycastHit2D hitInfo;
	/// <summary>
	/// 状态检测入口方法
	/// </summary>
	protected void StatusCheck()
	{
		if( !lockCheckAttack)
	         CheckAttack();

	}


	/// <summary>
	/// 攻击时应停止检测
	/// </summary>
	private void CheckAttack()
	{
		Vector3 direction;
		if( spriteRenderer.flipX )        // 敌人面向左边
		{
			direction = Vector3.left;
		}
		else
		{
			direction = Vector3.right;
		}
		// 获取攻击对象列表
		// hitInfoList = Physics2D.RaycastAll(transform.position, direction, ATKdistance, LayerMask.GetMask(enemyLayer));
		hitInfoList = Physics2D.RaycastAll(transform.position, direction, ATKdistance, LayerMask.GetMask(enemyLayer));
		hitInfo = Physics2D.Raycast(transform.position, direction, ATKdistance, LayerMask.GetMask(blockLayer));
		if ( hitInfoList.Length > 0 || hitInfo.collider!=null)
		{
			isAttack = true;		
			// Debug.DrawLine(transform.position, hitInfoList[0].point, Color.red);
		}
		else
		{
			isAttack = false;
			// Debug.DrawLine(transform.position, transform.position + direction * ATKdistance, Color.green);
		}


	}


	#endregion

	#region 战斗接口
	private BaseEnemy attackEnemyTarget;                                   // 攻击对象们         //  事件的拥有者
	private BaseBuilder attackBuilderTarget;
	private BaseBlock attackBlockTarget;

	protected void Do_Attack()                                                    // 触发事件的方法
	{
		//  attackTarget.Do_Hurt(ATK);
		SetAnim_Attacking();
	}                             

	public void Do_Hurt(int damage)             // 被攻击事件  回调方法原型
	{
		if (damage > DEF)
		{
			damage -= DEF;
			Hp -= damage;
		}

		if (Hp <= 0)
		{
			Do_Dying();
			return;
		}

		HPSlider.value = Hp;
	}                          
	
	

	protected void Do_Dying()
	{
		IsAlive = false;
		HPSlider.value = 0;
		
		Play_deathClip();
		SetAnim_Dying();
	}
	#endregion







	private float ctime = 0;


	#region UnityMono

	protected virtual void Awake()
	{
		// 获取自身组件
		rb = GetComponent<Rigidbody2D>();
		tran = GetComponent<Transform>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		HPSlider = gameObject.GetComponentInChildren<Slider>();

		// 状态机初始化
		StateBase walk = new EnemyWalkState(
			() => {
				if (Team == EnemyTeam.friend)
					GoLeft();
				if (Team == EnemyTeam.enemy)
					GoRight();
			},

			() => {
				Movement();                          // 移动
			},
			() => {
				StopMove();
			}
			);

		StateBase attack = new EnemyAttackState(
		    () => {
				// Debug.Log("Doing attack");
				lockCheckAttack = true;

				// fsmController.EnterState(StateProcess.Walk);
			},
			() => {
			     if (!IsAttack)
			     {
			     	fsmController.EnterState(StateProcess.Walk);
			     }
			     ctime += Time.deltaTime;
			     if (ctime > APSD)
			     {
			     	ctime = 0;
			     	Do_Attack();

					lockCheckAttack = false;
			     }


			},

			() => { }

			);

		StateBase hurt = new EnemyHurtState(
			() => {

				Play_hitClip();
				SetAnim_Hurt();

			},
			() => { },
			() => { }

			);

		fsmController = new StateHandler(walk);
		fsmController.AddState(attack);
		fsmController.AddState(hurt);


	}

	protected virtual void Start()
	{
		// BelongToWhichTeam();
		Physics2D.queriesStartInColliders = false;
		Physics2D.queriesHitTriggers = true;
	}

	protected  virtual void FixedUpdate()
	{
		StatusCheck();
	}


	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
	     if(other.gameObject.tag == "RevertEnemy")
		{
			IsAlive = false;
			ChunkAllocator.Instace.PushPrefab(this.gameObject.name, this.gameObject);
		}


	}


	protected virtual void Update()
	{
		fsmController.StateMachineUpdate();

	}

	#endregion



}

