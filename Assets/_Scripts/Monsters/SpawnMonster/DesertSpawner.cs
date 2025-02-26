using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertSpawner : MonoBehaviour
{
    public int[] enemyTypes = {2, 4, 7}; // Danh sách các loại quái cần spawn (zom, troll, skeleton)
    private List<GameObject> monsters = new();
    public int maxEnemies = 50;
    public float spawnInterval = 0.5f;
    private float spawnTimer;

    private bool playerInRange = false;

    private void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.isGetQuest && !GameManager.Instance.isQuestDone){
            if (!playerInRange) return;

            if (monsters.Count >= maxEnemies) return;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyTypes.Length);
        Vector2 centerPosition = transform.position;
        float spawnRange = 1f;
        Vector2 spawnPosition = centerPosition + Random.insideUnitCircle * spawnRange;
        GameObject m = EnemySpawner.Instance.GetEnemy(enemyTypes[randomIndex], spawnPosition);
        monsters.Add(m);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
