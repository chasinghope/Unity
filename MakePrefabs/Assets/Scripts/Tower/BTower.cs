using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTower : Tower
{
    public void Inital()
    {
        Name = "ArcherTower ";

        // TODO 从对象池获取weapon
        weaponName = "Arrow";
        APSD = 3f;
        Radius = 3f;
        Model = 1;
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
