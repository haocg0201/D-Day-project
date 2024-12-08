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
    void Start()
    {
        btnReplay.onClick.AddListener(OnReplay);
        btnHome.onClick.AddListener(OnHome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReplay(){
        rewardPanel.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        GameManager.Instance.PauseGame(false);
        Debug.Log("Replay" + currentSceneName);
    }
    public void OnHome(){
        rewardPanel.SetActive(false);
        StartCoroutine(WaitSceneLoading());
    }

    private IEnumerator WaitSceneLoading(){
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(4); // home, scene index = 4;
        GameManager.Instance.PauseGame(false);
        while(!asyncOperation.isDone){
            yield return null;
        }
        Debug.Log("Home" + asyncOperation.ToString());
    }
}
