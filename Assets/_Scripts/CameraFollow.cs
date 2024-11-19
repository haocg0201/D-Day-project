using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Drag player object to this field in Inspector
    //public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, -10f);
        transform.position = smoothedPosition;
    }
}
