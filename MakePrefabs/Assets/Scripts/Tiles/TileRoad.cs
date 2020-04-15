using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoad : BaseTile
{
    protected override void Start()
    {
        base.Start();
        TileType = "Road";
    }
}
