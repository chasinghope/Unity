using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuilder:MonoBehaviour
{
    #region Init parameter
    [Header("必需名字-->事件中心")]
    public string Name;

    // protected string Name { get; set; }
    protected int HP { get; set; }
    protected int DEF { get; set; }
    protected float BT { get; set; }
    protected EnemyTeam TeamType { get; set; }
    protected bool IsAuto { get; set; }
    public Transform BornPoint;

    // protected int selfLayer;
    protected string selfTag = "Builder";
    #endregion

    //    [SerializeField]
    //    protected List<GameObject> prefabList;
    private string unitPath = "Enemy/";
    private int amount = 0;

    protected Slider HPSlider;


    #region 服务上层
    protected void  MakeUnit(string enemyName, Transform parent, Vector3 bornPoint, EnemyTeam teamType)
    {
        amount++;
        ChunkAllocator.Instace.GetPrefab(unitPath +enemyName, (obj) =>
        {
           
            if(teamType == EnemyTeam.friend)
            {
                obj.name = enemyName.Replace("Enemy", "Soilder");
            }
            obj.transform.position = bornPoint;
            obj.GetComponent<SpriteRenderer>().sortingOrder = amount;
            // 获取对象池中的控制组件
            BaseEnemy tmpBaseEnemy = null;
            switch (enemyName)
            {
                case "Enemy1":
                    tmpBaseEnemy = obj.GetComponent<Enemy1>();
                    break;
                case "Enemy2":
                    tmpBaseEnemy = obj.GetComponent<Enemy2>();
                    break;
                case "Enemy3":
                    tmpBaseEnemy = obj.GetComponent<Enemy3>();
                    break;
            }

            

            // tmpBaseEnemy.I
            tmpBaseEnemy.Init();
            tmpBaseEnemy.Team = teamType;

        } );

    }

    private float rebuildTime = 0;
    /// <summary>
    /// 计时器    参数BT
    /// </summary>
    /// <returns></returns>
    protected bool CalcTime()
    {
        if (rebuildTime >= BT)
        {
            rebuildTime = 0;
            return true;
        }
        rebuildTime += Time.deltaTime;
        return false;
    }

    protected void CheckStatus()
    {

    }

    public void Do_Hurt(int damage)    
    {
        if (damage > DEF)
        {
            damage = damage - DEF;
            HP -= damage;
        }

        if (HP <= 0)
        {
            EventManager.Instace.EventTrigger<string>("BuildDead",Name);
            Destroy(gameObject);
            return;
        }

        HPSlider.value = HP;

    }


    #endregion

    #region CheckStatus
    // private void CheckStatus()
    // {
    //     if( HP<= 0)
    //     {
    //         HPSlider.value = 0;
    //     }
    // 
    //     Destroy(gameObject);
    // }

    #endregion


    #region Unity Mono
    protected virtual void Awake()
    {
        // unitScale = new Vector3(0.5f, 0.5f, 0.5f);
        HPSlider = gameObject.GetComponentInChildren<Slider>();
    }

    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        CalcTime();                        // 计算计时器时间
    }
    #endregion



}
