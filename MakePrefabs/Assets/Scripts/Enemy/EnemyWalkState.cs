using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : StateBase
{
	public EnemyWalkState(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun) : base(enterFun, updateFun, exitFun)
	{
		myStateProcess = StateProcess.Walk;
	}

}
