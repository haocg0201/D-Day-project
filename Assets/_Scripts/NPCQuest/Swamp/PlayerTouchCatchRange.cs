using UnityEngine;

public class PlayerTouchCatchRange : MonoBehaviour
{
    private KingEclipseNPC king;

    void Start()
    {
        // Lấy tham chiếu tới script BossController từ GameObject cha
        king = GetComponentInParent<KingEclipseNPC>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            king.isWalking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            king.isWalking = false;
        }
    }
}
