using UnityEngine;

public class CameraFollowPlayerSpawn : MonoBehaviour
{
    // private Transform target;
    // public void SetTarget(Transform newTarget){
    //     target = newTarget;
    // }

    void Update()
    {
        transform.position = Player.Instance.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        // if(target != null){
        //     Vector3 newPos = target.positio n;
        //     newPos.z = transform.position.z;
        //     transform.position = newPos;
        // }
    }
}
