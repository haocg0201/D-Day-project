using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUp : MonoBehaviour
{
    public TextMeshProUGUI txtDef, txtPDef, txtHealth, txtPHealth, txtAtk, txtPAtk, txtSvvability, txtPSvvability,txtBiography, txtLv;
    public Button btnPlayerLevelUp;
    public GameObject lvlPanel;
    void Start()
    {
        btnPlayerLevelUp.onClick.AddListener(OnPlayerLevelUp);
        SystemInfo();
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
            txtBiography.text = $"Hành trình trở về của nhân viên văn phòng mạnh nhất hiện tại.\n \n \n \n Số rune cần để tăng level bằng 100 + {GameManager.Instance.playerData.stat.level * 10}";
        }
    }

    void OnPlayerLevelUp(){
        if(GameManager.Instance != null){
            int lvlCost = GameManager.Instance.LvlCost();
            if(GameManager.Instance.playerData.stat.rune > lvlCost){
                GameManager.Instance.OnPlayerLevelUp(lvlCost);
                StartCoroutine(Wait(1.5f));
            }           
        }
    }

    private IEnumerator Wait(float time){
        lvlPanel.SetActive(false);
        SystemInfo();
        yield return new WaitForSeconds(time);
        lvlPanel.SetActive(true);
    }
}
