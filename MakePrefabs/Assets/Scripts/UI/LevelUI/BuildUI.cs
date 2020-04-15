using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    public string BuilderObjectType;          // "CastleTower"       "ArcherTower"   "SoilderBuilder"   "Rock"
    public TextMeshProUGUI BillCount;
    public Button BuildBtn;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        money = BillMgr.Instace.GetObjectBill(BuilderObjectType);
        BillCount.text = money.ToString();
        BuildBtn.onClick.AddListener( BuildBtn_onClick );
    }

    private void BuildBtn_onClick()
    {
        if(!LevelManager.Instance.IsBuilding)
        {
            // 判断能不能买得起
            if (BillMgr.Instace.EnoughToBuy(money))
            {
                // 
                LevelManager.Instance.BuildTarget = BuilderObjectType;
                LevelManager.Instance.IsBuilding = true;
                
                // Debug.Log(LevelManager.Instance.BuildTarget);
            }
        }
    }




}
