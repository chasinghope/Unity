using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileGrass : BaseTile
{
    protected override void Start()
    {
        base.Start();
        TileType = "Grass";
    }
}
