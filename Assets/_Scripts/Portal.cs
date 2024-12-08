using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private const int SVVMAP = 7; // ssv
    [SerializeField] private const int CAMA = 5; // ds
    [SerializeField] private const int CAMB = 6; // d br k
    [SerializeField] private const int CAMC = -1; // 
    [SerializeField] private const int CAMD = -1; //
    [SerializeField] private const int DUNGEON = -1; //
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
            int sceneIndex = GetSceneIndex(gameObject.tag);
            Vector3 spawnHere = SetSpawnPoint(gameObject.tag);
            if (sceneIndex != -1){
                StartCoroutine(LoadSceneAndSetPlayerSpawnPoint(sceneIndex, spawnHere));
            }else{
                // Map chưa sẵn sàng
            }
        }
    }

private IEnumerator LoadSceneAndSetPlayerSpawnPoint(int sceneIndex, Vector3 spawnHere)
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
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

    private int GetSceneIndex(string tag){
        return tag switch
        {
            "CampA" => CAMA,
            "CampB" => CAMB,
            "CampC" => CAMC,
            "CampD" => CAMD,
            "Dungeon" => DUNGEON,
            "Svv" => SVVMAP,
            _ => -1
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
