﻿using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{

    public static FirebaseManager Instance { get; private set; }
    public DatabaseReference DbReference { get; private set; }

    public event Action OnFirebaseInitialized;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // giu FirebaseManager khi chuyen scene nhe
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFirebase()
    {   
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance; // Khởi tạo app
                DbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase Database initialized!");
                OnFirebaseInitialized?.Invoke(); // Gọi sự kiện khởi tạo thành công nhé, xem trên stackoverflow + chatgpt
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Exception); // thế chịu ời, trượt môn thôi :(((
            }
        });

        StartCoroutine(CheckFirebaseInitialization());
    }

    private System.Collections.IEnumerator CheckFirebaseInitialization()
    {
        float timeout = 30f; // Thời gian chờ khởi tạo Firebase
        float elapsedTime = 0f;

        while (elapsedTime < timeout)
        {
            yield return new WaitForSeconds(1f); // Chời 2s rồi check lại 
            elapsedTime += 1f;

            // Kiểm tra nếu đã khởi tạo thành công
            if (DbReference != null)
            {
                yield break; // Thoát coroutine nếu đã khởi tạo thành công
            }
        }

        // Đã quá thời gian chờ
        Debug.LogError("Firebase initialization timed out. Exiting application.");
        /*Application.Quit();*/ // Thoát ứng dụng
    #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Dừng chế độ chơi trong Unity Editor :v sau build ae nhớ nhắc tôi để chỉnh lại thoát ứng dụng
    #endif
    }

    public async Task<int> Login(string username, string password)
    {
        try
        {
            var task = await DbReference.Child("player").OrderByChild("username").EqualTo(username).GetValueAsync();
            if (task.Exists && task.ChildrenCount > 0)
            {
                var enumerator = task.Children.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    DataSnapshot playerDataSnapshot = enumerator.Current;
                    string _dbPassword = playerDataSnapshot.Child("password").Value.ToString();
                    string playerId = playerDataSnapshot.Key;
                    if (_dbPassword == password)
                    {
                        PlayerPrefsManager.ClearPlayerData();
                        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataSnapshot.GetRawJsonValue());
                        Debug.Log("Đã lấy được dữ liệu: ");
                        PlayerPrefsManager.SavePlayerDataToPlayerPrefs(playerData,playerId);
                        Debug.Log($"Đã lưu dữ liệu vào PlayerPrefs: {playerDataSnapshot.GetRawJsonValue()}, các trường dữ liệu: {playerData.username}, {playerData.password}, {playerData.stat.level}");
                        Debug.Log("Đăng nhập thành công!");
                        return 1;
                    }
                    else
                    {
                        Debug.LogWarning("Sai tài khoản hoặc mật khẩu."); // check là sai tài khoản hoặc mật khẩu chung chung thôi cho bọn trộm tk nó khó check :]
                        return -1; 
                    }
                }
            }
            //else
            //{
            //    Debug.LogWarning("Tài khoản không tồn tại");
            //    return ??; // Username not found
            //}
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi truy xuất dữ liệu: " + ex);
            return 0; // khai ra là timeout cho đỡ lộ dùng free firebase realtime XD
        }
        Debug.Log("Lỗi khi truy xuất dữ liệu: ???");
        return 0;
    }

    public async Task<int> Register(string username, string password, int scode)
    {
        try
        {
            // Check tk xem tồn tại chưa nhé
            var task = await DbReference.Child("player").OrderByChild("username").EqualTo(username).GetValueAsync();

            if (task.Exists && task.ChildrenCount > 0)
            {
                return -1; // username đã tồn tại
            }

            PlayerData newPlayerData = new PlayerData
            {
                username = username,
                password = password,
                scode = scode,
                stat = new PlayerStat { level = 1, gem = 10000, rune = 10000 },
                skill = new PlayerSkill
                {
                    skillA = true,
                    skillB = false,
                    skillC = false,
                    skillD = false,
                    skillE = false,
                    skillF = false,
                    skillG = false,
                    skillH = false,
                    skillI = false,
                    skillJ = false,
                    skillK = false,
                    skillL = false
                },
                dungeon = 0,
                survival = "00:00",
                campaign = new PlayerCampaignAchievement
                {
                    mapA = "00:00",
                    mapB = "00:00",
                    mapC = "00:00",
                    mapD = "00:00"
                },
                weapon = new PlayerWeapon
                {
                    sword = 0,
                    knife = 0,
                    boxingGloves = 0,
                    pistol = 0,
                    akm = 0,
                    ordinaryStick = 0,
                    yoyo = 0
                }
            };

            // Lưu dữ liệu vào Firebase từ đây nhé
            string playerId = "playerId_" + Guid.NewGuid().ToString();
            string json = JsonUtility.ToJson(newPlayerData);
            await DbReference.Child("player").Child(playerId).SetRawJsonValueAsync(json);

            return 1; // Đăng ký thành công nè
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Lỗi khi đăng ký: " + ex);
            return 0; // Lỗi đăng ký báo là tại mạng hay timeout đi, không phải tại ta sai )
        }
    }

    public async Task<string> ForgotPassword(string username, int scode)
    {
        try
        {
            var task = await DbReference.Child("player").OrderByChild("username").EqualTo(username).GetValueAsync();
            if (task.Exists && task.ChildrenCount > 0)
            {
                var enumerator = task.Children.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    DataSnapshot playerDataSnapshot = enumerator.Current;
                    int _dbScode = int.Parse(playerDataSnapshot.Child("scode").Value.ToString());

                    if (_dbScode == scode)
                    {
                        string password = playerDataSnapshot.Child("password").Value.ToString();
                        return password; // Trả về mật khẩu luôn :v
                    }
                    else
                    {
                        return "scode"; // Mã bảo mật không đúng
                    }
                }
            }
            else
            {
                return "username"; // Tài khoản không tồn tại, sai tên tài khoản
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi lấy mật khẩu: " + ex);
            return "error"; // Chúng ta không sai, Database sai, mạng sai
        }

        return "error";
    }

    public async Task<int> UpdatePlayerData(string playerId, PlayerData updatedPlayerData)
    {
        try
        {
            string json = JsonUtility.ToJson(updatedPlayerData);

            await DbReference.Child("player").Child(playerId).SetRawJsonValueAsync(json); // fb không trả về gì đâu ae nên cứ cho cập nhật thành công đi :v
            Debug.Log("OK");
            return 1; // Cập nhật thành công
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi cập nhật dữ liệu: " + ex);
            return -1; // Lỗi xảy ra, báo tại mạng nhé
        }
    }

    public async Task<List<PlayerData>> GetAllPlayers()
    {
        List<PlayerData> players = new List<PlayerData>();
        try
        {
            var task = await DbReference.Child("player").GetValueAsync();
            if (task.Exists && task.ChildrenCount > 0)
            {
                foreach (var playerSnapshot in task.Children)
                {
                    PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerSnapshot.GetRawJsonValue());
                    players.Add(playerData);

                    //Debug.Log($"Đã lấy dữ liệu người chơi: {playerData.username}, {playerData.stat.level}");
                }
            }
            else
            {
                Debug.LogWarning("Không có người chơi nào trong database");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Lỗi khi truy xuất dữ liệu người chơi: " + ex);
        }
        
        return players;
    }

    // public async Task<List<PlayerData>> GetTop20Players()
    // {
    //     try 
    //     {
    //         List<PlayerData> players = new();
    //         var task = await DbReference.OrderByChild("stat/level").LimitToFirst(20).GetValueAsync(); 
    //         if (task.Exists && task.ChildrenCount > 0)
    //         {
    //             foreach (var playerDataSnapshot in task.Children)
    //             {
    //                 PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataSnapshot.GetRawJsonValue());
    //                 Debug.Log("Đã lấy được dữ liệu: " + playerData.username);
    //                 players.Add(playerData);
    //             }
    //             return players;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Không lấy được dữ liệu");
    //             return new List<PlayerData>(); 
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError("Lỗi khi truy xuất dữ liệu: " + ex);
    //         return new List<PlayerData>(); // Khi có lỗi, trả về danh sách rỗng
    //     }
    // }

    // public async Task<List<PlayerData>> GetTop20WealthPlayers()
    // {
    //     try 
    //     {
    //         List<PlayerData> players = new();
    //         var task = await DbReference.OrderByChild("stat/gem").LimitToFirst(20).GetValueAsync(); 
    //         if (task.Exists && task.ChildrenCount > 0)
    //         {
    //             foreach (var playerDataSnapshot in task.Children)
    //             {
    //                 PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataSnapshot.GetRawJsonValue());
    //                 Debug.Log("Đã lấy được dữ liệu: " + playerData.username);
    //                 players.Add(playerData);
    //             }
    //             return players;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Không lấy được dữ liệu");
    //             return new List<PlayerData>(); 
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError("Lỗi khi truy xuất dữ liệu: " + ex);
    //         return new List<PlayerData>(); // Khi có lỗi, trả về danh sách rỗng
    //     }
    // }

    // public async Task<List<PlayerData>> GetTop20CampaignPlayers()
    // {
    //     try 
    //     {
    //         List<PlayerData> players = new();
    //         var task = await DbReference.OrderByChild("campaign/mapA").LimitToFirst(20).GetValueAsync(); 
    //         if (task.Exists && task.ChildrenCount > 0)
    //         {
    //             foreach (var playerDataSnapshot in task.Children)
    //             {
    //                 PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataSnapshot.GetRawJsonValue());
    //                 Debug.Log("Đã lấy được dữ liệu: " + playerData.username);
    //                 players.Add(playerData);
    //             }
    //             return players;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Không lấy được dữ liệu");
    //             return new List<PlayerData>(); 
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError("Lỗi khi truy xuất dữ liệu: " + ex);
    //         return new List<PlayerData>(); // Khi có lỗi, trả về danh sách rỗng
    //     }
    // }

    // public async Task<List<PlayerData>> GetTop20SvvPlayers()
    // {
    //     try 
    //     {
    //         List<PlayerData> players = new();
    //         var task = await DbReference.OrderByChild("survival").LimitToFirst(20).GetValueAsync(); 
    //         if (task.Exists && task.ChildrenCount > 0)
    //         {
    //             foreach (var playerDataSnapshot in task.Children)
    //             {
    //                 PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataSnapshot.GetRawJsonValue());
    //                 Debug.Log("Đã lấy được dữ liệu: " + playerData.username);
    //                 players.Add(playerData);
    //             }
    //             return players;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Không lấy được dữ liệu");
    //             return new List<PlayerData>(); 
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError("Lỗi khi truy xuất dữ liệu: " + ex);
    //         return new List<PlayerData>(); // Khi có lỗi, trả về danh sách rỗng
    //     }
    // }

}

