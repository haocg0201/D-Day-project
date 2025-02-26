using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUp : MonoBehaviour
{
    public TextMeshProUGUI txtDef, txtPDef, txtHealth, txtPHealth, txtAtk, txtPAtk, txtSvvability, txtPSvvability,txtBiography, txtLv;
    public Button btnPlayerLevelUp, btnClose;
    public GameObject lvlPanel;
    void Start()
    {
        btnPlayerLevelUp.onClick.AddListener(OnPlayerLevelUp);
        btnClose.onClick.AddListener(OnClose);
        SystemInfo();
    }

    private void Awake() {
        SystemInfo();
    }
    void OnEnable()
    {
        if(Player.Instance != null){
            Player.Instance.isConsume = false;
        }
    }

    private void OnDisable() {
        if(Player.Instance != null){
            Player.Instance.isConsume = true;
        }
    }

    void OnClose(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
    }

    void SystemInfo(){
        if(GameManager.Instance != null){
            txtDef.text = GameManager.Instance.Def + "";
            txtPDef.text = $"+ {GameManager.Instance.DefFromWeapon}";
            txtHealth.text = GameManager.Instance.Health + " ";
            txtPHealth.text =  $"+ {GameManager.Instance.HealthFromWeapon}";
            txtAtk.text = GameManager.Instance.Dmg + " ";
            txtPAtk.text =  $"+ {GameManager.Instance.DmgFromWeapon}";
            txtSvvability.text = GameManager.Instance.Survivability + " ";
            txtPSvvability.text =  $"+ {GameManager.Instance.SurvivabilityFromWeapon}";
            txtLv.text = $"Level hiện tại: {GameManager.Instance.playerData.stat.level}";
            txtBiography.text = $"Hành trình trở về của nhân viên văn phòng mạnh nhất hiện tại.\n \n \n \n \n Số rune cần để tăng level bằng 100 + {GameManager.Instance.playerData.stat.level * 10}";
        }
    }

    void OnPlayerLevelUp(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(GameManager.Instance != null){
            int lvlCost = GameManager.Instance.LvlCost();
            if(GameManager.Instance.playerData.stat.rune > lvlCost){
                GameManager.Instance.OnPlayerLevelUp(lvlCost);
                StartCoroutine(Wait(1f));
            }else{
                WorldWhisperManager.Instance.TextBayLen("Bạn không đủ rune để tăng level");
            }           
        }else{
            Debug.Log("GameManager.Instance is null");
        }
    }

    private IEnumerator Wait(float time){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerUpgradeSound);
        lvlPanel.SetActive(false);
        yield return new WaitForSeconds(time);
        WorldWhisperManager.Instance.TextBayLen("Nâng cấp độ nhân vật thành công");
        lvlPanel.SetActive(true);
        SystemInfo();
    }
}
