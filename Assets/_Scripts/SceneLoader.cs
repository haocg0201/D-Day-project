using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set;}
    private float elapsedTime = 0f;
    private bool isCounting = false;

    public void StartCounting()
    {
        elapsedTime = 0f;
        isCounting = true;
    }

    public void StopCounting()
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

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StopCounting();
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
            //Debug.Log($"Elapsed Time: {elapsedTime} seconds");
        }
        
        if(GameManager.Instance != null && GameManager.Instance.Health <= 0){
            SetElapsedTime();
        }
        
    }

    public void SetElapsedTime(){
        GameManager.Instance.svvTime = elapsedTime;
        Debug.Log($"Time: {GameManager.Instance.svvTime}");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log($"Scene Loaded:{scene.name}");
        if(scene.name == "NewBorn"){
            EnemySpawner.Instance.ClearActiveEnemies();

        }
        if(scene.name == "SVV" || scene.name == "Campaign_Dark_Broken" || scene.name == "Campaign_Desert" || scene.name == "Campaign_Winter" || scene.name == "Campaign_Swamp"){
            GameManager.Instance.SetStat();
            Player.Instance.PlayerIdle();
        }
        Player.Instance.isConsume = true;
        //Debug.Log($"Scene Loaded: {scene.name}");

        switch (scene.name)
        {
            case "NewBorn":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
                break;

            case "BossFight":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.bossFightMusic);
                break;

            case "SVV":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.svv);
                StartCounting();
                break;
            case "Campaign_Dark_Broken":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campA);
                break;
            case "Campaign_Desert":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campB);
                break;
            case "Campaign_Winter":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campC);
                break;
            case "Campaign_Swamp": 
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campD);
                break;

            default:
                AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
                break;
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
        while (!operation.isDone)
        {
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
