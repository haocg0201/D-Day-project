using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // base stat tại lv1
    [SerializeField] private string playerId;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int def;
    [SerializeField] private int dmg;
    [SerializeField] private float survivability;
    Vector2 movementInput;
    Rigidbody2D rb;

    public string playerName;
    public PlayerData playerData;

    // Phương thức khởi tạo từ PlayerData
    public void Initialize(PlayerData data)
    {
        playerData = data;
        playerId = PlayerPrefsManager.GetPlayerIdFromPlayerPrefs();
        if(playerData != null) 
        {
            Debug.Log("Player initialized with data from PlayerPrefs.");
            SetStat(10);
        }
    }

    // Tải dữ liệu người chơi từ PlayerPrefs khi game bắt đầu
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        Initialize(playerData);
    }

    private void FixedUpdate() {
        if(movementInput != Vector2.zero)
        {
            // Thực hiện di chuyển người chơi

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if(monster != null)
            {
                Debug.Log("Player col with monster.");
                monster.TakeDamage(500,Color.yellow);
            }
        }
    }

    private void OnMovementInout(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    // Ví dụ về phương thức mất máu của người chơi
    public void TakeDamage(int damage)
    {
        Die();
    }

    // Phương thức khi người chơi chết
    private void Die()
    {
        Debug.Log($"{playerData.username} has died.");
        // Thực hiện các hành động khi người chơi chết
    }



    private void SetStat(int lvl)
    {
        int defaultHealth = 100;
        int defaultDef = 10;
        int defaultDmg = 50;
        float defaultSurvivability = 2f;

        // set stat
        health = defaultHealth * lvl;
        maxHealth = health;
        def = defaultDef + 5 * lvl;
        dmg = defaultDmg + 10 * lvl;
        survivability = defaultSurvivability + 0.01f * lvl;

         // phần nâng cấp vũ khí + chỉ số tính sau nhé
    }
}
