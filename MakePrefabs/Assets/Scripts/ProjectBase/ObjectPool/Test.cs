using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChunkAllocator.Instace.GetPrefab("Cube", (o) => { });
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChunkAllocator.Instace.GetPrefab("Sphere",(o) => { } );
        }
    }


}
