using UnityEngine;

public class CameraFollowPlayerSpawn : MonoBehaviour
{
    void Update()
    {
        if(Player.Instance != null){
            transform.position = Player.Instance.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
