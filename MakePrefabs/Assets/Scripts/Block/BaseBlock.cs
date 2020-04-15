using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBlock : MonoBehaviour
{
    #region 石头属性
    protected int HP { get; set; }
    protected int DEF { get; set; }
    private Slider HPSlider;
    public bool IsAlive => HP >= 0;

    private string selfTag = "Block";
    private string selfSortingLayerName = "Block";
    private int selfLayer = (int)EnemyTeam.block;
    #endregion


    private void Awake()
    {
        HPSlider = gameObject.GetComponentInChildren<Slider>();
        /// 设置tag layer  sortingLayer
        gameObject.tag = selfTag;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = selfSortingLayerName;
        gameObject.layer = selfLayer;      
    }

    private void Start()
    {
        /// 设置石头参数
        HP = 500;
        DEF = 5;
        HPSlider.maxValue = (float)HP;
        HPSlider.value = HPSlider.maxValue;
    }


    public void Do_Hurt(int damage)
    {
        if (damage > DEF)
        {
            damage = damage - DEF;
            HP -= damage;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
            return;
        }

        HPSlider.value = HP;

    }

}
