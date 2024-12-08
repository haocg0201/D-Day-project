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
    public int typeIndex; // phân loại quái, phân biệt
    public Animator animator;

    public bool isRunning = false;
    public bool isExhausted = false;
    [SerializeField]private float attackRange;
    private bool isOnCooldown = false;
    private bool isDead = false;
    private bool isAttacking = false;
    public float time;

    Transform playerTransform;
    public TextMeshProUGUI damageText;
    Transform damageTextTransform;
    private float textHeight;
    private EnemySpawner enemySpawner;

    public virtual void Initialize(string monsterName, int health, int attackDamage, float survivability, float size, List<string> dialogues, int typeIndex)
    {
        this.monsterName = monsterName;
        this.health = health;
        this.maxHealth = health;
        this.attackDamage = attackDamage;
        this.survivability = survivability;
        this.size = size;
        this.dialogues = dialogues;
        this.typeIndex = typeIndex;
    }

    public virtual void Start()
    {
        Initialize("Nowhere", 10000, 1000, 1f, 1.5f, new List<string>{"?","!!","$*&"},-1);
        playerTransform = Player.Instance.transform;
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(size, size, size);
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        if(damageText != null)
        {
            damageTextTransform = damageText.transform;
        }
        attackRange = 0.3f;
        isRunning = false;
        isAttacking = false;
        time = 1f;
    }

    public virtual void Update()
    {
        if (playerTransform == null || isDead) return;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            MoveTowardsPlayer();
        }

        // if(isAttacking) {
        //     //Debug.Log($"{monsterName} is attacking");
        //     time -= Time.deltaTime;
        // }

        // if(time <= 0){
        //     isAttacking = false;
        //     time = 1f;
        // }
    }

    // public void SetIsAttacking(){
    //     this.isAttacking = true;
    // }

    // public bool GetIsAttacking(){
    //     return this.isAttacking;
    // }

    protected void MoveTowardsPlayer()
    {
        if (isDead) return; // chớt thì thôi không cho nó làm gì nữa

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > attackRange && !isOnCooldown)
        {
            if (!isRunning)
            {
                isRunning = true;
                animator.SetBool("isRunning", isRunning);
            }
            MonsterStarePlayer();     
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                animator.SetBool("isRunning", isRunning);
            }

            AttackPlayer();
        }
    }

    private void MonsterStarePlayer(){
        FacePlayer();
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, survivability * Time.deltaTime);
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }

        
    }


    private IEnumerator AttackCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(1f);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet")){
            int dmg = GameManager.Instance.Dmg;
            TakeDamage(dmg, Color.red);
            StartCoroutine(WaitForSecondsToTakeDame(0.5f));
        }

        if(other.CompareTag("Player") && !isOnCooldown){
            Player.Instance.TakeDamage(attackDamage);
        }
    }

    IEnumerator WaitThenDie(float time){
        yield return new WaitForSeconds(time);
        EnemySpawner.Instance.ReturnEnemy(typeIndex, gameObject);
    }

    IEnumerator WaitForSecondsToTakeDame(float time){
        yield return new WaitForSeconds(time);
    }

    public void Die()
    {
        isDead = true; // Đánh dấu quái đã chết
        animator.SetTrigger("Exhausted");
        StartCoroutine(WaitThenDie(1.5f)); 
    }

    public virtual void SkillConsumption()
    {
        //
    }

    public void TriggerExhaustedState()
    {
        isExhausted = true;
        animator.SetTrigger("Exhausted");
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

    public void ResetMonster()
    {
        health = maxHealth;
        transform.rotation = Quaternion.identity;
    }
}
