using NUnit.Framework;
using UnityEditor;
using UnityEngine;
public class SpawnSkeleton :MonoBehaviour 
{
    private string mName;
    // Prefabs Skeleton
    // tạo danh sách quản lý quá sinh

    
    public GameObject enemyPrefabs;
    public int maxEnemy = 3;
    private int currentEnemyCount = 0;

    public float timeSpawn, spanwCountdown;

    public Transform[] spawnPoints;

    
    private void Start()
    {
        Skeleton skeleton = new Skeleton();
        string mNane = skeleton.monsterName;

        spanwCountdown = timeSpawn;
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            Amount();
        }
    }
    
    private void Amount()
    {
        // int soLuongSinh = 30; tiêu diệt diệt 1 quái -1 con
        // sinh quái ra
        // sinh xong add vào danh sách để quản lý
        // 
       
        if (spanwCountdown <= 0)
        {
            if (currentEnemyCount >= maxEnemy) return;

            spanwCountdown = timeSpawn;
            
            Instantiate(enemyPrefabs, transform.position, transform.rotation);
            currentEnemyCount++;
        }

    }
    private void Update()
    {
        spanwCountdown -= Time.deltaTime;
        
        if (currentEnemyCount < maxEnemy)
        {
            Amount();
        }
        
    }
}
