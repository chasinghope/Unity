using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : StateBase
{
	public EnemyAttackState(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun) : base(enterFun, updateFun, exitFun)
	{
		myStateProcess = StateProcess.Attack;
	}

}
