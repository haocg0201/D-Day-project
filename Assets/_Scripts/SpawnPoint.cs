using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
        void Start()
    {
        if(Player.Instance != null)
        {
            Player.Instance.transform.position = transform.position;
        }
    }
}
