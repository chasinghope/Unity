public interface IStateMachine
{
    void StateEnter();
    void StateUpdate();
    void StateExit();
}

public delegate void StateEnterFun();
public delegate void StateUpdateFun();
public delegate void StateExitFun();

public enum StateProcess
{
    // 敌人状态
    None,
    Walk,
    Hurt,
    Attack,


    // 塔楼状态
    TowerWait,
    TowerAttack

    //TODO  毋庸置疑，这种设计方式有问题
}