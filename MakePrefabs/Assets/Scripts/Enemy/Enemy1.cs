using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{


    private InputHandler handler;

    public override void Init()
    {
        // 测试用 设置 阵营
        // Team = EnemyTeam.enemy;
        // 初始化敌人属性
        enemyType = "Enemy1";
        Hp = 350;
        ATK = 55;
        DEF = 10;
        ATKdistance = 0.7f;
        APSD = 2.5f;
        speed = 0.6f;
        // 敌人属性应用
        HPSlider.maxValue = Hp;
        HPSlider.value = Hp;
        IsAlive = true;
        // 初始化手柄
        //   AutoMode = true;
        //   if(!AutoMode)
        //       handler = new InputHandler(Enemy1Controller);
    }




    #region Mono
    protected override void Start()
    {
        base.Start();
        Init();
    }
    protected override void Update()
    {
        base.Update();

        AutoController();


    }
    #endregion


    /// <summary>
    /// 玩家操控敌人
    /// </summary>
    private void Enemy1Controller(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.J:                                           // 向
                Debug.Log("按下J键");
                fsmController.EnterState(StateProcess.Attack);
                break;
            case KeyCode.A:                                           // 向左

                fsmController.EnterState(StateProcess.Walk);
                GoLeft();

                break;
            case KeyCode.S:                                          // 向下

                break;
            case KeyCode.D:                                         // 向右

                fsmController.EnterState(StateProcess.Walk);
                GoRight();

                break;
            case KeyCode.Space:                                // 跳跃 

                break;
        }
    }


    /// <summary>
    /// 敌人自动动作
    /// </summary>
    private void AutoController()
    {
        if (IsAttack)
        {
            fsmController.EnterState(StateProcess.Attack);
        }
    }

}
