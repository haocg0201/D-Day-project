using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public int health;
    public int maxHealth;
    public int attackDamage;
    public float survivability;
    public float size;
    public List<string> dialogues; 
    public Animator animator;

    public bool isRunning = true;
    public bool isExhausted = false;
    [SerializeField]private float attackRange = 1f;
    private bool isOnCooldown = false;
    private bool isDead = false;
    Transform playerTransform;
    public TextMeshProUGUI damageText;
    Transform damageTextTransform;
    private float textHeight;
    private EnemySpawner enemySpawner;

    public virtual void Initialize(string monsterName, int health, int attackDamage, float survivability, float size, List<string> dialogues )
    {
        this.monsterName = monsterName;
        this.health = health;
        this.maxHealth = health;
        this.attackDamage = attackDamage;
        this.survivability = survivability;
        this.size = size;
        this.dialogues = dialogues;
    }

    public virtual void Start()
    {
        Initialize("Nowhere", 10000, 1000, 1f, 1.5f, new List<string>{"?","!!","$*&"});
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(size, size, size);
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        if(damageText != null)
        {
            damageTextTransform = damageText.transform;
        }
        attackRange = 0.4f;
    }

    public virtual void Update()
    {
        // if (damageText != null)
        // {
        //     damageTextTransform.position = transform.position + new Vector3(0, textHeight, 0);
        // }

        if (playerTransform != null)
        {
            
            MoveTowardsPlayer();          
        }
    }

    protected void MoveTowardsPlayer()
    {
        if (isDead) return; // Ngừng di chuyển khi quái đã chết rồi nhé ae.
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > attackRange && !isOnCooldown)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.ResetTrigger("Attack");
            }
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
            FacePlayer();
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, survivability * Time.deltaTime);
        }
        else 
        {
            isRunning = false; // cứ để isRunning đó nhé sau để dùng cho boss bro
            AttackPlayer();         
        }
    }

    private void FacePlayer()
    {
        if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            damageTextTransform.localScale = new Vector3(-Mathf.Abs(damageTextTransform.localScale.x), damageTextTransform.localScale.y, damageTextTransform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            damageTextTransform.localScale = new Vector3(Mathf.Abs(damageTextTransform.localScale.x), damageTextTransform.localScale.y, damageTextTransform.localScale.z);
        }
    }

    protected void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        StartCoroutine(AttackCooldown());
        
        

        Player player = playerTransform.GetComponent<Player>();
        player?.TakeDamage(attackDamage);
    }

    private IEnumerator AttackCooldown()
    {
        isOnCooldown = true;
        animator.SetBool("isRunning", isRunning);
        yield return new WaitForSeconds(1.5f);
        isOnCooldown = false;
    }

    public void TakeDamage(int damage, Color color)
    {
        damageText.color = color;
        damageText.enabled = true;
        damageText.text = "-" + damage.ToString();
        StartCoroutine(FadeOutText(0.9f));

        health -= damage;
        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    IEnumerator WaitAndDestroy(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Die()
    {
        isDead = true; // Đánh dấu quái đã chết
        animator.SetTrigger("Exhausted");
        StartCoroutine(WaitAndDestroy(1.5f)); // Đợi 1,5 giây rồi mới xóa quái nhes
    }

    public virtual void SkillConsumption()
    {
        //
    }

    public void TriggerExhaustedState()
    {
        isExhausted = true;
        animator.SetTrigger("Exhausted");
        //StartCoroutine(WaitAndDestroy(1.5f));
        //StartCoroutine(RecoverFromExhaustedState(10f)); // Hồi sinhhhhhh
    }


    // dùng cái này cho boss có pharse 2 nhé
    IEnumerator RecoverFromExhaustedState(float duration)
    {
        yield return new WaitForSeconds(duration);
        isExhausted = false;
        animator.ResetTrigger("Exhausted");
    }

    public virtual string Idle()
    {
        return dialogues.Count > 0 ? dialogues[0] : "The monster is idling";
    }

    public virtual string Run()
    {
        return dialogues.Count > 0 ? dialogues[1] : "The monster is running";
    }

    public virtual string Attack()
    {
        return dialogues.Count > 0 ? dialogues[2] : "The monster attacks";
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
