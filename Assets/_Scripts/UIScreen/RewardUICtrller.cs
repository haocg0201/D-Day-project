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
   // string survvTime;
    void Start()
    {
        btnReplay.onClick.AddListener(OnReplay); // tôi đặt điều kiện check null ở dưới cho chắc vì người chơi ấn nhiều lần
        btnHome.onClick.AddListener(OnHome);
        SetInf();
    }
    void OnEnable()
    {
        if(SceneLoader.Instance != null){
            SceneLoader.Instance.StopCounting();
        }
        
    }

    void SetInf(){
        if(GameManager.Instance != null){
            txtSvvTime.text = "Thời gian sinh tồn: " + GameManager.Instance.svvTime + "s";
            txtKillCount.text = "Số lượng quái vật bị tiêu diệt: " + GameManager.Instance.killCount;
            txtRewardMG.text = "Lượng nguyệt thạch thu thập được: " + GameManager.Instance._mgCounter;
            txtRewardRG.text = "Lượng rune thu thập được: " + GameManager.Instance._rgCounter;
        }   
    }

    public void OnReplay(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(Player.Instance != null && GameManager.Instance != null){
            GameManager.Instance.PauseGame(false);
            GameManager.Instance.ResetTheCounter();
            EnemySpawner.Instance.ClearActiveEnemies();
            Player.Instance.Recovery();
            SceneLoader.Instance.StartCounting();
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
            GameManager.Instance.ResetTheCounter();
            rewardPanel.SetActive(false);
            SceneLoader.Instance.LoadSceneBySceneName("NewBorn");
        }
    }

    private IEnumerator Wait(float time){
        yield return new WaitForSeconds(time);
    }

}
