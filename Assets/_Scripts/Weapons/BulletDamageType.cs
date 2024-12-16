using UnityEngine;

public class BulletDamageType : MonoBehaviour
{
//     public int baseDamage = 0;      
//     public string bulletTag = "Weapon";  // Tag chung cho tất cả viên đạn
//     public float minDamageVariance = 0.8f;  // Biến đổi sát thương tối thiểu
//     public float maxDamageVariance = 1.2f;  // Biến đổi sát thương tối đa

//     void Start()
//     {
//         baseDamage = (int) (GameManager.Instance != null ? GameManager.Instance.Dmg : 1000);
//     }
//     void OnTriggerEnter2D(Collider2D other)
//     {
//          // Kiểm tra nếu va chạm với quái và viên đạn có tag "Weapon"
//         if (other.CompareTag(bulletTag))
//         {
//             // Kiểm tra layer của viên đạn để xác định sát thương
//             int damage = CalculateDamage();

//             // Gọi phương thức nhận sát thương của quái (ví dụ: Enemy)
//             Monster enemy = other.gameObject.GetComponent<Monster>();
//             if (enemy != null)
//             {
//                 enemy.TakeDamage(damage, Color.red);
//             }
//         }
//     }

//     // Phương thức tính toán sát thương dựa vào layer của viên đạn và có độ ngẫu nhiên
//     int CalculateDamage()
//     {
//         // Lấy layer của viên đạn
//         LayerMask bulletLayer = gameObject.layer;

//         int calculatedDamage = 0;

//         // Dựa vào layer để tính sát thương
//         switch (bulletLayer)
//         {
//             case 1:
//             case 2: 
//             case 3:
//             case 4:
//             case 5: 
//             case 6:
//             case 7:
//                 calculatedDamage = (int)baseDamage * 15/10;
//                 break;
//             default: // Đạn thông thường
//                 calculatedDamage = baseDamage;
//                 break;
//         }

//         // Thêm độ ngẫu nhiên vào sát thương (từ minDamageVariance đến maxDamageVariance)
//         float randomVariance = Random.Range(minDamageVariance, maxDamageVariance);
//         int finalDamage = Mathf.RoundToInt(calculatedDamage * randomVariance);

//         return finalDamage;
//     }
// }

// public class Enemy : MonoBehaviour
// {
//     public int health = 100;

//     // Phương thức nhận sát thương
//     public void TakeDamage(int damage)
//     {
//         health -= damage;
//         Debug.Log($"Enemy took {damage} damage, remaining health: {health}");

//         if (health <= 0)
//         {
//             Die();
//         }
//     }

//     // Phương thức chết
//     void Die()
//     {
//         Debug.Log("Enemy has died!");
//         Destroy(gameObject);  // Quái bị hủy khi chết
//     }
}

