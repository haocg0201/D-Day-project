using NUnit.Framework;
using UnityEditor;
using UnityEngine;
public class SpawnSkeleton :MonoBehaviour 
{
    private string mName;
    // Prefabs Skeleton
    // tạo danh sách quản lý quá sinh

    
    public GameObject enemyPrefabs;

    public float timeSpawn, spanwCountdown, amountMax, amountCount;

    
    private void Start()
    {
        Skeleton skeleton = new Skeleton();
        string mNane = skeleton.monsterName;

        spanwCountdown = timeSpawn;
    }

    
    private void Amount()
    {
        // int soLuongSinh = 30; tiêu diệt diệt 1 quái -1 con
        // sinh quái ra
        // sinh xong add vào danh sách để quản lý
        // 
       
        if (spanwCountdown <= 0)
        {
            spanwCountdown = timeSpawn;
            Instantiate(enemyPrefabs, transform.position, transform.rotation);
        }

    }
    private void Update()
    {
        spanwCountdown -= Time.deltaTime;
        
        Amount();
        
    }
}
