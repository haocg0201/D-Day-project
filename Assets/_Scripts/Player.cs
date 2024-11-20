using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public TextMeshProUGUI damageText;
    Transform damageTextTransform;
    private float textHeight;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;

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
            SetStat(0);
        }

        damageText = GetComponentInChildren<TextMeshProUGUI>();
        if(damageText != null)
        {
            damageTextTransform = damageText.transform;
        }
    }

    // Tải dữ liệu người chơi từ PlayerPrefs khi game bắt đầu
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        Initialize(playerData);
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
    }

    private void FixedUpdate() {
        // if movement input is not 0, moveeeee
        if (movementInput != Vector2.zero)
        {
            // move
            int count = rb.Cast(
                movementInput, // x-y (1; -1) 
                movementFilter, // cho phép va chạm sảy ra ở đâu, giữa ai với ai XD
                castCollisions, // danh sách collision
                survivability * Time.fixedDeltaTime * collisionOffset
            );
            // if (count == 0)
            // {
            //     rb.MovePosition(rb.position + movementInput * survivability * Time.fixedDeltaTime);
            // }
            rb.MovePosition(rb.position + movementInput * survivability * Time.fixedDeltaTime);
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if(monster != null)
            {
                Debug.Log("Player col with monster.");
                monster.TakeDamage(dmg,Color.yellow);
            }
        }
    }

    void GetDamage(){
        return; // chưa tính
    }

    public void TakeDamage(int damage)
    {
        int damageToPlayer =  (int)Mathf.Floor(damage * (999/(100 + def)));
        UpdateHealth(damageToPlayer);
        ShowDamage(damageToPlayer);
    }

    void UpdateHealth(int damageToPlayer)
    {
        health -=damageToPlayer;
        if (health < 0) health = 0;
        if (health > 0) health = maxHealth;
        if (health <= 0) Die();
    }

    void ShowDamage(int damageToPlayer)
    {
        damageText.color = Color.red;
        damageText.enabled = true;
        damageText.text = "-" + damageToPlayer.ToString();
        StartCoroutine(FadeOutText(0.9f));
    }

    // Phương thức khi người chơi chết
    private void Die()
    {
        Debug.Log($"{playerData.username} has died.");
        // Thực hiện các hành động khi player hẹo
        // ...
    }



    private void SetStat(int lvl)
    {
        int defaultHealth = 100;
        int defaultDef = 10;
        int defaultDmg = 50;
        float defaultSurvivability = 2f;

    if (lvl == 0){
        health = defaultHealth;
        maxHealth = health;
        def = defaultDef;
        dmg = defaultDmg;
        survivability = defaultSurvivability + 3f; // để tạm + 3f cho nhân vật đi nhanh tí nhé
    }

    // phòng trường hợp lỗi hoặc ông nào mode nó âm level
    if(lvl >= 1){
        // set stat
        health = defaultHealth * lvl;
        maxHealth = health;
        def = defaultDef + 5 * lvl;
        dmg = defaultDmg + 10 * lvl;
        survivability = (defaultSurvivability + 0.01f * lvl) + 3f;
    }
        

         // phần nâng cấp vũ khí + chỉ số tính sau nhé
    }

        private IEnumerator FadeOutText(float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = damageText.color;

        while (elapsedTime < duration)
        {
            damageTextTransform.position += Vector3.up * Time.deltaTime; // Text di chuyển lên nèee
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        damageText.enabled = false;
    }
}
