using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOpenSpace : BaseTile
{
    public string CastleName;  //   "Castle1"  "Castle2"    "Castle3"      "Castle4"     "Castle5s"
    protected override void Start()
    {
        base.Start();
        TileType = "OpenSpace";
    }

    protected override void BuildTargetCallback(GameObject obj)
    {
        base.BuildTargetCallback(obj);
        Debug.Log("buildTargetCallback Test" +obj.gameObject.name);
        obj.GetComponent<SoilderBuilder>().Name = CastleName;
        // obj.transform.position.y -= 0.42f;
        // 调整防止城堡的位置
        obj.transform.position -= new Vector3(0, 0.42f, 0);
    }


}

