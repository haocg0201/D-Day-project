using System.Collections;
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
    [SerializeField] private int mana;
    [SerializeField] private int maxMana;

    [SerializeField] private int healthFromWeapon = 0;
    [SerializeField] private int defFromWeapon = 0;
    [SerializeField] private int dmgFromWeapon = 0;
    [SerializeField] private float survivabilityFromWeapon = 0;

    public static GameManager Instance { get; private set; } // singleton
    public PlayerData playerData;
    public List<GameObject> monsters = new();

    // Check var mapppp
    public int killCount = 0;
    public int killCountBoss = 0;
    public float svvTime = 0f;
    public int _mgCounter = 0;
    public int _rgCounter = 0;
    public bool isGetQuest = false;
    public bool isHalfQuest = false;
    public bool isQuestDone = false;
    private int dmgFlg = 0;
    public int dropRate = 20;


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

    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    public void InitPlayerData()
    {
        playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        if (playerData != null)
        {
            SetStat();
//            Debug.Log($"un: {playerData.username}, lvl: {playerData.stat.level} khi khởi tạo");
        }
        else
        {
            Debug.Log("Không tải đc dữ liệu :v");
        }
        if (playerData == null)
        {
        }
        else
        {
//           Debug.Log($"un: {playerData.username}, lvl: {playerData.stat.level} khi player đã tồn tại");
        }
    }


    public void OnLogout(){
        Player.DestroyInstance();
        SceneLoader.DestroyInstance();
        EnemySpawner.DestroyInstance();
        WorldWhisperManager.Instance.gameObject.SetActive(false);
        WorldWhisperManager.DestroyInstance();
        DestroyInstance();
    }


    public void SetStat()
    {
        int defaultHealth = 100;
        int defaultDef = 10;
        int defaultDmg = 50;
        float defaultSurvivability = 2f;
        mana = 100;

        if (playerData.stat.level <= 0)
        {
            playerData.stat.level = 1;
        }

        if (playerData.stat.level >= 1)
        {
            // set stat
            this.health = defaultHealth * playerData.stat.level + healthFromWeapon;
            this.def = defaultDef + 5 * playerData.stat.level + defFromWeapon;
            this.dmg = defaultDmg + 10 * playerData.stat.level + dmgFromWeapon;
            this.survivability = defaultSurvivability + (0.01f * playerData.stat.level) + survivabilityFromWeapon;
        }
        // phần nâng cấp vũ khí + chỉ số tính sau nhé
        this.maxHealth = health;
        this.maxMana = (int)mana;
    }

    public void SetWeaponStat(List<Weapon> weapons)
    {   
        float h = 0;
        float d = 0;
        float s = 0;
        if(weapons != null && weapons.Count > 0){
            foreach(Weapon weapon in weapons){
                Weapon w = weapon.GetComponent<Weapon>();
                if(GameManager.Instance != null){
                    h += w.wHealth / 1.2f;
                    d += w.wDef / 1.2f;
                    s += w.wSvvability / 1.2f; 
                }
            }
            this.healthFromWeapon = (int)h;
            this.defFromWeapon = (int)d;
            this.dmgFromWeapon = 100;
            this.survivabilityFromWeapon = s;

        }else{
            this.healthFromWeapon = 0;
            this.defFromWeapon = 0;
            this.dmgFromWeapon = 0;
            this.survivabilityFromWeapon = 0; 
        }
        SetStat();

        // làm gì đó ở đây thì làm sau vậy
    }

    void Start()
    {
        Debug.Log("GameManager Start");
        InitPlayerData();
        LoadGameData();
        StartCoroutine(RegenHealthAndMana());
    }

    private void Update()
    {

    }
    public void LoadGameData() { }

    public void StartGame() { }
    public void PauseGame(bool time)
    {
        Time.timeScale = time ? 0 : 1;
    }
    public void ResumeGame() { }
    public void EndGame() { }
    private void UpdateUI() { }
    public void ResetTheCounter()
    {
        killCount = 0;
        killCountBoss = 0;
        svvTime = 0f;
        _mgCounter = 0;
        _rgCounter = 0;
        isGetQuest = false;
        isHalfQuest = false;
        isQuestDone = false;
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
        if(WorldWhisperManager.Instance != null){
            WorldWhisperManager.Instance.GetComponent<WorldSetting>().UpdateUIGem();
        }
        PlayerPrefsManager.SavePlayerDataToPlayerPrefsWithoutPlayerId(data);
        InitPlayerData();
        SetStat();
        SaveAndUpdatePlayerDataFireBase();
        Debug.Log("Dữ liệu người chơi đã được lưu thành công.");
        
    }


    public async void SaveAndUpdatePlayerDataFireBase()
    {
        PlayerData playerData = PlayerPrefsManager.LoadPlayerDataFromPlayerPrefs();
        string playerId = PlayerPrefsManager.GetPlayerIdFromPlayerPrefs();
        if (FirebaseManager.Instance != null)
        {
            await FirebaseManager.Instance.UpdatePlayerData(playerId, playerData);
        }
        GetPlayerData();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public bool IsTenPercentChance()
    {
        int randomNumber = Random.Range(1, 101); 
        return randomNumber <= dropRate;
    }

    public void SkillAward(string campaign)
    {
        string checkvar = "...";
        if (IsTenPercentChance())
        {
            switch (campaign)
            {
                case "mapA":
                    if (!playerData.skill.skillB)
                    {
                        playerData.skill.skillB = true;
                        checkvar = "Bạn vừa mở khóa 1 kỹ năng mạnh mẽ: Lighting ball";
                    }
                    break;

                case "mapB":
                    if (!playerData.skill.skillC)
                    {
                        playerData.skill.skillC = true;
                        checkvar = "Bạn vừa mở khóa 1 kỹ năng mạnh mẽ: Iron will";
                    }
                    break;

                case "mapC":
                    if (!playerData.skill.skillD)
                    {
                        playerData.skill.skillD = true;
                        checkvar = "Bạn vừa mở khóa 1 kỹ năng mạnh mẽ: Gospel";
                    }
                    break;

                case "mapD":
                    if (!playerData.skill.skillE)
                    {
                        playerData.skill.skillE = true;
                        checkvar = "Bạn vừa mở khóa 1 kỹ năng mạnh mẽ: Immortal curse";
                    }
                    break;

                default:
                    checkvar = "Kỹ năng này tới từ nowhere";
                    break;
            }
        }else{
            checkvar = "Chúc bạn lần tới may mắn";
        }
        WorldWhisperManager.Instance.TextBayLen(checkvar);

    }


    public void UpdateMoonG()
    {
        this.playerData.stat.gem += _mgCounter;
        SaveAndUpdatePlayerData(playerData);
    }

    public void ToSubTract(int quanlity)
    {
        this.playerData.stat.gem -= quanlity;
        if (this.playerData.stat.gem < 0){
            this.playerData.stat.gem = 0;
        }
        SaveAndUpdatePlayerData(playerData);
    }

    public void UpdateRune()
    {
        this.playerData.stat.rune += _rgCounter;
        if (this.playerData.stat.rune < 0){
            this.playerData.stat.rune = 0;
        }
        SaveAndUpdatePlayerData(playerData);
    }   

    public void TenPercentRDDmg(){
        dmgFlg = dmg;
        dmg = Mathf.RoundToInt(dmg * 0.9f);
    } 

    public void SetRealDame(){
        dmg = dmgFlg;
    }
    
    public void OnPlayerLevelUp(int quantity){
        this.playerData.stat.rune -= quantity;
        if (this.playerData.stat.rune < 0){
            this.playerData.stat.rune = 0;
        }
        this.playerData.stat.level += 1;
        SaveAndUpdatePlayerData(playerData);
    }

    public int LvlCost(){
        return 100 + playerData.stat.level * 10;
    }

    public void UpdateTraningTime()
    {
        int seconds = (int)svvTime;
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;  // đoạn trên thấy thừa thãi quá, thôi kệ vậy @@
        string survvTime = minutes.ToString("00") + ":" + remainingSeconds.ToString("00");
        GetHigherSvvTime(survvTime, playerData.survival);
        SaveAndUpdatePlayerData(playerData);
    }

    public void TraningTime(string campaign)
    {
        int seconds = (int)svvTime;
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;  // đoạn trên thấy thừa thãi quá, thôi kệ vậy @@
        string survvTime = minutes.ToString("00") + ":" + remainingSeconds.ToString("00");
        GetCampaignTime(survvTime, playerData.survival);
        SaveAndUpdatePlayerData(playerData);
    }

    public void GetHigherSvvTime(string time1, string time2)
    {

        int ConvertToSeconds(string time)
        {
            var parts = time.Split(':');
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return minutes * 60 + seconds;
        }

        // So sánh hai thời gian
        int seconds1 = ConvertToSeconds(time1);
        int seconds2 = ConvertToSeconds(time2);

        if (seconds1 > seconds2)
        {
            playerData.survival = time1;
        }
        else if (seconds1 < seconds2)
        {
            playerData.survival = time2;
        }
        else
        {
            playerData.survival = time1;
        }
    }

    public void GetCampaignTime(string time1, string campaign)
    {
        string time = "";
        time = campaign switch
        {
            "mapA" => playerData.campaign.mapA,
            "mapB" => playerData.campaign.mapB,
            "mapC" => playerData.campaign.mapC,
            "mapD" => playerData.campaign.mapD,
            _ => "00:00",
        };
        int ConvertToSeconds(string time)
        {
            var parts = time.Split(':');
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return minutes * 60 + seconds;
        }

        // So sánh hai thời gian
        int seconds1 = ConvertToSeconds(time1);
        int seconds2 = ConvertToSeconds(time);

    if (seconds1 < seconds2)
    {
        switch(campaign)
        {
            case "mapA": playerData.campaign.mapA = time1; break;
            case "mapB": playerData.campaign.mapB = time1; break;
            case "mapC": playerData.campaign.mapC = time1; break;
            case "mapD": playerData.campaign.mapD = time1; break;
            default: break;
        }
    }
    // Cập nhật lại time nếu time1 nhỏ hơn
    else if (seconds1 > seconds2)
    {
        switch(campaign)
        {
            case "mapA": playerData.campaign.mapA = time; break;
            case "mapB": playerData.campaign.mapB = time; break;
            case "mapC": playerData.campaign.mapC = time; break;
            case "mapD": playerData.campaign.mapD = time; break;
            default: break;
        }
    }
    else
    {
        // Nếu thời gian bằng nhau, giữ nguyên hoặc chọn time1
        switch(campaign)
        {
            case "mapA": playerData.campaign.mapA = time1; break;
            case "mapB": playerData.campaign.mapB = time1; break;
            case "mapC": playerData.campaign.mapC = time1; break;
            case "mapD": playerData.campaign.mapD = time1; break;
            default: break;
        }
    }
    }

    public string PlayerId { get { return playerId; } set { playerId = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public int Def { get { return def; } set { def = value; } }
    public int Dmg { get { return dmg; } set { dmg = value; } }
    public float Survivability { get { return survivability; } set { survivability = value; } }
    public int Mana { get { return mana; } set { mana = value; } }
    public int MaxMana { get { return maxMana; } set { maxMana = value; } }
    public int HealthFromWeapon
    {
        get { return healthFromWeapon; }
        set { healthFromWeapon = value; }
    }

    public int DefFromWeapon
    {
        get { return defFromWeapon; }
        set { defFromWeapon = value; }
    }

    public int DmgFromWeapon
    {
        get { return dmgFromWeapon; }
        set { dmgFromWeapon = value; }
    }

    public float SurvivabilityFromWeapon
    {
        get { return survivabilityFromWeapon; }
        set { survivabilityFromWeapon = value; }
    }

    private IEnumerator RegenHealthAndMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f); // Chờ 

            // Hồi 2% máu và mana
            health += (int)(maxHealth * (1 / 100f));
            mana += (int)(maxMana * (2 / 100f));

            // Đảm bảo không vượt quá giới hạn
            health = Mathf.Min(health, maxHealth);
            mana = Mathf.Min(mana, maxMana);

            //Debug.Log($"Health: {health}/{maxHealth}, Mana: {mana}/{maxMana}");
        }
    }

}
