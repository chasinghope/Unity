using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler
{
    private StateBase nowState;
    private List<StateBase> allState;

    public StateHandler(StateBase startState)
    {
        allState = new List<StateBase>();
        nowState = startState;
        AddState(startState);
        
    }


    /// <summary>
    /// 进入某个状态
    /// </summary>
    /// <param name="process">StateProcess 枚举类型，要进入的状态</param>
    public void EnterState(StateProcess process)
    {
        if (process == nowState.MyStateProcess)
        {
            return;
        }

        StateBase state = SearchState(process);
        if( state != null)
        {
            if (nowState != null)
            {
                nowState.StateExit();
            }

            nowState = state;
            nowState.StateEnter();
        }

    }


    /// <summary>
    /// 状态机更新函数     StateHandler controller = new StateHandler(xx);    
    /// 注意在 MonoBehaviour Update()函数中 调用  -->  StateMachineUpdate();
    /// </summary>
    public void StateMachineUpdate()
    {
        if( nowState != null)
            nowState.StateUpdate();
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state"></param>
    public void AddState(StateBase state)
    {
        if(state!=null && !allState.Contains(state))
        {
            allState.Add(state);
        }
    }

    /// <summary>
    /// 寻找状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private StateBase SearchState(StateProcess state)
    {
        foreach (var item in allState)
        {
            if(item.MyStateProcess == state)
            {
                return item;
            }
        }
        return null;
    }
}
