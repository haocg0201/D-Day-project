using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldSetting : MonoBehaviour
{
    public Button btnPause, btnClose, btnSound, btnControl, btnLogout, btnExit, btnSaveToFireBase, btnHome, btnAboutUs;
    public GameObject panelSetting, panelSound, panelControl, panelAboutUs;
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
        btnAboutUs.onClick.AddListener(OnAboutUs);
        UpdateUIGem();
    }

    private void OnEnable() {
        Player.Instance.isConsume = false;
    }
    void OnDisable()
    {
        Player.Instance.isConsume = true;
    }

    public void OnAboutUs(){
        SoundEffect();
        panelAboutUs.SetActive(true);
        panelSound.SetActive(false);
        panelControl.SetActive(false);
    }

    public void OnHomeClick(){
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SoundEffect();

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
        SoundEffect();
        GameManager.Instance.PauseGame(true);
        panelSetting.SetActive(true);
        panelSound.SetActive(true);
    }

    public void OnClose(){
        SoundEffect();
        GameManager.Instance.PauseGame(false);
        panelSetting.SetActive(false);
    }
    public void OnControl(){
        SoundEffect();
        panelSound.SetActive(false);
        panelControl.SetActive(true);
        panelAboutUs.SetActive(false);
    }

    public void OnSound(){
        SoundEffect();
        panelSound.SetActive(true);
        panelControl.SetActive(false);
        panelAboutUs.SetActive(false);
    }

    public void OnLogout(){
        SoundEffect();
        StartCoroutine(LoadSceneAsync("Login"));
    }
    public void OnExit(){
        SoundEffect();
        SceneLoader.Instance.QuitGame();
    }
    public void OnSaveToFireBase(){
        SoundEffect();
        GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
    }

    private void SoundEffect(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
    }

        private IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        OnClose();
        GameManager.Instance.OnLogout();
    }
}
