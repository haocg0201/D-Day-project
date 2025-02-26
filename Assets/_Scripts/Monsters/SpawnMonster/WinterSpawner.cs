using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterSpawner : MonoBehaviour
{
    public GameObject[] spawnGates;
    public int[] enemyTypes = { 0, 1, 6 };
    private List<GameObject> monsters = new();
    public int maxEnemies = 111;
    public int spawnPerGate = 10;        
    public float spawnInterval = 0.5f;   
    public float gateSpawnDelay = 5f;  

    private bool playerInRange = false;
    private int currentGateIndex = 0;    
    private bool isSpawning = false; 

    void OnDisable()
    {
        monsters.Clear();
    }  

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGetQuest && !GameManager.Instance.isQuestDone)
        {
            if (monsters.Count >= maxEnemies) return; //!playerInRange || 

            if (!isSpawning)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }

    private IEnumerator SpawnWave()
    {

        if (monsters.Count >= maxEnemies) gameObject.SetActive(false);
        isSpawning = true;
        for (int i = 0; i < spawnPerGate; i++)
        {
            if (monsters.Count >= maxEnemies) break;
            Vector2 spawnPosition = spawnGates[currentGateIndex].transform.position;
            GameObject m = EnemySpawner.Instance.GetEnemy(enemyTypes[currentGateIndex], spawnPosition);
            monsters.Add(m);

            yield return new WaitForSeconds(spawnInterval);
        }
        yield return new WaitForSeconds(gateSpawnDelay);
        // Chuyển sang cổng tiếp theo nhé @@ đoạn này tôi xem video 
        currentGateIndex = (currentGateIndex + 1) % spawnGates.Length;
        isSpawning = false;
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
