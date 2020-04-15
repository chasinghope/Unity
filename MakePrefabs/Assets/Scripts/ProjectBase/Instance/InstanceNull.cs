using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceNull<T> where T : new()
{
    private static T instace;
    public static T Instace
    {
        get
        {
            if (instace == null)
            {
                instace = new T();
            }
            return instace;
        }
    }
}
