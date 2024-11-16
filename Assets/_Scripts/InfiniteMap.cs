using UnityEngine;
using System.Collections.Generic;

public class InfiniteMap : MonoBehaviour
{
    public GameObject mapTilePrefab; // Khu vực map prefab
    public Transform player;
    public int viewDistance = 3; // Số ô map xung quanh được tạo
    private Vector2 playerPos;
    private Dictionary<Vector2, GameObject> spawnedTiles = new Dictionary<Vector2, GameObject>();

    void Update()
    {
        Vector2 newPlayerPos = new Vector2(
            Mathf.Floor(player.position.x / 10),
            Mathf.Floor(player.position.y / 10)
        );

        if (newPlayerPos != playerPos)
        {
            playerPos = newPlayerPos;
            GenerateTiles();
        }
    }

    void GenerateTiles()
    {
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2 tilePos = playerPos + new Vector2(x, y);

                if (!spawnedTiles.ContainsKey(tilePos))
                {
                    Vector3 spawnPos = new Vector3(tilePos.x * 10, tilePos.y * 10, 0);
                    GameObject tile = Instantiate(mapTilePrefab, spawnPos, Quaternion.identity);
                    spawnedTiles[tilePos] = tile;
                }
            }
        }

        // Xóa khu vực map xa khỏi nhân vật
        List<Vector2> tilesToRemove = new List<Vector2>();
        foreach (var tile in spawnedTiles)
        {
            if (Vector2.Distance(playerPos, tile.Key) > viewDistance)
            {
                Destroy(tile.Value);
                tilesToRemove.Add(tile.Key);
            }
        }
        foreach (var tilePos in tilesToRemove)
        {
            spawnedTiles.Remove(tilePos);
        }
    }
}
