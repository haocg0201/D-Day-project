using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardUICtrller : MonoBehaviour
{
    public GameObject rewardPanel;
    public TextMeshProUGUI txtSvvTime, txtKillCount, txtRewardMG, txtRewardRG;
    public Button btnReplay, btnHome;
    string survvTime;
    void Start()
    {
        btnReplay.onClick.AddListener(OnReplay); // tôi đặt điều kiện check null ở dưới cho chắc vì người chơi ấn nhiều lần
        btnHome.onClick.AddListener(OnHome);
        SetInf();
    }

    void SetInf(){
        if(GameManager.Instance != null){
            int seconds = (int)GameManager.Instance.svvTime; 
            int minutes = seconds / 60; 
            int remainingSeconds = seconds % 60; 
            string survvTime = minutes.ToString("00") + ":" + remainingSeconds.ToString("00");
            txtSvvTime.text = "Thời gian sinh tồn: " + survvTime;
            txtKillCount.text = "Số lượng quái vật bị tiêu diệt: " + GameManager.Instance.killCount;
            txtRewardMG.text = "Lượng nguyệt thạch thu thập được: " + GameManager.Instance._mgCounter;
            txtRewardRG.text = "Lượng rune thu thập được: " + GameManager.Instance._rgCounter;
        }   
    }

    public void OnReplay(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(Player.Instance != null && GameManager.Instance != null){
            GameManager.Instance.PauseGame(false);
            EnemySpawner.Instance.ClearActiveEnemies();
            Player.Instance.Recovery();
            rewardPanel.SetActive(false);
        }  
    }
    public void OnHome(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(Player.Instance != null && GameManager.Instance != null){
            
            Player.Instance.Recovery();
            Player.Instance.transform.position = new Vector3(0, 0, 0);
            GameManager.Instance.UpdateMoonG();
            GameManager.Instance.UpdateRune();
            GameManager.Instance.UpdateTraningTime();
            GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
            rewardPanel.SetActive(false);
            SceneLoader.Instance.LoadSceneBySceneName("NewBorn");
        }
    }

    private IEnumerator Wait(float time){
        yield return new WaitForSeconds(time);
    }

}
