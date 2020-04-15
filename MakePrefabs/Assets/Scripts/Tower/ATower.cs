using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATower : Tower
{
    public void Inital()
    {
        Name = "CastleTower ";

        // TODO 从对象池获取weapon
        weaponName = "Fireball";
        APSD = 4f;
        Radius = 5f;
        Model = 0;
    }


    #region Unity Mono

    protected override void Awake()
    {
        base.Awake();
    }



    protected override void Start()
    {
        Inital();
        base.Start();

    }


    protected override void Update()
    {
        base.Update();
    }
    #endregion

}
