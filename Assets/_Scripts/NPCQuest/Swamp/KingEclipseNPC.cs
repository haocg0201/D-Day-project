using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KingEclipseNPC : MonoBehaviour
{
    public string monsterName;
    public int health;
    public int maxHealth;
    public int attackDamage;
    public float survivability;
    public float size;
    public List<string> dialogues; 
    public int typeIndex; // phân loại quái, phân biệt
    Animator animator;
    public bool isFlipped = false;

    public bool isWalking = false;
    public bool isAttack = false;
    public bool isExhausted = false;
    private bool isDead = false;
    public bool isEnrage = false;
    public Vector3 attackOffset;
    public float attackRange = 11f;
    public LayerMask attackMask;

    Transform player;
    KingWeaponAttack kingWeapon;


    // public TextMeshProUGUI damageText;
    // Transform damageTextTransform;
    // private float textHeight;
    // public Image healthBar;

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
        Initialize("King of Eclipse", 150000, 1000, 3f, 5f, new List<string>{"?","!!","$*&"},0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackMask = LayerMask.GetMask("Player");
        attackOffset = new Vector3(3f, -1f, 0);
        kingWeapon = GetComponent<KingWeaponAttack>();
    }

    public void FacePlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;

        if (player.position.x < transform.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }else if( transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
        UpdateAttackOffset();
    }

    public void UpdateAttackOffset()
    {
        attackOffset.x = isFlipped ? Mathf.Abs(attackOffset.x) : -Mathf.Abs(attackOffset.x);
    }

    // public void FacePlayer()
    // {
    //     if (player.position.x < transform.position.x && !isFlipped)
    //     {
    //         // Quay mặt sang trái
    //         transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //         isFlipped = true;
    //     }
    //     else if (player.position.x > transform.position.x && isFlipped)
    //     {
    //         // Quay mặt sang phải
    //         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //         isFlipped = false;
    //     }
    // }



    public void AttackPlayer()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange , attackMask);
        if(colInfo != null){
            Player.Instance.TakeDamage(attackDamage);
        }      
        Debug.DrawLine(transform.position, transform.position + attackOffset, Color.red,1f);

    }

    public void AttackSkill1Pharse1()
    {
        Vector3 pos = transform.position;
        float offsetX = isFlipped ? -attackOffset.x : attackOffset.x;
        pos += transform.right * offsetX;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            kingWeapon.SpawnEffect(pos);
            Player.Instance.TakeDamage(attackDamage);
        }

        Debug.DrawLine(transform.position, pos, Color.red, 1f);
    }

    public void AttackSkill1Pharse2()
    {
        Vector3 pos = transform.position;
        float offsetX = isFlipped ? -attackOffset.x : attackOffset.x;
        pos += transform.right * offsetX;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            kingWeapon.SpawnBullets(pos);
            Player.Instance.TakeDamage(attackDamage);
        }

        Debug.DrawLine(transform.position, pos, Color.red, 1f);
    }


    // private IEnumerator AttackCooldown()
    // {
    //     isOnCooldown = true;
    //     yield return new WaitForSeconds(1f);
    //     isOnCooldown = false;

    // }

    // public void TakeDamage(int damage, Color color)
    // {
    //     damageText.color = color;
    //     damageText.enabled = true;
    //     damageText.text = "-" + damage.ToString();
    //     StartCoroutine(FadeOutText(0.9f));

    //     health -= damage;
    //     if (health < 0) health = 0;
    //     if (health > maxHealth) health = maxHealth;

    //     if (health <= 0 && !isDead)
    //     {
    //         Die();
    //     }

    //     healthBar.fillAmount = (float)health / maxHealth;
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.CompareTag("Bullet")){
    //         int dmg = GameManager.Instance.Dmg;
    //         TakeDamage(dmg, Color.red);
    //         StartCoroutine(WaitForSecondsToTakeDame(0.5f));
    //     }

    //     if(other.CompareTag("Player")){
    //         isAttacking = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     isAttacking = false;
    // }

    // IEnumerator WaitForSecondsToTakeDame(float time){
    //     yield return new WaitForSeconds(time);
    // }

    // public void Die()
    // {
    //     isDead = true; // Đánh dấu quái đã chết
    //     animator.SetTrigger("Exhausted");
    //     StartCoroutine(WaitThenDie(1.5f)); 
    // }

    // IEnumerator WaitThenDie(float time){
    //     yield return new WaitForSeconds(time);
    //     EnemySpawner.Instance.ReturnEnemy(typeIndex, gameObject);
    // }

    // public virtual void SkillConsumption()
    // {
    //     //
    // }

    // public void TriggerExhaustedState()
    // {
    //     isExhausted = true;
    //     animator.SetTrigger("Exhausted");
    // }


    // // dùng cái này cho boss có pharse 2 nhé
    // IEnumerator RecoverFromExhaustedState(float duration)
    // {
    //     yield return new WaitForSeconds(duration);
    //     isExhausted = false;
    //     animator.ResetTrigger("Exhausted");
    // }

    // public virtual string Idle()
    // {
    //     return dialogues.Count > 0 ? dialogues[0] : "The monster is idling";
    // }

    // public virtual string Run()
    // {
    //     return dialogues.Count > 0 ? dialogues[1] : "The monster is running";
    // }

    // public virtual string Attack()
    // {
    //     return dialogues.Count > 0 ? dialogues[2] : "The monster attacks";
    // }

    // private IEnumerator FadeOutText(float duration)
    // {
    //     float elapsedTime = 0f;
    //     Color originalColor = damageText.color;

    //     while (elapsedTime < duration)
    //     {
    //         damageTextTransform.position += Vector3.up * Time.deltaTime; // Text di chuyển lên nèee
    //         elapsedTime += Time.deltaTime;
    //         float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
    //         damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    //         yield return null;
    //     }

    //     damageText.enabled = false;
    // }

    // public void ResetMonster()
    // {
    //     isDead = false;
    //     if (maxHealth == 0)
    //     {
    //         //Debug.LogError("maxHealth is 0! Cannot reset monster.");
    //         return;
    //     }
    //     health = maxHealth;
    //     healthBar.fillAmount = health / maxHealth;
    //     transform.rotation = Quaternion.identity;
    // }
}
