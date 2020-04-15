using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : StateBase
{
	public EnemyHurtState(StateEnterFun enterFun, StateUpdateFun updateFun, StateExitFun exitFun) : base(enterFun, updateFun, exitFun)
	{
		myStateProcess = StateProcess.Hurt;
	}
}
