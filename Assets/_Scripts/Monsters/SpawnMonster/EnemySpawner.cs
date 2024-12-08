using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> enemyPrefabs; // Danh sách các loại quái
    [SerializeField]private Dictionary<int, Queue<GameObject>> poolDictionary = new(); // Pool cho từng loại quái
    // typeIndex: golem = 0, orc = 1, skeleton = 2, slime = 3, troll = 4, vampire = 5, werewolf = 6, zombie = 7
    public List<GameObject> ActiveEnemies = new(); // Danh sách quái đang hoạt động


    void Awake()
    {
         if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
         }else Destroy(gameObject);
    }

    void DestroyEnemySpawner(){
        if(Instance != null){
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            PreloadEnemies(i, 10);
            Debug.Log($"Preloaded {i} and enemyPrefabs[i].GetComponent<Monster>().typeIndex: {enemyPrefabs[i].GetComponent<Monster>().typeIndex}");
        }
    }

    // Tạo pool cho từng loại quái
    public void PreloadEnemies(int typeIndex, int count)
    {
        if (!poolDictionary.ContainsKey(typeIndex))
            poolDictionary[typeIndex] = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[typeIndex]);
            enemy.transform.SetParent(transform);
            enemy.SetActive(false);
            poolDictionary[typeIndex].Enqueue(enemy);
        }
    }

    // Lấy quái vật từ pool
    public GameObject GetEnemy(int typeIndex, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(typeIndex) || poolDictionary[typeIndex].Count == 0)
        {
            Debug.LogWarning("No enemies available in the pool! Creating new.");
            GameObject newEnemy = Instantiate(enemyPrefabs[typeIndex]);
            newEnemy.transform.position = position;
            RegisterEnemy(newEnemy);
            return newEnemy;
        }

        GameObject enemy = poolDictionary[typeIndex].Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
        if(enemy != null){
            enemy.GetComponent<Monster>().ResetMonster();
        }else{
            Debug.LogError("Monster component is missing on enemy!");
        }
        
        
        RegisterEnemy(enemy);
        return enemy;
    }

    // Trả quái vật về pool
    public void ReturnEnemy(int typeIndex, GameObject enemy)
    {
        enemy.SetActive(false);
        poolDictionary[typeIndex].Enqueue(enemy);
        ActiveEnemies.Remove(enemy);
    }

    // Đăng ký quái vào danh sách quái đang hoạt động
    public void RegisterEnemy(GameObject enemy)
    {
        ActiveEnemies.Add(enemy);
    }
}
