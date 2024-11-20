using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    //public Vector3 offset = new Vector3(2, 2, -10);

    void LateUpdate()
    {
        
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x,player.position.y,-10);
    }
}
