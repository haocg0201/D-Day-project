using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private const string SVVMAP = "SSV"; // ssv
    [SerializeField] private const string CAMA = "Campaign_Desert"; // ds
    [SerializeField] private const string CAMB = "Campaign_Dark_Broken"; // d br k
    [SerializeField] private const string CAMC = "Campaign_Ice_Winter"; // 
    [SerializeField] private const string CAMD = "Campaign_Swamp"; //
    [SerializeField] private const string DUNGEON = "0"; //
    [SerializeField] private Transform spawnPointCampA; //
    [SerializeField] private Transform spawnPointCampB;
    [SerializeField] private Transform spawnPointCampC;
    [SerializeField] private Transform spawnPointCampD;
    [SerializeField] private Transform spawnPointDungeon;
    [SerializeField] private Transform spawnPointSvv;

    void Start()
    {
        spawnPointCampA = transform;
        spawnPointCampB = transform;
        spawnPointCampC = transform;
        spawnPointCampD = transform;
        spawnPointDungeon = transform;
        spawnPointSvv = transform;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            string sceneIndex = GetScene(gameObject.tag);
            Vector3 spawnHere = SetSpawnPoint(gameObject.tag);
            if (sceneIndex != "0"){
                StartCoroutine(LoadSceneAndSetPlayerSpawnPoint(sceneIndex, spawnHere));
            }else{
                // Map chưa sẵn sàng
            }
        }
    }

private IEnumerator LoadSceneAndSetPlayerSpawnPoint(string sceneName, Vector3 spawnHere)
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
    while (!asyncLoad.isDone)
    {
        yield return null; 
    }

    //Debug.Log($"Scene {sceneIndex} loaded.");
    if (Player.Instance != null)
    {
        Player.Instance.transform.position = spawnHere;
        Debug.Log($"Player moved to {spawnHere}");
    }
    else
    {
        Debug.LogError("Player.Instance is null!");
    }
}

    private string GetScene(string tag){
        return tag switch
        {
            "CampA" => CAMA,
            "CampB" => CAMB,
            "CampC" => CAMC,
            "CampD" => CAMD,
            "Dungeon" => DUNGEON,
            "Svv" => SVVMAP,
            _ => "0"
        };
    } 

    private Vector3 SetSpawnPoint(string tag){
        return tag switch
        {
            "CampA" => spawnPointCampA.position = new Vector3(0,0,0),
            "CampB" => spawnPointCampB.position = new Vector3(0,0,0),
            "CampC" => spawnPointCampC.position = new Vector3(0,0,0),
            "CampD" => spawnPointCampD.position = new Vector3(0,0,0),
            "Dungeon" => spawnPointDungeon.position = new Vector3(0,0,0),
            "Svv" => spawnPointSvv.position = new Vector3(0,0,0),
            _ => new Vector3(0,0,0),
        };
    }
}
