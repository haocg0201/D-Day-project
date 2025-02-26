using UnityEngine;

public class PaperScroll : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            GameManager.Instance.killCountBoss += 1;
            Destroy(gameObject,0.5f);
        }
    }
}
