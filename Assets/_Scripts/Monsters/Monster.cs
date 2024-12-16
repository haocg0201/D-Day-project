using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isRunning;
    public bool isExhausted = false;
    [SerializeField] private float attackRange;
    private bool isOnCooldown;
    private bool isDead = false;
    private bool isAttacking;
    public float time;
    private bool isTakeDame = false;

    Transform playerTransform;
    public TextMeshProUGUI damageText;
    Transform damageTextTransform;
    private float textHeight;
    public Image healthBar;

    [Header("Tỉ lệ tăng/giảm damage")]
    public float fireMultiplier = 1f;
    public float waterMultiplier = 1f;
    public float earthMultiplier = 1f;

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
        Initialize("Nowhere", 10000, 1000, 1f, 1.5f, new List<string> { "?", "!!", "$*&" }, 0);
        if (Player.Instance != null)
        {
            playerTransform = Player.Instance.transform;
        }

        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(size, size, size);
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        if (damageText != null)
        {
            damageTextTransform = damageText.transform;
        }
        attackRange = 0.5f;
        isRunning = false;
        isOnCooldown = false;
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
    }

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
            AudioManager.Instance?.PlaySFX(AudioManager.Instance.enemyAttackSound);
            AttackPlayer();
        }
    }

    private void MonsterStarePlayer()
    {
        FacePlayer();
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, survivability * Time.deltaTime);
    }


    private void FacePlayer()
    {
        GameObject hpChild = transform.GetChild(0).GetChild(1).gameObject;
        if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            damageTextTransform.localScale = new Vector3(-Mathf.Abs(damageTextTransform.localScale.x), damageTextTransform.localScale.y, damageTextTransform.localScale.z);

            if (hpChild != null)
            {
                hpChild.transform.localScale = new Vector3(-Mathf.Abs(hpChild.transform.localScale.x), hpChild.transform.localScale.y, hpChild.transform.localScale.z);
            }
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            damageTextTransform.localScale = new Vector3(Mathf.Abs(damageTextTransform.localScale.x), damageTextTransform.localScale.y, damageTextTransform.localScale.z);

            if (hpChild != null)
            {
                hpChild.transform.localScale = new Vector3(Mathf.Abs(hpChild.transform.localScale.x), hpChild.transform.localScale.y, hpChild.transform.localScale.z);
            }
        }
    }

    protected void AttackPlayer()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            if (isAttacking)
            {
                Player.Instance.TakeDamage(attackDamage);
            }

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

        healthBar.fillAmount = (float)health / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            isTakeDame = true;
            if (isTakeDame){
                int bulletLayer = other.gameObject.layer;
                int baseDamage = GameManager.Instance.Dmg;

                int finalDamage = CalculateDamage(baseDamage, bulletLayer);
                
                Debug.Log($"Monster Type {this.typeIndex} nhận {finalDamage} damage.");
                TakeDamage(finalDamage, Color.red);
                StartCoroutine(WaitForSecondsToTakeDame(0.3f));
            }  
        }

        if (other.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isAttacking = false;
        if (other.CompareTag("Bullet"))
        {
            isTakeDame = false;
        }

    }

    IEnumerator WaitForSecondsToTakeDame(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void Die()
    {
        isDead = true; // Đánh dấu quái đã chết
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.enemyDeathSound);
        animator.SetTrigger("Exhausted");
        StartCoroutine(WaitThenDie(1.5f));
    }

    IEnumerator WaitThenDie(float time)
    {
        yield return new WaitForSeconds(time);
        EnemySpawner.Instance.ReturnEnemy(typeIndex, gameObject);
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
        isDead = false;
        if (maxHealth == 0)
        {
            //Debug.LogError("maxHealth is 0! Cannot reset monster.");
            return;
        }
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
        transform.rotation = Quaternion.identity;
    }

    int CalculateDamage(int baseDamage, int bulletLayer)
    {
        float multiplier = 1f;

        // typeIndex: golem = 0, orc = 1, skeleton = 2, slime = 3, troll = 4, vampire = 5, werewolf = 6, zombie = 7
        switch (this.typeIndex)
        {
            case 0:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 1.5f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;

            case 1:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 0.9f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 1.1f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier * 1.2f;
                break;

            case 2:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 3:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 4:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 5:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 6:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 7:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;
            case 8:
                if (bulletLayer == LayerMask.NameToLayer("Fire"))
                    multiplier = fireMultiplier * 1.2f;
                else if (bulletLayer == LayerMask.NameToLayer("Water"))
                    multiplier = waterMultiplier * 0.8f;
                else if (bulletLayer == LayerMask.NameToLayer("Earth"))
                    multiplier = earthMultiplier;
                break;

            default:
                multiplier = 1f;
                break;
        }

        // Tính damage với multiplier và ngẫu nhiên +/- 20
        int randomVariance = Random.Range(-20, 21);
        int finalDamage = Mathf.RoundToInt(baseDamage * multiplier) + randomVariance;

        return Mathf.Max(finalDamage, 0); // Đảm bảo damage không âm
    }
}
