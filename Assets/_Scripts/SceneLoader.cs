using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set;}
    private float elapsedTime = 0f;
    private bool isCounting = false;
    // private int atFirstDmg;
    // private float atFirstSvvability;

    public void StartCounting()
    {
        elapsedTime = 0f;
        isCounting = true;
    }

    public void StopCounting()
    {
        Debug.Log($"Thời gian đếm được: {elapsedTime} giây");
        GameManager.Instance.svvTime = elapsedTime;
        isCounting = false;
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
        SceneManager.sceneUnloaded += OnSceneUnloaded; 
        
    }

    

    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded; 
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded; 
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
            //Debug.Log("Elapsed Time: " + elapsedTime);
        }else{

        }
        
        if(GameManager.Instance.Health <= 0 && !isCounting){
            SetElapsedTime();
        }
        
    }

    public void SetElapsedTime(){
        GameManager.Instance.svvTime = elapsedTime;
        //Debug.Log($"Time: {GameManager.Instance.svvTime}");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        // int atFirstDmg = GameManager.Instance.Dmg;
        // float atFirstSvvability = GameManager.Instance.Survivability;
    
        Debug.Log($"Scene Loaded:{scene.name}");
        if(scene.name == "NewBorn"){
            EnemySpawner.Instance.ClearActiveEnemies();

        }
        if(scene.name == "SSV" || scene.name == "Campaign_Dark_Broken" || scene.name == "Campaign_Desert" || scene.name == "Campaign_Winter" || scene.name == "Campaign_Swamp"){
            GameManager.Instance.SetStat();
            Player.Instance.PlayerIdle();
            StartCounting();
        }
        Player.Instance.isConsume = true;
        //Debug.Log($"Scene Loaded: {scene.name}");

        switch (scene.name)
        {
            case "NewBorn":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
                // atFirstDmg = GameManager.Instance.Dmg;
                // atFirstSvvability = GameManager.Instance.Survivability;
                break;

            case "BossFight":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.bossFightMusic);
                break;

            case "SVV":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.svv);
                break;
            case "Campaign_Dark_Broken":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campA);
                int dmgRD = Mathf.RoundToInt(GameManager.Instance.Dmg * 0.2f);
                GameManager.Instance.Dmg -= dmgRD;
                break;
            case "Campaign_Desert":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campB);
                break;
            case "Campaign_Winter":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campC);
                GameManager.Instance.Survivability -= 1.5f;
                break;
            case "Campaign_Swamp": 
                AudioManager.Instance.PlayMusic(AudioManager.Instance.campD);
                break;

            default:
                AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
                break;
        }
    }

    void OnSceneUnloaded(Scene scene) {
    
        // if (scene.name == "Campaign_Dark_Broken") {
        //     GameManager.Instance.Dmg = atFirstDmg;
        // }

        // if (scene.name == "Campaign_Winter") {
        // GameManager.Instance.Survivability = atFirstSvvability;
        // }
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
