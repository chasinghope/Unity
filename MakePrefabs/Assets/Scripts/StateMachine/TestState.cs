using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : MonoBehaviour
{
    StateBase walk;
    StateBase attack;
    StateHandler fsmController;

    InputHandler handler;
    private void Awake()
    {
        handler = new InputHandler(WASD);
        walk = new EnemyWalkState(
             () => { Debug.Log("进入行走状态"); },
            () => { Debug.Log("正在行走状态"); },
            () => { Debug.Log("退出行走状态"); }
        );

        attack = new EnemyAttackState(
             () => { Debug.Log("进入攻击状态"); },
            () => { Debug.Log("正在攻击状态"); },
            () => { Debug.Log("退出攻击状态"); }
        );

        fsmController = new StateHandler(walk);
        fsmController.AddState(attack);


    }

    // TODO
    private void Update()
    {
        fsmController.StateMachineUpdate();
    }


    private void WASD(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.W:                    
                fsmController.EnterState(StateProcess.Attack);                       // 向上
                break;
            case KeyCode.A:
                fsmController.EnterState(StateProcess.Walk);                     // 向左
                break;
            case KeyCode.S:                                          // 向下
                break;
            case KeyCode.D:                                         // 向右
                break;
            case KeyCode.Space:                                // 跳跃 
                break;
        }
    }

}
