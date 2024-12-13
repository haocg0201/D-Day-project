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
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public static void DestroyPlayerInstance()
    {
        Instance = null;
        Debug.Log("Singleton instance destroyed.");
    }

    public Boolean isTakeDamage;


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
    private List<GameObject> weapons = new();
    public Transform posLeft;
    public Transform posTopMid;
    public Transform posRight;

    

    List<RaycastHit2D> castCollisions = new();
    Vector2 movementInput;

    Rigidbody2D rb;
    Animator animator;

    bool isRunning;
    bool isWalking;
    bool isExhausted;
    bool isDisarmer = true;
    private float textHeight;
    private Weapon weapon;
    public bool isConsume = false;
    
    // public string playerName;
     public PlayerData playerdata = new ();
    

    


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

    void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();

        if (playerInput.devices.Count > 0)
        {
            if (playerInput.currentControlScheme == null)
            {
                playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            }
        }
        else
        {
            Debug.LogWarning("No devices connected. Unable to switch control scheme.");
            playerInput.SwitchCurrentControlScheme(playerInput.currentControlScheme);
        }

        playerInput.enabled = false;
        playerInput.enabled = true;
    }


    public void EquipWeapon(GameObject newWeapon)
    {
        if(weapons.Count < 3){
            GameObject nowWeapon = Instantiate(newWeapon, GetPos(), Quaternion.identity,transform);
            Vector3 targetWPosition = new(transform.localPosition.x + 0.2f, transform.localPosition.y - 0.1f,transform.localPosition.z);
            nowWeapon.transform.localPosition = targetWPosition;
            weapons.Add(nowWeapon);
            this.weapon = nowWeapon.GetComponent<Weapon>();
            ExcuteToGameManager();
        }      
                
    } 

    public void ExcuteToGameManager(){
        List<Weapon> weaponList = new List<Weapon>();
        foreach(GameObject weapon in weapons){
            Weapon w = weapon.GetComponent<Weapon>();
            if(w != null){
                w.Equip();
                Debug.Log($"Weapon equipped: {w.wName}");
            }   
            weaponList.Add(w);
        }
        GameManager.Instance?.SetWeaponStat(weaponList);
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
        GameObject weaponToRemove = weapons[^1];
        Weapon w = weaponToRemove.GetComponent<Weapon>();// Lấy vũ khí cuối cùng GameObject weaponToRemove = weapons[weapons.Count - 1]
        Destroy(weaponToRemove);                               // Hủy GameObject vũ khí
        //Debug.Log("Đã hủy trang bị vũ khí cuối danh sách.");
        weapons.RemoveAt(weapons.Count - 1); 
        ExcuteToGameManager();
    }



    private IEnumerator WaitingForGameManagerInit(){
        while (GameManager.Instance == null && GameManager.Instance.playerData == null){
            Debug.Log("Waiting for GameManager and PlayerData are init-in...");
            yield return null;
        }
        //Debug.Log("PlayerData loaded successfully.");
        playerData = GameManager.Instance.playerData;
        Initialize(playerData);
    }

    public void ResetInput(){
        var existingInputs = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);

        foreach (var input in existingInputs)
        {
            if (input != this)
            {
                Destroy(input.gameObject); 
            }
        }
    }


    private void Start()
    {
        StartCoroutine(WaitingForGameManagerInit());
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector3(1.2f,1.2f,0);
        if (Player.Instance != null && Player.Instance != this)
        {
            gameObject.SetActive(false);
        }
        // weapons.Add(new GameObject());
        isExhausted = false;
        isRunning = false;
        playerdata = GameManager.Instance.GetPlayerData();
        isTakeDamage = true;
        isConsume = true;
    }

    void Update()
    {
        SByS();
    }

    void SByS(){
        int count = weapons.Count;
        switch(count) {
            case 1 : weapons[0].transform.position = posLeft.transform.position; break;
            case 2 : weapons[0].transform.position = posLeft.transform.position;
                     weapons[1].transform.position = posRight.transform.position; break;
            case 3 : weapons[0].transform.position = posLeft.transform.position;
                    weapons[1].transform.position = posRight.transform.position;
                    weapons[2].transform.position = posTopMid.transform.position; break;
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
        if(!isConsume){
            animator.Play("Idle"); 
            return;
        }
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
            animator.SetBool("isWalking", success);
            
            if (success)
            {
                StartCoroutine(SwitchToRunningAfterDelay(1.5f));
            }
            //Debug.Log($"Movement input - success: {success} and movementInput: {movementInput}");

            timeSincelastshoot += Time.deltaTime;
            Shoot();
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
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

    public void PlayerIdle(){
        animator.Play("Idle");
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
        if(isConsume){
            movementInput = movementValue.Get<Vector2>();
        }else{
            movementInput = Vector2.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isTakeDamage)
        {
            float def = GameManager.Instance.Def;
            float health = GameManager.Instance.Health;

            float damageToPlayer = Mathf.Floor(damage - (damage * (def / 100f)));

            float minDamage = health * 0.03f;

            if (damageToPlayer < minDamage)
            {
                damageToPlayer = Mathf.Ceil(minDamage);
            }

            Debug.Log($"{playerData.username} take {damageToPlayer} damage while def = {def} and dmg input = {damage}");

            if (isExhausted) return;

            UpdateHealth((int)damageToPlayer);
            ShowDamage((int)damageToPlayer);
        }
    }

    public void Recovery(){
        GameManager.Instance.Health = GameManager.Instance.MaxHealth;
        isExhausted = false;
        animator.ResetTrigger("Exhausted");
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

    void UpdateMana(int mn)
    {
        int mana = GameManager.Instance.Mana;
        int maxMana = GameManager.Instance.MaxMana;
        mana += mn;
        if (mana > maxMana){
           mana = maxMana; 
        } 

        if (mana <= 0){
            mana = 0;
        } 
        GameManager.Instance.Mana = mana;
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

    public void GetManaBuff(int mana){
        UpdateMana(mana);
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
