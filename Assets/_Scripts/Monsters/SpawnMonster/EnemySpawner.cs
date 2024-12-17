using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> enemyPrefabs; // Danh sách các loại quái
    [SerializeField]private Dictionary<int, Queue<GameObject>> poolDictionary = new(); // Pool cho từng loại quái
    // typeIndex: golem = 0, orc = 1, skeleton = 2, slime = 3, troll = 4, vampire = 5, werewolf = 6, zombie = 7
    public List<GameObject> ActiveEnemies = new(); // Danh sách quái đang hoạt động

   
    [Header("Vật phẩm có thể rơi ra")]
    public float[] lootDropRates = {0.2f,0.01f,0.01f,0.01f,0.05f,0.01f};
    public List<GameObject> itemPrefabs = new(); // Danh sách các loại vật phẩm có thể rơi ra
    bool isDropItem = false;

    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }

    }


    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    void Start()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            PreloadEnemies(i, 10);
            //Debug.Log($"Preloaded {i} and enemyPrefabs[i].GetComponent<Monster>().typeIndex: {enemyPrefabs[i].GetComponent<Monster>().typeIndex}");
        }
        isDropItem = true;
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

    public void ClearActiveEnemies()
    {
        if (ActiveEnemies == null || ActiveEnemies.Count == 0)
        {
            // Debug.Log("No active enemies to clear.");
            return;
        }

        for (int i = ActiveEnemies.Count - 1; i >= 0; i--)
        {
            var enemy = ActiveEnemies[i];
            if (enemy != null)
            {
                var monster = enemy.GetComponent<Monster>();
                if (monster != null)
                {
                    ReturnEnemy(monster.typeIndex, enemy);
                }
                else
                {
                    // Debug.Log("Enemy missing Monster component.");
                }
            }
            else
            {
                // Debug.Log("ActiveEnemies contains a null reference.");
            }
        }

        ActiveEnemies.Clear();
    }

    // Lấy quái vật từ pool :((
    public GameObject GetEnemy(int typeIndex, Vector3 position)
    {
        // Kiểm tra pool có tồn tại và có quái vật không ở đây nhé
        if (!poolDictionary.ContainsKey(typeIndex) || poolDictionary[typeIndex].Count == 0)
        {
            //Debug.LogWarning($"No enemies available in the pool for typeIndex {typeIndex}. Creating new.");
            
            // Tạo quái vật mới nếu pool trống
            if (typeIndex < 0 || typeIndex >= enemyPrefabs.Count)
            {
                //Debug.LogError($"Invalid typeIndex {typeIndex}. Unable to create enemy.");
                return null;
            }

            GameObject newEnemy = Instantiate(enemyPrefabs[typeIndex]);
            newEnemy.transform.position = position;
            RegisterEnemy(newEnemy); // Đăng ký quái vật mới vào danh sách
            return newEnemy;
        }

        // Lấy quái vật từ pool
        GameObject enemy = poolDictionary[typeIndex].Dequeue();

        if (enemy == null)
        {
            //Debug.LogError($"Enemy in pool is null for typeIndex {typeIndex}. Skipping.");
            return null;
        }

        // Đảm bảo chỉ lấy những quái vật chưa kích hoạt
        if (!enemy.activeSelf)
        {
            enemy.transform.position = position;
            enemy.SetActive(true);

            // Kiểm tra và reset quái vật
            Monster monster = enemy.GetComponent<Monster>();
            if (monster != null)
            {
                monster.ResetMonster();
                //Debug.Log($"Enemy {monster.typeIndex} spawned at {position} and reset successfully.");
            }
            else
            {
                //Debug.LogError($"Monster component is missing on enemy for typeIndex {typeIndex}.");
            }

            RegisterEnemy(enemy); // Đăng ký quái vật đã kích hoạt
            return enemy;
        }
        else
        {
            //Debug.LogWarning($"Enemy {enemy.name} is already active. This shouldn't happen in pool management.");
            return null;
        }
    }

    // Trả quái vật về pool
    public void ReturnEnemy(int typeIndex, GameObject enemy)
    {
        if(isDropItem){
            DropLoot(enemy);
        }

        if(GameManager.Instance != null){
            GameManager.Instance.killCount+=1; // Tăng số lượng quái vật đã giết
        }

        enemy.SetActive(false);
        poolDictionary[typeIndex].Enqueue(enemy);
        ActiveEnemies.Remove(enemy);
    }

    // Đăng ký quái vào danh sách quái đang hoạt động
    public void RegisterEnemy(GameObject enemy)
    {
        ActiveEnemies.Add(enemy);
    }

    void DropLoot(GameObject enemy)
    {
        if (itemPrefabs == null || itemPrefabs.Count == 0) return;

        float randomValue = Random.Range(0f, 1f); // Lấy giá trị ngẫu nhiên từ 0 đến 1
        float cumulativeRate = 0f; // Để lưu trữ tổng xác suất đến thời điểm hiện tại

        for (int i = 0; i < lootDropRates.Length; i++)
        {
            cumulativeRate += lootDropRates[i];

            if (randomValue <= cumulativeRate)
            {
                GameObject itemPrefab = itemPrefabs[i];

                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, enemy.transform.position, Quaternion.identity);
                    //Debug.Log($"Dropped item: {itemPrefab.name}");
                    Destroy(item,10f);
                }
                break;
            }
        }
    }
}
