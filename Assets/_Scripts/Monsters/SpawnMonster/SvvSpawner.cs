using UnityEngine;

public class SvvSpawner : MonoBehaviour
{
    public int[] enemyTypes = {0,1,2,3,4,5,6,7}; // Danh sách các loại quái cần spawn
    public int maxEnemies = 50; // Số lượng quái tối đa trong map
    public float spawnInterval = 0.3f; // Thời gian giữa các lần spawn
    private float spawnTimer;

    private void Update()
    {
        if (EnemySpawner.Instance.ActiveEnemies.Count >= maxEnemies) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyTypes.Length);

        Vector2 centerPosition = Player.Instance.transform.position;
        float spawnRange = 10f;

        Vector2 spawnPosition = centerPosition + Random.insideUnitCircle * spawnRange;
        EnemySpawner.Instance.GetEnemy(enemyTypes[randomIndex], spawnPosition);
    }
}
