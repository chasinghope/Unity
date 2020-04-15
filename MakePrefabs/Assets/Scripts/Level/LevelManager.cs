using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // 关卡标识  用于关卡加载和事件发送与接受
    public int LevelID;

    // 关卡玩家参数   用于ShowInfoPanel UI界面和事件传递
    // public int BillCount;
    [HideInInspector]
    public string LevelTimer;
    public int StarCount = 3;             // 星星数量，很重要

    // 关卡难度参数    考虑后期直接抽象为一个关卡难度类
    private int StartBill = 500;
    private float cTimer = 0;
    private int enemyBuildCount = 5;         // 地方建筑数 
    private bool playerBuildDeadLock = false;     // 玩家建筑被毁   是否减星



    // 鼠标点击选择建筑物
    [Header("鼠标光标")]
    public Texture2D castletowerCursor;
    public Texture2D archertowerCursor;
    public Texture2D soilderBuilderCursor;
    public Texture2D rockCursor;

    private Texture2D buildCursor;
    private string buildTarget;
    private int buildTargetBill;
    public string BuildTarget
    {
        get { return buildTarget; }
        set
        {
            buildTarget =  value;
            // 绑定参数
            BindParaBuildTarget();
            // 设置瓦片
            SetTile(buildTarget);
        }
    }

    private bool isBuilding;
    public bool IsBuilding { get { return isBuilding; } set { isBuilding = value; SetCursor(); } }

    private BaseTile[] allTile;

    // 游戏手柄
    private InputHandler inputHandler;


    #region Unity Mono
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        UIManager.Instace.HidePanel("LoadingPanel");

        //TODO  加载ShowInfoPanel
        UIManager.Instace.ShowPanel<ShowInfoPanel>("ShowInfoPanel", UILayer.top);

        //TODO 考虑协程
        // 加载地图的所有挂载BaseTile 的瓦片
        GameObject map = GameObject.Find("Map");
        allTile = map.GetComponentsInChildren<BaseTile>();
    }

    private void Start()
    {
        // 游戏状态从   暂停-> 恢复; 放BGM
        Time.timeScale = 1;
        MusicMgr.Instace.PlayBGM(GameManager.Instance.BGMName);

        // 加载关卡难度参数
        BillMgr.Instace.InitBill(StartBill);

        // 初始化游戏手柄
        inputHandler = new InputHandler(InputKeyDown);

        // 事件监听
        DealEvent();
    }

    private void Update()
    {
        CalcLevelTime();



    }

    
    #endregion


    #region 私有方法
    private int minute;
    private int second;

    private bool timeLock1 = false;
    private bool timeLock2 = false;
    private bool timeLock3 = false;
    /// <summary>
    /// 计算关卡游戏时间
    /// </summary>
    private void CalcLevelTime()
    {
        cTimer += Time.deltaTime;
        minute = (int)cTimer / 60;
        second = (int)cTimer - minute * 60;
        LevelTimer = string.Format("{0:D2}:{1:D2}", minute, second);

        if(minute >= 15)
        {
            StarCount -= 1;
        }

        // 发送事件节点事件  每隔5分钟发送一次事件
        if(minute == 5)
        {
            if (!timeLock1)
            {
                timeLock1 = true;
                EventManager.Instace.EventTrigger<int>("TimeEvent", minute);
            }
        }

        if (minute == 10)
        {
            if (!timeLock2)
            {
                timeLock2 = true;
                EventManager.Instace.EventTrigger<int>("TimeEvent", minute);
            }
        }

        if (minute == 15)
        {
            if (!timeLock3)
            {
                timeLock3 = true;
                EventManager.Instace.EventTrigger<int>("TimeEvent", minute);
            }
        }

    }

    bool QkeyTrigger = false;
    private void InputKeyDown(KeyCode code)
    {

        switch (code)
        {
            case KeyCode.Q:                                           // 打开UserPanel

                // Debug.Log("QkeyTrigger" + QkeyTrigger);
                if (QkeyTrigger == false)
                {
                    UIManager.Instace.ShowPanel<UserControlPanel>("UserControlPanel", UILayer.mid);
                }
                if (QkeyTrigger == true)
                {
                    UIManager.Instace.HidePanel("UserControlPanel");
                }
                QkeyTrigger = !QkeyTrigger;

                break;
            case KeyCode.J:                                           // 打开GameOverPanel
                UIManager.Instace.ShowPanel<GameOverPanel>("GameOverPanel", UILayer.top);
                break;
            case KeyCode.S:                                          // 向下
                break;
            case KeyCode.D:                                         // 向右
                break;
            case KeyCode.Space:                                // 跳跃 
                break;
            case KeyCode.Mouse1:
                // IsBuilding = false;
                IsBuilding = false;
                BillMgr.Instace.EarnBill(buildTargetBill);

                // TODO 鼠标左键取消建造模式并退还金币


                break;
        }
    }

    private void BindParaBuildTarget()
    {
        buildTargetBill = BillMgr.Instace.GetObjectBill(buildTarget);
        switch (buildTarget)
        {
            case "CastleTower":
                buildTarget = "Tower/CastleTower";
                buildCursor = castletowerCursor;
                
                break;

            case "ArcherTower":
                buildTarget = "Tower/ArcherTower";
                buildCursor = archertowerCursor;
                break;

            case "SoilderBuilder":
                buildTarget = "Builder/SoilderBuilder";
                buildCursor = soilderBuilderCursor;
                break;

            case "Rock":
                buildTarget = "Block/Rock";
                buildCursor = rockCursor;
                break;

            default:
                Debug.LogError("BuildUI BuilderObjectType variable error");

                break;
        }

    }



    /// <summary>
    /// 设置所有瓦片的List
    /// </summary>
    /// <param name="target"></param>
    private void SetTile(string target)
    {
        for (int i = 0; i < allTile.Length; i++)
        {
            allTile[i].CheckBuildObject(target);
        }
    }


    /// <summary>
    /// 设置鼠标光标
    /// </summary>
    private CursorMode cursorMode = CursorMode.Auto;
    private void SetCursor()
    {
        if( isBuilding)
        {
            // Set building Cursor
            Cursor.SetCursor(buildCursor, Vector2.zero, cursorMode);
        }
        else
        {
            // Set Cursor
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }


    private void DealEvent()
    {
        // 监听敌人单位死亡事件
        EventManager.Instace.AddEventListener<EnemyInfo>("enemyDead", (enemyInfo) =>
        {
            if (enemyInfo.enemyTeam == EnemyTeam.enemy)
            {
                BillMgr.Instace.EarnBill(BillMgr.Instace.GetObjectBill(enemyInfo.enemyType));
            }

        });

        // 监听建筑死亡事件
        EventManager.Instace.AddEventListener<string>("BuildDead", (tmpName)=> {

            switch (tmpName)
            {
                case "EnemyBuilder1":
                    //Dosomething
                    EnemyBuildDeadEarn();
                    break;

                case "EnemyBuilder2":
                    //Dosomething
                    EnemyBuildDeadEarn();
                    break;

                case "EnemyBuilder3":
                    //Dosomething
                    EnemyBuildDeadEarn();
                    break;

                case "EnemyBuilder4":
                    //Dosomething
                    EnemyBuildDeadEarn();
                    break;

                case "EnemyBuilder5":
                    //Dosomething
                    EnemyBuildDeadEarn();
                    break;

                case "Castle1":
                    //Dosomething
                    PlayerBuildDead();
                    break;

                case "Castle2":
                    //Dosomething
                    PlayerBuildDead();
                    break;

                case "Castle3":
                    //Dosomething
                    UIManager.Instace.ShowPanel<GameOverPanel>("GameOverPanel", UILayer.top);
                    break;

                case "Castle4":
                    //Dosomething
                    PlayerBuildDead();
                    break;

                case "Castle5":
                    //Dosomething
                    PlayerBuildDead();
                    break;


                default:
                    break;
            }


        });


    }


    /// <summary>
    /// 敌人建筑死亡收益
    /// </summary>
    private void EnemyBuildDeadEarn()
    {
        enemyBuildCount -= 1;
        if (enemyBuildCount == 0)
        {
            // 玩家胜利
            UIManager.Instace.ShowPanel<GameOverPanel>("GameOverPanel", UILayer.top);

        }

        BillMgr.Instace.EarnBill(300);
    }

    private void PlayerBuildDead()
    {
        if (!playerBuildDeadLock)
        {
            StarCount -= 1;
            playerBuildDeadLock = true;
        }
    }

    #endregion




    #region 对外方法

    /// <summary>
    /// 隐藏Level关卡所有UI
    /// </summary>
    public void HideLevelUI()
    {
        ChunkAllocator.Instace.ClearPool();
        UIManager.Instace.HidePanel("ShowInfoPanel");
        UIManager.Instace.HidePanel("SettingLevelPanel");
        UIManager.Instace.HidePanel("GameOverPanel");
        UIManager.Instace.HidePanel("UserControlPanel");
    }


    #endregion

}
