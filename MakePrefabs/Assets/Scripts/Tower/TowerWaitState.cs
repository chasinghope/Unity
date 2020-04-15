using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWaitState : StateBase
{
	public TowerWaitState(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun) : base(enterFun, updateFun, exitFun)
	{
		myStateProcess = StateProcess.TowerWait;
	}
}
