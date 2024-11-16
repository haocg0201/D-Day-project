using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public Transform player;
    public List<GameObject> monsters = new List<GameObject>();
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private float spawnInterval = 0.3f;
    private float spawnTimer = 0f;

    void Update()
    {
        if (monsters.Count >= 300) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("Yahalo, Enemy Prefabs list is empty or null!");
            return;
        }

        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        if (randomEnemyPrefab == null)
        {
            Debug.LogWarning("Yahalo, A prefab in the list is null!");
            return;
        }

        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        GameObject spawnedEnemy = Instantiate(randomEnemyPrefab, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        spawnedEnemy.SetActive(true);
        monsters.Add(spawnedEnemy);
    }
}
