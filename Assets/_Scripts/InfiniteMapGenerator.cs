using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapGenerator : MonoBehaviour
{
    public GameObject[] chunkPrefabs;    // Các prefab Tilemap ngẫu nhiên
    Transform gridTransform;      // Grid cha
    public int chunkSize = 20;           // Kích thước mỗi chunk (ví dụ: 10x10)
    Transform player;             // Transform của người chơi

    private Vector2Int currentChunkPos;
    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();

    // Phạm vi giữ các chunk xung quanh người chơi (ví dụ: 3x3 chunk)
    public int viewDistance = 2;

    void Start()
    {
        gridTransform = transform;
        if(Player.Instance != null){
            player = Player.Instance.transform;
        }
        UpdateChunks();
    }

    void Update()
    {
        // Cập nhật khi người chơi di chuyển sang chunk mới
        Vector2Int playerChunkPos = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );

        if (playerChunkPos != currentChunkPos)
        {
            currentChunkPos = playerChunkPos;
            UpdateChunks();
        }
    }

    void UpdateChunks()
    {
        // Spawn các chunk xung quanh người chơi trong phạm vi viewDistance
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int chunkPos = currentChunkPos + new Vector2Int(x, y);
                if (!spawnedChunks.ContainsKey(chunkPos))
                {
                    SpawnRandomChunk(chunkPos);
                }
            }
        }

        // Xóa các chunk nằm ngoài phạm vi viewDistance
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunk in spawnedChunks)
        {
            if (Mathf.Abs(chunk.Key.x - currentChunkPos.x) > viewDistance || Mathf.Abs(chunk.Key.y - currentChunkPos.y) > viewDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkPos in chunksToRemove)
        {
            Destroy(spawnedChunks[chunkPos]);
            spawnedChunks.Remove(chunkPos);
        }
    }

    void SpawnRandomChunk(Vector2Int chunkPos)
    {
        Vector3 worldPosition = new Vector3(chunkPos.x * chunkSize, chunkPos.y * chunkSize, gridTransform.position.z);
        GameObject randomChunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        
        // Instantiate và gắn vào Grid cha
        GameObject newChunk = Instantiate(randomChunk, worldPosition, Quaternion.identity, gridTransform);
        spawnedChunks.Add(chunkPos, newChunk);
        //Debug.Log($"Spawned chunk at: {worldPosition}");
    }
}

