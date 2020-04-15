using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateUI : MonoBehaviour
{

    [Header("必须设置->事件中心")]
    public string CreateObjectTag;   // "城堡名_敌人名   _动作"

    // 动作
    private string add = "_Add";
    private string remove = "_Remove";
    private string nums = "_Num";

    [Header("子控件")]
    public Button AddBtn;
    public Button RemoveBtn;
    public TextMeshProUGUI EnemyNumText;
    public TextMeshProUGUI BillNumText;

    private int billNum = 0;

    private void Start()
    {
        SetBillNum();

        AddBtn.onClick.AddListener(
            () =>
            {
                // Debug.Log("点击添加按钮啦");
                // Debug.Log("花钱前 ： " + BillMgr.Instace.GetBill() );
                // Debug.Log("花钱对象名" + billNum);
                if( BillMgr.Instace.EnoughToBuy(billNum))
                {
                    //  Debug.Log("我花钱了把");
                    EventManager.Instace.EventTrigger(CreateObjectTag + add);
                }
               //  Debug.Log("花钱后 ： " + BillMgr.Instace.GetBill());

                // Debug.Log("触发事件" + CreateObjectTag + add);
            });

        RemoveBtn.onClick.AddListener(
            () =>
            {
                Debug.Log(int.Parse(EnemyNumText.text));
                if( int.Parse(EnemyNumText.text) > 0)
                {
                    BillMgr.Instace.EarnBill(billNum);
                    EventManager.Instace.EventTrigger(CreateObjectTag + remove);
                }
                
            });
    }

    private void Update()
    {
        EventManager.Instace.AddEventListener<int>(CreateObjectTag + nums,
            (a) =>
            {
                EnemyNumText.text = a.ToString();
            });
    }


    private void SetBillNum()
    {
        string[] sArray = CreateObjectTag.Split('_');
        // 从金币管理器中获取对应金币
        // BillNumText.text = BillMgr.Instace.GetObjectBill(sArray[1]).ToString();
        billNum = BillMgr.Instace.GetObjectBill(sArray[1]);
        BillNumText.text = billNum.ToString();
    }
}

