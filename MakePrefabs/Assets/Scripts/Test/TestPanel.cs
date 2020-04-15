using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    private RectTransform rectTrans;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        // rectTrans.rect.Set(0, 0, 100, 200);  无用
        rectTrans.sizeDelta = new Vector2(100, 200);
        // Debug.Log(rectTrans.rect.width); 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
