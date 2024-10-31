using UnityEngine;

public class LoginMonoBehaviourScript : MonoBehaviour
{
    void Start()
    {
        if(FirebaseManager.Instance != null)
        {
            Debug.Log("Firebase is ready to use!");
            // Ở đây đã có thể sử dụng FirebaseManager.Instance.DbReference để read/write dữ liệu
        }
        else
        {
            Debug.Log("Firebase is not initizlized yet!");
        }
    }

    void Update()
    {
        
    }
}
