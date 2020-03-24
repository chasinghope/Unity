using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Push", 1);
    }

    void Push()
    {
        Debug.Log(this.gameObject.name);
        ChunkAllocator.Instace.PushPrefab(this.gameObject.name, this.gameObject);
    }
}
