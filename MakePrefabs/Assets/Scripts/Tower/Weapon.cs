using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region 武器属性
    protected virtual int ATK { get; set; }
    protected virtual string Name { get; set; }
    protected virtual float Speed { get; set; }

    private string selfTag = "Weapon";
    private string soringLayerName = "Weapon";
    private string weaponPath = "Weapon/";
    #endregion


    private GameObject attackTarget = null;                    // attackTarget 是从Tower 的allEnemy列表中获取
    private Transform targetTrans;
    private BaseEnemy targetBaseEnemy;



    public void Inital(GameObject obj)
    {
        if( obj != null)
        {
            attackTarget = obj;
            targetTrans = attackTarget.GetComponent<Transform>();
            targetBaseEnemy = attackTarget.GetComponent<BaseEnemy>();
        }
        else
        {
            Debug.Log("初始化武器未获得攻击对象");
         
        }
    }


    #region Unity Mono
    private void Awake()
    {
        gameObject.tag = selfTag;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = soringLayerName;
    }



    private void Update()
    {
        if(attackTarget != null)
        {
            if (!targetBaseEnemy.IsAlive)
            {
                // TODO回收对象
                ChunkAllocator.Instace.PushPrefab(weaponPath + Name, gameObject);
            }
            else
            {
                Vector3 p = targetTrans.position - transform.position;
                p = p.normalized;
                float angle = Mathf.Atan2(p.y, p.x) * Mathf.Rad2Deg;
                transform.position += p * Speed * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject == attackTarget && other.gameObject.tag == "Enemy")
        {
            // 对攻击对象造成伤害
            MakeDamage();

        }
    }

    #endregion

    #region 私有方法
    /// <summary>
    /// 对目标造成伤害
    /// </summary>
    private void MakeDamage()
    {
        if (attackTarget != null)
        {
            // TODO 该处 考虑封装BaseEnemy 受伤方法
            if (targetBaseEnemy.IsAlive)
            {
                targetBaseEnemy.fsmController.EnterState(StateProcess.Hurt);
                targetBaseEnemy.Do_Hurt(ATK);
                // 攻击完成后 回收武器至资源池
                ChunkAllocator.Instace.PushPrefab(weaponPath + Name, gameObject);
            }
        }
    }

    #endregion




}

