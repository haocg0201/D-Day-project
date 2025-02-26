using UnityEngine;

public class PlayerTouchCatchRange : MonoBehaviour
{
    private KingEclipseNPC king;
    public GameObject panelBossHp;

    void Start()
    {
        // Lấy tham chiếu tới script BossController từ GameObject cha
        king = GetComponentInParent<KingEclipseNPC>();
        panelBossHp.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            king.isWalking = true;
            panelBossHp.SetActive(true);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            king.isWalking = false;
            panelBossHp.SetActive(false);
        }
    }
}
