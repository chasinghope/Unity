using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region Tower属性

    protected string Name;
    protected string weaponName;
    protected float APSD;
    protected float Radius;
    protected int Model = 0;              // model 0  -->   单体攻击             model  1  --> 群体攻击

    public Transform BornPoint;


    protected string selfTag = "Tower";
    protected string selfSortingLayer = "Tower";

    // 金币系统
    protected int OutBill;
    protected int InBill;
    #endregion

    #region 私有成员变量   字段
    /// <summary>
    ///  状态机控制器
    /// </summary>
    private StateHandler fsmTowerController;


    private CircleCollider2D cirColl;

    private List<GameObject> allEnemy= new List<GameObject>();

    private string weaponPath = "Weapon/";              // Weapon Prefab 在Resources 文件夹下的存放目录
    #endregion



    #region Unity Mono


    protected virtual void Awake()
    {

        cirColl = GetComponent<CircleCollider2D>();

        StateBase wait = new TowerWaitState(
            ()=>{ Debug.Log("已进入警戒状态");  },
            ()=>{

                if (IsHaveEnemy())
                    fsmTowerController.EnterState(StateProcess.TowerAttack);          
            },
            ()=>{            } );


        StateBase attack = new TowerAttackState(
            () => { Debug.Log("已进入攻击状态");
                if( Model == 0)
                {
                    StartCoroutine(StartLaunch());
                }
                if (Model == 1)
                {
                    StartCoroutine(StartLaunchAll());
                }

            },
            () => {
                if ( !IsHaveEnemy())
                    fsmTowerController.EnterState(StateProcess.TowerWait);
            },
            () => {
                if (Model == 0)
                {
                    StopCoroutine(StartLaunch());
                }
                if (Model == 1)
                {
                    StopCoroutine(StartLaunchAll());
                }

            }
            );

        fsmTowerController = new StateHandler(attack);
        fsmTowerController.AddState(wait);

    }


    protected virtual void Start()
    {
        // 设置标签 及 图层
        gameObject.tag = selfTag;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = selfSortingLayer;

        // 设置防御塔范围
        cirColl.radius = Radius;

        // 建立后首先进入警戒状态


        fsmTowerController.EnterState(StateProcess.TowerWait);

    }

    protected virtual void Update()
    {
        // 更新状态
        fsmTowerController.StateMachineUpdate();

        ClearDeadEnemy();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // 将进入攻击范围的敌人加入攻击列表
        if (other.gameObject.tag == "Enemy")
        {
            allEnemy.Add(other.gameObject);
            //   Debug.Log(allEnemy.Count);
        }
              
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 将逃离攻击范围的敌人从列表中删除
        if( other.gameObject.tag == "Enemy" && allEnemy.Contains(other.gameObject))
        {
            allEnemy.Remove(other.gameObject);
            //   Debug.Log(allEnemy.Count);
        }
            
    }

    #endregion


    #region 私有方法

    private void ClearDeadEnemy()
    {
        //TODO 删除在敌人里面的空指针
        List<int> tmpArray = new List<int>();
        if (allEnemy.Count > 0)
        {
            // 寻找死了的敌人
            for (int i = 0; i < allEnemy.Count; i++)
            {
                if(!allEnemy[i].GetComponent<BaseEnemy>().IsAlive)
                {
                    tmpArray.Add(i);
                }

            }

            // 有死了的敌人就进行删除
            if( tmpArray.Count > 0)
            {
                for (int i = tmpArray.Count-1; i >= 0; i--)
                {
                    if (allEnemy[i] )
                    {
                        allEnemy.RemoveAt(i);
                    }
                }
            }
        }

        // 清空
        tmpArray.Clear();
    }

    #region MODEL 0 单体攻击



    /// <summary>
    /// 从敌人列表中获取敌人
    /// </summary>
    /// <returns></returns>
    private GameObject GetAttackTarget()
    {
        GameObject tmpEnemy = null;

        if (allEnemy.Count > 0)
        {
            // 寻找敌人列表中或者的敌人
            foreach (var item in allEnemy)
            {
                if( item.GetComponent<BaseEnemy>().IsAlive)
                {
                    tmpEnemy = item;
                    break;
                }
            }

        }
        return tmpEnemy;
    }






    /// <summary>
    /// 是否有敌人进入射程
    /// </summary>
    /// <returns></returns>
    private bool IsHaveEnemy()
    {
        if (allEnemy.Count > 0)
            return true;
        return false;
    }


    /// <summary>
    /// 发射武器  从对象池中获取武器
    /// </summary> 
    private void LaunchWeapon(Vector3 bornPoint)
    {
        ChunkAllocator.Instace.GetPrefab(weaponPath + weaponName,
            (obj) =>
            {
                obj.transform.localPosition = bornPoint;
                obj.GetComponent<Weapon>().Inital(GetAttackTarget());
            }

            );
    }


    /// <summary>
    /// 每个一段时间，发射武器
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartLaunch()
    {
        while (true)
        {
            yield return new WaitForSeconds(APSD);
            // Debug.Log("执行一次攻击");
            LaunchWeapon(BornPoint.position);
        }
    }

    #endregion



    #region MODEL 1 群体攻击
    /// <summary>
    /// 获取所有进入射程的所有的敌人
    /// </summary>
    /// <returns> 敌人列表</returns>
    private List<GameObject> GetAllAttackTarget()
    {
        List<GameObject> objList = new List<GameObject>();
        // List<int> clearDeadList = new List<int>();
        for (int i = 0; i < allEnemy.Count; i++)
        {
            if (allEnemy[i].GetComponent<BaseEnemy>().IsAlive)
            {
                objList.Add(allEnemy[i]);
            }
        }

        return objList;
    }


    /// <summary>
    /// 对allEnemy列表中所有存活的敌人发动攻击
    /// </summary>
    /// <param name="bornPoint"></param>
    /// <param name="objList"></param>
    private void LaunchAllWeapon(Vector3 bornPoint, List<GameObject> objList)
    {
        if( objList.Count > 0)
        {
            // Debug.Log("objList.Count : " + objList.Count);
            foreach (GameObject item in objList)
            {
                ChunkAllocator.Instace.GetPrefab(weaponPath + weaponName,
                    (obj) =>
                    {
                        obj.transform.localPosition = bornPoint;

                        // TODO   this error
                        obj.GetComponent<Weapon>().Inital(item);
                    }

                    );
            }
        }

    }


    private IEnumerator StartLaunchAll()
    {
        while (true)
        {
            yield return new WaitForSeconds(APSD);
            // Debug.Log("执行一次攻击");
            LaunchAllWeapon(BornPoint.position, GetAllAttackTarget());
        }
    }
    #endregion





    #endregion



}
