using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMono<T> : MonoBehaviour 
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if( instance == null)
            {
                GameObject go = GameObject.Find("InstaceMgrMono");
                if( go == null)
                {
                    go = new GameObject("InstanceMgrMono");
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

}
