using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private string playerId;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int def;
    [SerializeField] private int dmg;
    [SerializeField] private float survivability;
    public static GameManager Instance { get; private set; } // singleton
    public PlayerData playerData;
    public List<GameObject> monsters = new();

    // Check var mapppp
    public int killCount = 0;
    public float svvTime = 0f;
    public int _mgCounter = 0;
    public int _rgCounter = 0;

    private void Awake()
    {
        // singleton pattern để đảm bảo rằng là chỉ có duy nhất một GameManager nhé
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Mới xóa kìa");
        } 
    }

    public void InitPlayerData()
    {
        playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
            if (playerData != null){
                SetStat(playerData.stat.level);
                Debug.Log($"un: {playerData.username}, lvl: {playerData.stat.level} khi khởi tạo");
            }else{
                Debug.Log("Không tải đc dữ liệu :v");
            }   
        if (playerData == null)
        { 
        }else{
           Debug.Log($"un: {playerData.username}, lvl: {playerData.stat.level} khi player đã tồn tại"); 
        }
        
    }

    private void SetStat(int lvl)
    {
        int defaultHealth = 100;
        int defaultDef = 10;
        int defaultDmg = 50;
        float defaultSurvivability = 2f;

        if (lvl <= 0){
            lvl = 1;
        }

        if(lvl >= 1){
            // set stat
            this.health = defaultHealth * lvl;
            this.def = defaultDef + 5 * lvl;
            this.dmg = defaultDmg + 10 * lvl;
            this.survivability = (defaultSurvivability + 0.01f * lvl) + 3f;
        }
         // phần nâng cấp vũ khí + chỉ số tính sau nhé
        this.maxHealth = health;
    }

    void Start()
    {
        Debug.Log("GameManager Start");
        InitPlayerData();
        LoadGameData();
    }
    
    private void Update() {
        
    }
    public void LoadGameData(){}
    
    public void StartGame(){}    
    public void PauseGame(bool time){
        Time.timeScale = time ? 0 : 1;
    }
    public void ResumeGame(){}
    public void EndGame(){}
    private void UpdateUI(){}
    public void ResetTheCounter(){
        killCount = 0;
        svvTime = 0f;
        _mgCounter = 0;
        _rgCounter = 0;
    }



    public void UpdateWeaponLevel(string weaponCode, int newLevel)
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData không tồn tại. Không thể cập nhật cấp độ vũ khí.");
            return;
        }

        switch (weaponCode)
        {
            case "sword":
                playerData.weapon.sword = newLevel;
                break;
            case "knife":
                playerData.weapon.knife = newLevel;
                break;
            case "boxingGloves":
                playerData.weapon.boxingGloves = newLevel;
                break;
            case "pistol":
                playerData.weapon.pistol = newLevel;
                break;
            case "akm":
                playerData.weapon.akm = newLevel;
                break;
            case "ordinaryStick":
                playerData.weapon.ordinaryStick = newLevel;
                break;
            case "yoyo":
                playerData.weapon.yoyo = newLevel;
                break;
            default:
                Debug.LogWarning($"WeaponCode '{weaponCode}' không hợp lệ. Không thể cập nhật cấp độ huhu.");
                return;
        }

        Debug.Log($"Đã cập nhật cấp độ vũ khí '{weaponCode}' lên cấp độ {newLevel}.");
        SaveAndUpdatePlayerData(playerData);
    }

    public void SaveAndUpdatePlayerData(PlayerData data)
    {
        PlayerPrefsManager.SavePlayerDataToPlayerPrefsWithoutPlayerId(data);
        Debug.Log("Dữ liệu người chơi đã được lưu thành công.");
    }

    
    public async void SaveAndUpdatePlayerDataFireBase(){
        PlayerData playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        string playerId = PlayerPrefsManager.GetPlayerIdFromPlayerPrefs();
        await FirebaseManager.Instance.UpdatePlayerData(playerId,playerData);
    }

    public PlayerData GetPlayerData(){
        return playerData;
    }

    public void UpdateMoonG(int quantity){
        this.playerData.stat.gem += quantity;
    }

    public void UpdateRune(int quantity){
        this.playerData.stat.rune += quantity;
    }

    public string PlayerId { get { return playerId; } set { playerId = value; } } 
    public int Health { get { return health; } set { health = value; } } 
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public int Def { get { return def; } set { def = value; } } 
    public int Dmg { get { return dmg; } set { dmg = value; } } 
    public float Survivability { get { return survivability; } set { survivability = value; } }

}
