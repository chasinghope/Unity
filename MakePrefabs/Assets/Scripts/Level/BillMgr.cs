using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillMgr : InstanceNull<BillMgr>
{
    private int billCount;

    #region 游戏对象物价
    // 游戏对象物价
    // enemyxInBill = enemyxOutBill * 0.6
    private int enemy1OutBill = 30;
    private int enemy2OutBill = 30;
    private int enemy3OutBill = 45;
    private int archerTowerOutBill = 100;
    private int castleTowerOutBill = 120;
    private int soilderBuilderOutBill = 300;
    private int rockOutBill = 50;

    private int enemy1InBill = 18;
    private int enemy2InBill = 18;
    private int enemy3InBill = 25;


    #endregion




    /// <summary>
    /// 初始化金钱
    /// </summary>
    /// <param name="count"></param>
    public void InitBill(int count)
    {
        billCount = count;
    }

    /// <summary>
    /// 查看金钱
    /// </summary>
    /// <returns>玩家拥有的金钱</returns>
    public int GetBill()
    {
        return billCount;
    }

    /// <summary>
    /// 赚钱
    /// </summary>
    /// <param name="count">赚的钱</param>
    public void EarnBill(int count)
    {
        billCount += count;
    }


    /// <summary>
    /// 花钱   返回值为true -> 买得起并买了         false -> 买不起，并没有买
    /// </summary>
    /// <param name="count">要花的钱</param>
    /// <returns>是否花得起</returns>
    public bool EnoughToBuy(int count)
    {
        if( billCount - count >= 0)
        {
            billCount -= count;
            return true;
        }
        else
        {
            return false;
        }

    }


    /// <summary>
    /// 获取游戏对象的价格
    /// </summary>
    /// <param name="tmp">要获取的游戏对象名称</param>
    /// <returns>对应的价格</returns>
    public int GetObjectBill(string tmp)
    {
        int bill;
        switch (tmp)
        {
            case "Enemy1":
                bill =  enemy1OutBill;
                break;
            case "Enemy2":
                bill = enemy2OutBill;
                break;
            case "Enemy3":
                bill = enemy3OutBill;
                break;

            case "ArcherTower":
                bill = archerTowerOutBill;
                break;
            case "CastleTower":
                bill = castleTowerOutBill;
                break;

            case "SoilderBuilder":
                bill = soilderBuilderOutBill;
                break;
            case "Rock":
                bill = rockOutBill;
                break;


            default: Debug.LogError("BillMgr.cs 参数有误！");
                return bill = 0;


        }
        return bill;
    }

    /// <summary>
    /// 赚钱游戏对象的价格
    /// </summary>
    /// <param name="tmp">要赚钱的游戏对象名称</param>
    /// <returns>对应的价格</returns>
    public int EarnObjectBill(string tmp)
    {
        int bill;
        switch (tmp)
        {
            case "Enemy1":
                bill = enemy1InBill;
                break;
            case "Enemy2":
                bill = enemy2InBill;
                break;
            case "Enemy3":
                bill = enemy3InBill;
                break;


            default:
                Debug.LogError("BillMgr.cs 参数有误！");
                return bill = 0;
        }
        return bill;
    }

}
