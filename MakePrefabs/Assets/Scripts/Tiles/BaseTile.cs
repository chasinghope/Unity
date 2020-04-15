using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color HightlightColor;
    public LayerMask ObLayermask;
    // 该瓦片是否可以防止防御塔
    public bool CanPlace = false;
    private bool haveObstacle = false;

    //TODO isBuilding belongTo levelManager.cs
    // public bool isBuilding = true;

    //TODO  need a string variable To represent what you will build. the variable should in levelManager.cs
    //MAKER  Check the string variable whether in my build list.
    //        if yes  ->    CanPlace = true;
    //        if no  ->    CanPlace = false;
    //TODO  to make a BaseTile.cs. And  need a tileType variable to represent tile's type.
    //MAKER   according to the tileType to make the build list.

    // tileType have three type. they are  1. Road 2. Grass 3. OpenSpace
    private string tileType;        
    public string TileType
    {
        set
        {
            tileType = value;
            ToMakeMyBuilList(tileType);
        }
    }


    private List<string> myBuildList = new List<string>();
    private void ToMakeMyBuilList(string tileType)
    {
        switch (tileType)
        {
            case "Grass":
                myBuildList.Add("Tower/CastleTower");
                myBuildList.Add("Tower/ArcherTower");
                break;

            case "Road":
                myBuildList.Add("Block/Rock");
                break;

            case "OpenSpace":
                myBuildList.Add("Builder/SoilderBuilder");
                break;

            default: Debug.LogError("tile class tileType variable error");  break;
        }
    }

    /// <summary>
    public void CheckBuildObject(string tileType)       // tileType is LevelManager.cs 
    {
        CanPlace = false; 
        foreach (var item in myBuildList)
        {
            if( tileType == item && !haveObstacle)
            {
                // Debug.Log( "myBuildList 成员： " +item);
                CanPlace = true;
                break;
            }

        }
    }


    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CheckObstale();
    }

    
    #region OnMouseXX()  
    private void OnMouseEnter()
    {
        if (!LevelManager.Instance.IsBuilding || !CanPlace)
            return;
        // Debug.Log("瓦片类型" + tileType);
        spriteRenderer.sortingOrder = 25;
        spriteRenderer.color = HightlightColor;
        StartCoroutine(CancleHightLight());
    }

    private void OnMouseExit()
    {
        if (!LevelManager.Instance.IsBuilding || !CanPlace)
            return;
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.color = Color.white;
    }


    /// <summary>
    /// 5s 后取消瓦片高亮
    /// </summary>
    /// <returns></returns>
    private IEnumerator CancleHightLight()
    {
        yield return new WaitForSeconds(5f);
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (!LevelManager.Instance.IsBuilding || !CanPlace)
            return;

        ToBuild(LevelManager.Instance.BuildTarget) ;
    }


    /// <summary>
    /// 执行建造
    /// </summary>
    /// <param name="tmp"></param>
    private void ToBuild(string tmp)
    {
        ResMgr.Instace.LoadAsync<GameObject>(tmp, (obj) =>
        {
            obj.transform.position = transform.position;
            BuildTargetCallback(obj);
        });
        haveObstacle = true;
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.color = Color.white;
        LevelManager.Instance.IsBuilding = false;
    }

    protected virtual void BuildTargetCallback(GameObject obj)
    {
        //Do something
    }

    // Cheak whether have Obstacle in here.
    private void CheckObstale()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, spriteRenderer.bounds.extents.x, ObLayermask);
        if( coll != null)
        {
            haveObstacle = true;
        }
        else
        {
            haveObstacle = false;
        }
    }



    #endregion
    
}
