using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldSetting : MonoBehaviour
{
    public Button btnPause, btnClose, btnSound, btnControl, btnLogout, btnExit, btnSaveToFireBase, btnHome;
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
        btnHome.onClick.AddListener(OnHomeClick);
        UpdateUIGem();
    }

    public void OnHomeClick(){
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "NewBorn")
        {
            SceneLoader.Instance.LoadSceneBySceneName("NewBorn");
            panelSetting.SetActive(false);
        }
        else
        {
            panelSetting.SetActive(false);
            GameManager.Instance.PauseGame(false);
        }
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
        SceneLoader.Instance.QuitGame();
    }
    public void OnSaveToFireBase(){
        GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
    }
}
