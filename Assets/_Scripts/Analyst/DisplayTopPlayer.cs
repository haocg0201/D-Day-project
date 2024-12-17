using UnityEngine;
using UnityEngine.UI;

public class DisplayTopPlayer : MonoBehaviour
{
    public GameObject panelTopPlayer;
    public Button btnClose;

    void Start()
    {
        btnClose.onClick.AddListener(() => {OnClose();});
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player") && Player.Instance != null){
            Player.Instance.isConsume = false;
            panelTopPlayer.SetActive(true);
        }      
    }

    void OnClose(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        Player.Instance.isConsume = true;
        panelTopPlayer.SetActive(false);
    }
}
