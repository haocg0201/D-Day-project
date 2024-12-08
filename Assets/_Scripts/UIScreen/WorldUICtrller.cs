using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSetting : MonoBehaviour
{
    public Button btnPause, btnClose, btnSound, btnControl, btnLogout, btnExit, btnSaveToFireBase;
    public GameObject panelSetting, panelSound, panelControl;
    public TextMeshProUGUI txtMoonG, txtRuneG;
//    public GameObject[] tabs;

    void Start()
    {
        btnPause.onClick.AddListener(OnPause);
        btnClose.onClick.AddListener(OnClose);
        btnSound.onClick.AddListener(OnSound);
        btnControl.onClick.AddListener(OnControl);
        btnLogout.onClick.AddListener(OnLogout);
        btnExit.onClick.AddListener(OnExit);
        btnSaveToFireBase.onClick.AddListener(OnSaveToFireBase);
        UpdateUIGem();
    }

    public void UpdateUIGem(){
        txtMoonG.text = GameManager.Instance.playerData.stat.gem.ToString();
        txtRuneG.text = GameManager.Instance.playerData.stat.rune.ToString();
    }

    public void OnPause(){
        GameManager.Instance.PauseGame(true);
        panelSetting.SetActive(true);
        panelSound.SetActive(true);
    }

    public void OnClose(){
        GameManager.Instance.PauseGame(false);
        panelSetting.SetActive(false);
    }
    public void OnControl(){
        panelSound.SetActive(false);
        panelControl.SetActive(true);
    }

    public void OnSound(){
        panelSound.SetActive(true);
        panelControl.SetActive(false);
    }

    public void OnLogout(){

    }
    public void OnExit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void OnSaveToFireBase(){
        GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
    }

    // public void ShowTab(int index)
    // {
    //     for (int i = 0; i < tabs.Length; i++)
    //     {
    //         tabs[i].SetActive(i == index);
    //     }
    // }

}
