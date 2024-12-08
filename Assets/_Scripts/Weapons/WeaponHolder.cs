using System;
using System.Collections;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject wType;
    public Canvas wInfoCanvas;
    public TextMeshProUGUI txtWName, txtWLvl, txtWHP, txtWDef, txtWSvvability, txtDescribe, txtNofi;
    public GameObject wFrame;
    public Button btnEnhance, btnEquipment;
    private int lvlNow = 1;
    private Weapon w;
    public GameObject enhance;
    public Player player;

    void Start()
    {   
        player = Player.Instance;
        btnEnhance.onClick.AddListener(OnEnhanceWeapon);
        btnEquipment.onClick.AddListener(OnWeaponEquipment);
    }

    public void ShowWeaponInfo(){
        if (w == null) 
        {
            w = GetComponentInChildren<Weapon>();
            if (w != null) lvlNow = w.wLvl;
        }

        if(w != null){
            lvlNow = w.wLvl;
            txtWName.text = "Chủng loại: " + w.wName;
            txtWLvl.text = (w.wLvl <= 0) ? "Chưa mở khóa" : (w.wLvl <= 99) ?  "Lvl hiện tại: " + w.wLvl :  "Lvl hiện tại: 99" ;
            txtWHP.text = "Chỉ số HP: " + w.wHealth;
            txtWDef.text = "Chỉ số phòng thủ: " + w.wDef;
            txtWSvvability.text = "Chỉ số sinh tồn: " + w.wSvvability;
            txtDescribe.text = "Âm thanh: " + w.describe;
            if(wFrame != null){
                wFrame.GetComponent<Image>().sprite = wType.GetComponent<SpriteRenderer>().sprite;
            }
            wInfoCanvas.gameObject.SetActive(true);
        }else{
            Debug.Log("Ủa không tìm thấy vũ khí :v");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            ShowWeaponInfo();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideWeaponInfo();
        }
    }

    public void HideWeaponInfo(){
        w = null;
        if(wInfoCanvas != null){
            wInfoCanvas.gameObject.SetActive(false);
        } 
    }

    public void OnWeaponEquipment()
    {
        if (w != null && player != null)
        {
            GameObject weaponInstance = Instantiate(wType);
            player.EquipWeapon(weaponInstance);
            Debug.Log("Đã trang bị vũ khí");
            Destroy(weaponInstance);
        }
        else
        {
            //Debug.Log("Ủa không tìm thấy vũ khí hay người chơi đâu :v");
            return;
        }
    }

    public void OnEnhanceWeapon() {
        if(w != null){
            if(lvlNow < 99){ 
                PlayerData playerData = GameManager.Instance.GetPlayerData();
                if(playerData != null){
                    lvlNow ++;
                    w.UpdateStat(lvlNow);
                    GameManager.Instance.UpdateMoonG(-200); // thực hiện trước nhé
                    GameManager.Instance.UpdateWeaponLevel(w.wCode, lvlNow);
                    WorldWhisperManager.Instance.GetComponent<WorldSetting>().UpdateUIGem();
                    GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
                    StartCoroutine(WaitForSecond(1.5f));

                }
            }else{
                // không nâng cấp được nữa (cap level)
            }
        }else{
            //Debug.Log("Ủa không tìm thấy vũ khí đâu :v");
            return;
        }
        
    }

    private IEnumerator WaitForSecond(float duration){
        if(enhance != null){
            enhance.transform.position = w.transform.position;
            enhance.SetActive(true);
            HideWeaponInfo();         
        } 
        yield return new WaitForSeconds(duration);
        enhance.SetActive(false);
        //Debug.Log("Đã nâng cấp thành công");
        ShowWeaponInfo();
    }
}