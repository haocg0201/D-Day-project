using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }  
    }

    public static void DestroyPlayerInstance()
    {
        Instance = null;
        Debug.Log("Singleton instance destroyed.");
    }

    public string playerName;
    public PlayerData playerData;
    public GameObject currentWeaponLeft;
    public GameObject currentWeaponMidTop;
    public GameObject currentWeaponRight;

    [Header("UI & Effects")]
    public TextMeshProUGUI damageText;
    public Transform damageTextTransform;
    public float collisionOffset = 1f;
    public ContactFilter2D movementFilter;
    private List<GameObject> weapons = new List<GameObject>();
    public Transform posLeft;
    public Transform posTopMid;
    public Transform posRight;

    

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Vector2 movementInput;

    Rigidbody2D rb;
    Animator animator;

    bool isRunning;
    bool isWalking;
    bool isExhausted;
    bool isDisarmer = true;
    private float textHeight;
    private Weapon weapon;
    
    public string playerName;
    public PlayerData playerData;
    

    private bool isImmune = false;
    private bool isGospel = false;



    public GameObject Effect_Immortal, Effect_Gospel;
    public Transform attactPoint;
    private GameObject instantiatedObject; //luu tru doi tuong da instance
    private bool isCooldown = false;




    [Header("Bullet")]
    public GameObject bullet;
    public Transform bulletPos;
    public float cooldownshoot = 5f;
    private float timeSincelastshoot;

    // Phương thức khởi tạo từ PlayerData
    public void Initialize(PlayerData data)
    {
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        if(damageText != null)
        {
            damageTextTransform = damageText.transform;
        }
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        if(weapons.Count <= 3){
            GameObject nowWeapon = Instantiate(newWeapon, GetPos(), Quaternion.identity,transform);
            Vector3 targetWPosition = new(transform.localPosition.x + 0.2f, transform.localPosition.y - 0.1f,transform.localPosition.z);
            nowWeapon.transform.localPosition = targetWPosition;
            weapons.Add(nowWeapon);
            this.weapon = nowWeapon.GetComponent<Weapon>();
            foreach(GameObject weapon in weapons){
                Weapon w = weapon.GetComponent<Weapon>();
                if(w != null){
                    w.Equip();
                    Debug.Log($"Weapon equipped: {w.wName}");
                }             
            }
        }
        
    }

    public Vector3 GetPos(){
        int count = weapons.Count;
        return count switch{
            1 => posLeft.transform.localPosition,
            2 => posRight.transform.localPosition,
            3 => posLeft.transform.localPosition,
            _ => new Vector2(0,0)
        };
    }
    public void UnEquipWeapon()
    {
        if (weapons.Count > 1)
        {
            GameObject weaponToRemove = weapons[^1]; // Lấy vũ khí cuối cùng GameObject weaponToRemove = weapons[weapons.Count - 1]
            weapons.RemoveAt(weapons.Count - 1);                   // Xóa khỏi danh sách
            Destroy(weaponToRemove);                               // Hủy GameObject vũ khí
            Debug.Log("Đã hủy trang bị vũ khí cuối danh sách.");
        }
        else
        {
            //Debug.LogWarning("Không còn vũ khí nào để hủy trang bị.");
            return;
        }
    }

    private IEnumerator WaitingForGameManagerInit(){
        while (GameManager.Instance == null && GameManager.Instance.playerData == null){
            Debug.Log("Waiting for GameManager and PlayerData are init-in...");
            yield return null;
        }
        Debug.Log("PlayerData loaded successfully.");
        playerData = GameManager.Instance.playerData;
        Initialize(playerData);
    }


    private void Start()
    {
        StartCoroutine(WaitingForGameManagerInit());
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(1.2f,1.2f,0);
        weapons.Add(new GameObject());
        isExhausted = false;
        isRunning = false;
    }

    void Update()
    {
        SByS();
    }

    void SByS(){
        int count = weapons.Count;
        switch(count) {
            case 2 : weapons[1].transform.position = posLeft.transform.position; break;
            case 3 : weapons[1].transform.position = posLeft.transform.position;
                     weapons[2].transform.position = posRight.transform.position; break;
            case 4 : weapons[1].transform.position = posLeft.transform.position;
                    weapons[2].transform.position = posRight.transform.position;
                    weapons[3].transform.position = posTopMid.transform.position; break;
            default: break;
        }
    }

    private void FixedUpdate() {
        if (isExhausted) return;
        HandleMovement();
        // // Kiểm tra nếu Player đang bị dính vào Tilemap :v thế quái nào nhân vật chạm vào cái gì là nó bị dính vào với ní luông
        // Collider2D overlapCollider = Physics2D.OverlapCircle(transform.position, 0.1f, movementFilter.layerMask);
        // if (overlapCollider != null)
        // {
        //     //Debug.Log("Mắc map, đẩy nó ra nè");
        //     Vector2 pushDirection = (rb.position - (Vector2)overlapCollider.transform.position).normalized;
        //     rb.MovePosition(rb.position + pushDirection * 0.1f);
        // } 
    }

private void HandleMovement()
{
    if (movementInput != Vector2.zero)
    {
        bool success = TryMove(movementInput);

        if (!success)
        {
            success = TryMove(new Vector2(movementInput.x, 0)); 
        }

        if (!success)
        {
            success = TryMove(new Vector2(0, movementInput.y));
        }

        if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            if (!isImmune)
            {
                StartCoroutine(ActivateImmunity());
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && !isCooldown) 
        {
            if (!isGospel)
            {
                StartCoroutine(ActivateGospel());
            }
        }
        

        timeSincelastshoot += Time.deltaTime;
        Shoot();
    }
    // Skill_Ready to fight
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.J) && timeSincelastshoot >= cooldownshoot)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity, transform);
            timeSincelastshoot = 0f;
        }
    }

    

    IEnumerator ActivateImmunity()
    {
        isImmune = true; // Bật chế độ miễn nhiễm
        
        if (Effect_Immortal != null && attactPoint != null)
        {
            instantiatedObject = Instantiate(Effect_Immortal, attactPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(attactPoint);

            instantiatedObject.transform.position = attactPoint.position;
        }
        Debug.Log("Player is immune to damage for 5 seconds!");

        // Đợi trong 5 giây
        yield return new WaitForSeconds(5f);

        Debug.Log("Player is no longer immune to damage,Cooldown in 10 seconds!");
        isImmune = false; // Tắt chế độ miễn nhiễm sau 5 giây
        isCooldown = true;
        Destroy(instantiatedObject);
        yield return new WaitForSeconds(10f);
        isCooldown = false;
    }

    IEnumerator ActivateGospel()
    {
        isGospel = true; // Bật phúc âm
        survivability += 1f;
        if (Effect_Gospel != null && attactPoint != null)
        {
            instantiatedObject = Instantiate(Effect_Gospel, attactPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(attactPoint);

            instantiatedObject.transform.position = attactPoint.position;
        }
        Debug.Log("Survivability +1 for 30 seconds!");

        // Đợi trong 30 giây
        yield return new WaitForSeconds(5f);

        Debug.Log("Gospel expires.Cooldown in 10 seconds!");
        isGospel = false; // Tắt phúc âm sau 30 giây
        isCooldown = true;
        survivability -= 1f;
        Destroy(instantiatedObject);
        yield return new WaitForSeconds(10f);
        isCooldown = false;
    }

        animator.SetBool("isWalking", success);
        if (success)
        {
            StartCoroutine(SwitchToRunningAfterDelay(1.5f));
        }
        //Debug.Log($"Movement input - success: {success} and movementInput: {movementInput}");
    }
    else
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
}


    void StartWalking()
    {
        if (!isWalking)
        {
            isWalking = true;
            animator.SetBool("isWalking", true);

            StartCoroutine(SwitchToRunningAfterDelay(2f));
        }
    }

    private IEnumerator SwitchToRunningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isWalking = false;
        isRunning = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
    }

    private bool TryMove(Vector2 direction)
    {
        if(direction != Vector2.zero){
            float svvability = GameManager.Instance.Survivability;

            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                svvability * Time.fixedDeltaTime * collisionOffset
            );

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * svvability * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }else{
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void TakeDamage(int damage)
    {
        float def = GameManager.Instance.Def;
        float damageToPlayer =  (int)Mathf.Floor(damage - (damage * (def/100f)));
        if(damageToPlayer <= 0) damageToPlayer = 5; // sát thương ở mức tối thiểu
        Debug.Log($"{playerData.username} take {damageToPlayer} damage while def = {def} and dmg input = {damage}");
        if(isExhausted) return;
        UpdateHealth((int)damageToPlayer);
        ShowDamage((int)damageToPlayer);
        if (isImmune)
        {
            Debug.Log("Player is immune, no damage taken!");
            return;
        }
        int damageToPlayer =  (int)Mathf.Floor(damage * (999/(100 + def)));
        UpdateHealth(damageToPlayer);
        ShowDamage(damageToPlayer);
    }

    void UpdateHealth(int damageToPlayer)
    {
        int health = GameManager.Instance.Health;
        int maxHealth = GameManager.Instance.MaxHealth;
        health -=damageToPlayer;
        animator.SetTrigger("Hurt");

        if (health > maxHealth){
           health = maxHealth; 
        } 

        if (health <= 0){
            health = 0;
            Die();
        } 
        GameManager.Instance.Health = health;
//        if (health > 0 && !isExhausted) BackToBattle();GameManager.Instance.Health = health; // logic sai, tạm thời chưa dùng @@
    }

    void ShowDamage(int damageToPlayer)
    {
        damageText.color = Color.red;
        damageText.enabled = true;
        damageText.text = "-" + damageToPlayer.ToString();
        StartCoroutine(FadeOutText(2f));
    }

    private void Die()
    {
        animator.SetTrigger("Exhausted");
        isExhausted = true;
        RewardUICtrller rewardUICtrller = WorldWhisperManager.Instance.GetComponent<RewardUICtrller>();
        rewardUICtrller.rewardPanel.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }

    public void BackToBattle(){
        isExhausted = true;
        //
    }

    public void GetHealBuff(int heal){
        UpdateHealth(-heal);
        //if (isGospel)
        //{
        //    survivability += 1f;
        //    Debug.Log("Survivability +1 for 30 seconds!");
        //}


        // phần nâng cấp vũ khí + chỉ số tính sau nhé
    }

    private IEnumerator FadeOutText(float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = damageText.color;

        while (elapsedTime < duration)
        {
            damageTextTransform.position += Vector3.up * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        damageText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Disarmer")) {
            if(isDisarmer){
                UnEquipWeapon();
                
                StartCoroutine(HandleDisarmer());
            }
        }
    }

    private IEnumerator HandleDisarmer() {
        isDisarmer = false;
        yield return new WaitForSeconds(2f); 
        isDisarmer = true;
    }
}
