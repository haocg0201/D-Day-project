using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Info")]
    public string wCode;
    public int wLvl;
    public string wName;
    public float wHealth;
    public float wDef;
    public float wSvvability;
    public String describe;
    PlayerData playerData;
    public GameManager gameManager;
    protected float baseDef;
    protected float baseHealth;
    protected float baseSvvability;
    public float baseLvlUp = 1.5f;
    private bool isEquipped = false;

    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    Transform firePoint;
    Animator animator;
    public float fireRate ; // thời lượng giữ mỗi phát bắn
    public float fireRateAllTheTime;

    protected virtual void Initialize(string wCode, string wName, float baseHealth, float baseDef, float baseSvvability, string baseDescribe, float fireRate)
    {
        this.wCode = wCode;
        this.wName = wName;
        this.baseDef = baseDef;
        this.baseHealth = baseHealth;
        this.baseSvvability = baseSvvability;
        this.fireRate = fireRate;

        wLvl = GetWeaponLevelFromWCode(wCode);
        if(wLvl >= 0){
            CalculateStats(baseHealth, baseDef, baseSvvability);
            describe = string.IsNullOrEmpty(baseDescribe) ? "This weapon comes from nowhere" : baseDescribe;
        }
        else
        {
            SetDefaultStats();
        }
    }

    public void UpdateStat(int lvl)
    {
        this.wLvl = lvl; 
        CalculateStats(baseHealth, baseDef, baseSvvability); 
        
    }

    private void CalculateStats(float baseHealth, float baseDef, float baseSvvability)
    {
        wHealth = baseHealth * wLvl * baseLvlUp;
        wDef = baseDef * wLvl * baseLvlUp;
        wSvvability = wLvl == 0? 0 : baseSvvability;
        if(wLvl >= 5){
            wHealth += 25;
            wDef += 25;
        }

        if(wLvl == 5){
            wHealth += 50;
            wDef += 50;
        }
    }

    private int GetWeaponLevelFromWCode(string wCode)
    {
        return wCode switch
        {
            "sword" => playerData.weapon.sword,
            "knife" => playerData.weapon.knife,
            "boxingGloves" => playerData.weapon.boxingGloves,
            "pistol" => playerData.weapon.pistol,
            "akm" => playerData.weapon.akm,
            "ordinaryStick" => playerData.weapon.ordinaryStick,
            "yoyo" => playerData.weapon.yoyo,
            _ => 0,
        };
    }

    private void SetDefaultStats()
    {
        wLvl = 0;
        wHealth = 0;
        wDef = 0;
        wSvvability = 0;
        wName = "Unknown";
        wCode = "404";
        describe = "No data available";
    }
    public virtual void Start()
    {   
        playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        Initialize("Unknown", "Unknown", 0, 0, 0,"",1f);// Debug.Log("Weapon: " + wName + " Health: " + wHealth + " Def: " + wDef + " Svvability: " + wSvvability);
        firePoint = GetComponentInChildren<Transform>();
        animator = GetComponent<Animator>();
        fireRateAllTheTime = fireRate;
    }

    public virtual void Update()
    {
        if(Player.Instance != null && Player.Instance.isConsume){
            if(isEquipped){
                AttackRotateWeaponTowardsMouse(); 
                fireRate -= Time.deltaTime;
                if(Input.GetMouseButton(0) && fireRate < 0){
                    StartCoroutine(ThrowTheWeapon());      
                }

                if(Input.GetMouseButtonUp(0)){
                    if(Player.Instance.isConsume){
                        if(wCode == "akm" || wCode == "pistol"){
                        AudioManager.Instance?.PlaySFX(AudioManager.Instance.bulletSound);
                        }else{
                            AudioManager.Instance?.PlaySFX(AudioManager.Instance.weaponShootSound);
                        }
                    }
                    animator.SetBool("isShooting", false);
                }
            }
        }  
    }

    public int GetLvl(){
        return this.wLvl;
    }

    protected virtual void AttackRotateWeaponTowardsMouse(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lockDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(lockDirection.y, lockDirection.x) * Mathf.Rad2Deg;

        Quaternion quaternion = Quaternion.Euler(new Vector3(0,0,angle)); // quay truc z thoi nhe
        transform.rotation = quaternion;

        if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90){
            transform.localScale = new Vector3(1,-1, 0);
        }else{
            transform.localScale = new Vector3(1, 1, 0);
        }

    }
    public virtual IEnumerator ThrowTheWeapon()
    {
        // Lấy vị trí chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Bỏ trục Z vì game 2D

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = Vector3.MoveTowards(transform.position, transform.position * (-0.2f), Time.deltaTime * 10f * fireRateAllTheTime);
        yield return null;

        // Spawn ra viên đạn
        SpawnBullet(direction);

        

    }

    protected virtual void SpawnBullet(Vector3 direction)
    {
        animator.SetBool("isShooting", true);
        FireBullet();
    }

    void FireBullet(){
        fireRate = fireRateAllTheTime;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.rotation = transform.rotation;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    public void Equip(){
        isEquipped = true;
    }

    public void UnEquip(){
        isEquipped = false;
    }



    // đây là xoay trục z nhé, sủa lại sang trục mn muốn là được
    // public IEnumerator ThrowAndSpinTheWeapon(){
    //     Vector3 targetPosition = transform.localPosition + transform.right * 0.2f; // niem luc dk vu khi di

    //     // di chuyen vk toi cho muc tieu
    //     float elapsedTime = 0f;
    //     float duration = 0.5f; // quang thoi gian nay de di chuyen toi vi tri da cai nhe
    //     while (elapsedTime < duration){
    //         transform.localPosition = Vector3.Lerp(originalPos, targetPosition, elapsedTime / duration);
    //         elapsedTime += Time.deltaTime;  
    //         yield return null;
    //     }

    //     transform.localPosition = targetPosition;

    //     // xoay vk ne
    //     elapsedTime = 0f;
    //     duration = 1f;
    //     while (elapsedTime < duration)
    //     {
    //         transform.Rotate(Vector3.forward, 360 * Time.deltaTime / duration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg));

    //     // ve vi tri ban dau
    //     elapsedTime = 0f;
    //     duration = 0.5f;
    //     while (elapsedTime < duration)
    //     {
    //         transform.localPosition = Vector3.Lerp(targetPosition, originalPos, elapsedTime / duration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     } 

    //     transform.localPosition = originalPos;

    //     // dat lai goc quay ban dau
    //     transform.rotation = Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(originalPos.y, originalPos.x) * Mathf.Rad2Deg));
    // }

    private void WeaponBehaivours(){
        // bỏ
    }

}
