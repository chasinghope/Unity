using UnityEngine;


/// <summary>
/// WASD 控制相机移动
/// 限制相机移动范围
/// 
/// </summary>
public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float size = 6;                 // 摄像头范围
    [SerializeField]
    private float speed = 6;

    private Camera myCamera;
    private Transform trans;

    [SerializeField]
    private float sizeMax = 8;
    [SerializeField]
    private float sizeMin = 4;
    [SerializeField]
    private float sizeChange = 30f;
    //  [SerializeField]
    private float xMax = 31.49f;
    //  [SerializeField]
    private float yMax = 19.73f;
    //  [SerializeField]
    private float xMin = -31.26f;
    //  [SerializeField]
    private float yMin = -18.44f;

    private bool rightLimit;
    private bool upLimit;
    private bool leftLimit;
    private bool downLimit;

    #region Unity Mono
    private void Awake()
    {
        myCamera = GetComponent<Camera>();
        trans = myCamera.transform;
    }
    // Start is called before the first frame update
    private void Start()
    {
        myCamera.orthographicSize = size;
    }

    private void Update()
    {
        Limited();
        InputC();
        Zoom();
    }

    #endregion

    private void Limited()
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        Vector3 sp = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        if (wp.x >= xMax)
            rightLimit = true;
        else rightLimit = false;

        if (wp.y >= yMax)
            upLimit = true;
        else upLimit = false;

        if (sp.x <= xMin)
            leftLimit = true;
        else leftLimit = false;

        if (sp.y <= yMin)
            downLimit = true;
        else downLimit = false;

    }

    private void InputC()
    {
        if (Input.GetKey(KeyCode.W) && !upLimit)
        {
            trans.position += Time.deltaTime * speed * Vector3.up;
        }

        if (Input.GetKey(KeyCode.A) && !leftLimit)
        {
            trans.position += Time.deltaTime * speed * Vector3.left;
        }

        if (Input.GetKey(KeyCode.D) && !rightLimit)
        {
            trans.position += Time.deltaTime * speed * Vector3.right;
        }

        if (Input.GetKey(KeyCode.S) && !downLimit)
        {
            trans.position += Time.deltaTime * speed * Vector3.down;
        }
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (Camera.main.orthographicSize <= sizeMax && !upLimit && !downLimit && !leftLimit && !rightLimit)
                Camera.main.orthographicSize += sizeChange * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            if (Camera.main.orthographicSize >= sizeMin)
                Camera.main.orthographicSize -= sizeChange * Time.deltaTime;
        }

    }

}