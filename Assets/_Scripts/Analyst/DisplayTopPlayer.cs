using UnityEngine;

public class DisplayTopPlayer : MonoBehaviour
{
    public GameObject panelTopPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        panelTopPlayer.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        panelTopPlayer.SetActive(false);
    }
}
