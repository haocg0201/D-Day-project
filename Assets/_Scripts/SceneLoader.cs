using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set;}
    private float elapsedTime = 0f;
    private bool isCounting = false;

    void StartCounting()
    {
        elapsedTime = 0f;
        isCounting = true;
    }

    void StopCounting()
    {
        Debug.Log($"Thời gian đếm được: {elapsedTime} giây");
        elapsedTime = 0f;
        isCounting = false;
    }

    void Start()
    {
        elapsedTime = 0f;
    }

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);    
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
        }
        
        if(GameManager.Instance != null && GameManager.Instance.Health == 0){
            GameManager.Instance.svvTime = elapsedTime;
            StopCounting();
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log($"Scene Loaded:{scene.name}");
        if(scene.name == "NewBorn"){
            EnemySpawner.Instance.ClearActiveEnemies();
        }
        if(scene.name == "SVV" || scene.name == "Campaign_Dark_Broken" || scene.name == "Campaign_Desert" || scene.name == "Campaign_Winter"){
            GameManager.Instance.SetStat();
            StartCounting();
        }
    }

    public void LoadSceneBySceneName(string sceneName){
        if(GameManager.Instance != null){
            GameManager.Instance.SaveAndUpdatePlayerDataFireBase();
            GameManager.Instance.ResetTheCounter();
            GameManager.Instance.PauseGame(false);
        }
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // public void LoadSceneBySceneIndex(int sceneIndex){
    //     GameManager.Instance.ResetTheCounter();
    //     GameManager.Instance.PauseGame(false);
    //     StartCoroutine(LoadSceneAsync(sceneIndex));
    // }

    private IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone){
            yield return null;
        }
    }

    public void ReloadCurrentScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        LoadSceneBySceneName(currentScene.name);
    }
    
    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
