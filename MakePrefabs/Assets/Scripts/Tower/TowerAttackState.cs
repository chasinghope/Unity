using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : StateBase
{
	public TowerAttackState(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun) : base(enterFun, updateFun,  exitFun)
	{
		myStateProcess = StateProcess.TowerAttack;
	}
}
