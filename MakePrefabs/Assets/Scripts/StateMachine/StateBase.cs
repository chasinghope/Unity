using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : IStateMachine
{
    public StateEnterFun EnterFun;
    public StateExitFun ExitFun;
    public StateUpdateFun UpdateFun;

    protected  StateProcess myStateProcess = StateProcess.None;
    public virtual StateProcess MyStateProcess 
    {
        get
        {
            return myStateProcess;
        }
        set
        {

        }
    }

    /// <summary>
    /// 该状态绑定的功能， 建议使用 lambda表达式， 否则一个状态要使用三个函数，状态太多会导致使用的函数过多
    /// </summary>
    /// <param name="enterFun"></param>
    /// <param name="updateFun"></param>
    /// <param name="exitFun"></param>
    public StateBase(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun)
    {
        this.EnterFun = enterFun;
        this.UpdateFun = updateFun;
        this.ExitFun = exitFun;
    }

    public virtual void StateEnter()
    {
        if (EnterFun != null)
            EnterFun();
    }

    public virtual void StateExit()
    {
        if (ExitFun != null)
            ExitFun();
    }

    public virtual void StateUpdate()
    {
        if (UpdateFun != null)
            UpdateFun();
    }
}
