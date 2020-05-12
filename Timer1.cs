using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime;                   //  回合总时间
    [SerializeField] private float burningTime;             //  燃烧绳子的时间
    [SerializeField] private float playerTime;                //  玩家剩余的时间，游戏一开始时候，玩家的剩余时间将会等于每回合可用的总时间

    [SerializeField] private GameObject ropeGO;        //  用于显示或隐藏绳子
    [SerializeField] private Slider ropeSlider;              

    [SerializeField] private int playerID;                     //   0 or 1 区分玩家0和玩家1

    [Header("角色图像")]
    [SerializeField] private Image player00Image;
    [SerializeField] private Image player01Image;


    private void Awake()
    {
        playerID = 0;
        RoleImage(playerID);
        //   playerID = 0;
        //   player00Image.color = Color.white;
        //   player01Image.color = Color.black;

        ropeGO.SetActive(false);
        ropeSlider.maxValue = burningTime;
        ropeSlider.value = burningTime;
    }

    private void Start()
    {
        playerTime = totalTime;
    }

    private void Update()
    {
        playerTime -= Time.deltaTime;

        // TODO 开启燃烧绳子
        if ( playerTime <= burningTime )
        {            
            if (!ropeGO.activeSelf)
                ropeGO.SetActive(true);
            
            ropeSlider.value = playerTime;
        }          

        //TODO 绳子燃烧尽
        if( playerTime <= 0)
        {
            ManualStop();
        }

    }

   public void ManualStop()
    {
        RoleShift();
        // NextTurn();
        ropeGO.SetActive(false);       
        playerTime = totalTime;
    }

    private void RoleShift()
    {
        if (playerID == 0)
            playerID = 1;
        if (playerID == 1)
            playerID = 0;

        RoleImage(playerID);
    }

    private void RoleImage(int id)
    {
        switch (id)
        {
            case 0:
                player00Image.color = Color.white;
                player01Image.color = Color.black;
                Debug.Log("00");
                break;
            case 1:
                player00Image.color = Color.black;
                player01Image.color = Color.white;
                Debug.Log("11");
                break;
        }
        Debug.Log("Enter here.");

    }

    private void NextTurn()
    {
        if (playerID == 0)//当玩家ID等于0，那么就切换为1，并且进行相应的变色
        {
            playerID = 1;
            player01Image.color = Color.white;
            player00Image.color = Color.black;
        }
        else if (playerID == 1)//当玩家ID等于1，那么就切换为0，并且进行相应的变色
        {
            playerID = 0;
            player00Image.color = Color.white;
            player01Image.color = Color.black;
        }
    }
}
