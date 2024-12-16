using UnityEngine;

public class KingWeaponAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject effectPrefab;
    public float effectDuration = 2f;
    public float bulletSpeed = 10f;
    public float spawnRadius = 0.5f;

    public void SpawnBullets(Vector3 attackPosition)
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomDirection = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = attackPosition + new Vector3(randomDirection.x, randomDirection.y, 0);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 moveDirection = (attackPosition - spawnPosition).normalized * bulletSpeed;
                rb.linearVelocity = moveDirection;
            }
        }
    }

    public void SpawnEffect(Vector3 attackPosition)
    {
        GameObject effect = Instantiate(effectPrefab, attackPosition, Quaternion.identity);
        Destroy(effect, effectDuration);
    }
}
