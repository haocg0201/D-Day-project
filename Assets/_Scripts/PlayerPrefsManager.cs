using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPrefsManager
{
    // lưu dữ liệu của người chơi vào PlayerPrefs
    public static void SavePlayerDataToPlayerPrefs(PlayerData playerData)
    {
        // inf
        PlayerPrefs.SetString("username", playerData.username);
        PlayerPrefs.SetString("password", playerData.password);
        PlayerPrefs.SetInt("scode", playerData.scode);

        // stat
        PlayerPrefs.SetInt("level", playerData.stat.level);
        PlayerPrefs.SetInt("gem", playerData.stat.gem);
        PlayerPrefs.SetInt("rune", playerData.stat.rune);

        //skill
        PlayerPrefs.SetInt("skillA", playerData.skill.skillA ? 1 : 0);
        PlayerPrefs.SetInt("skillB", playerData.skill.skillB ? 1 : 0);
        PlayerPrefs.SetInt("skillC", playerData.skill.skillC ? 1 : 0);
        PlayerPrefs.SetInt("skillD", playerData.skill.skillD ? 1 : 0);
        PlayerPrefs.SetInt("skillE", playerData.skill.skillE ? 1 : 0);
        PlayerPrefs.SetInt("skillF", playerData.skill.skillF ? 1 : 0);
        PlayerPrefs.SetInt("skillJ", playerData.skill.skillJ ? 1 : 0);
        PlayerPrefs.SetInt("skillH", playerData.skill.skillH ? 1 : 0);
        PlayerPrefs.SetInt("skillI", playerData.skill.skillI ? 1 : 0);
        PlayerPrefs.SetInt("skillJ", playerData.skill.skillJ ? 1 : 0);
        PlayerPrefs.SetInt("skillK", playerData.skill.skillK ? 1 : 0);
        PlayerPrefs.SetInt("skillL", playerData.skill.skillL ? 1 : 0);

        // dungeon và survival
        PlayerPrefs.SetInt("dungeon", playerData.dungeon);
        PlayerPrefs.SetString("survival", playerData.survival);

        // campaign
        PlayerPrefs.SetString("mapA", playerData.campaign.mapA);
        PlayerPrefs.SetString("mapB", playerData.campaign.mapB);
        PlayerPrefs.SetString("mapC", playerData.campaign.mapC);
        PlayerPrefs.SetString("mapD", playerData.campaign.mapD);

        // Lưu vũ khí
        PlayerPrefs.SetInt("sword", playerData.weapon.sword);
        PlayerPrefs.SetInt("knife", playerData.weapon.knife);
        PlayerPrefs.SetInt("boxingGloves", playerData.weapon.boxingGloves);
        PlayerPrefs.SetInt("pistol", playerData.weapon.pistol);
        PlayerPrefs.SetInt("akm", playerData.weapon.akm);
        PlayerPrefs.SetInt("ordinaryStick", playerData.weapon.ordinaryStick);
        PlayerPrefs.SetInt("yoyo", playerData.weapon.yoyo);

        PlayerPrefs.Save();
        Debug.Log("Player data saved to PlayerPrefs.");
    }

    public static PlayerData LoadPlayerDataFromPlayerPrefs()
    {
        PlayerData playerData = new PlayerData
        {
            username = PlayerPrefs.GetString("username", ""),
            password = PlayerPrefs.GetString("password", ""),
            scode = PlayerPrefs.GetInt("scode", 0),

            stat = new PlayerStat
            {
                level = PlayerPrefs.GetInt("level", 0),
                gem = PlayerPrefs.GetInt("gem", 0),
                rune = PlayerPrefs.GetInt("rune", 0)
            },

            skill = new PlayerSkill
            {
                skillA = PlayerPrefs.GetInt("skillA", 0) == 1,
                skillB = PlayerPrefs.GetInt("skillB", 0) == 1,
                skillC = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillD = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillE = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillF = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillG = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillH = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillI = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillJ = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillK = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillL = PlayerPrefs.GetInt("skillC", 0) == 1
            },

            dungeon = PlayerPrefs.GetInt("dungeon", 0),
            survival = PlayerPrefs.GetString("survival", "00:00"),

            campaign = new PlayerCampaignAchievement
            {
                mapA = PlayerPrefs.GetString("mapA", ""),
                mapB = PlayerPrefs.GetString("mapB", ""),
                mapC = PlayerPrefs.GetString("mapC", ""),
                mapD = PlayerPrefs.GetString("mapD", "")
            },

            weapon = new PlayerWeapon
            {
                sword = PlayerPrefs.GetInt("sword", 0),
                knife = PlayerPrefs.GetInt("knife", 0),
                boxingGloves = PlayerPrefs.GetInt("boxingGloves", 0),
                pistol = PlayerPrefs.GetInt("pistol", 0),
                akm = PlayerPrefs.GetInt("akm", 0),
                ordinaryStick = PlayerPrefs.GetInt("ordinaryStick", 0),
                yoyo = PlayerPrefs.GetInt("yoyo", 0)
            }
        };

        Debug.Log("PlayerData loaded from PlayerPrefs.");
        return playerData;
    }

    public static void ClearPlayerData()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.DeleteKey("scode");

        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("gem");
        PlayerPrefs.DeleteKey("rune");

        PlayerPrefs.DeleteKey("skillA");
        PlayerPrefs.DeleteKey("skillB");
        PlayerPrefs.DeleteKey("skillC");
        PlayerPrefs.DeleteKey("skillD");
        PlayerPrefs.DeleteKey("skillE");
        PlayerPrefs.DeleteKey("skillF");
        PlayerPrefs.DeleteKey("skillG");
        PlayerPrefs.DeleteKey("skillH");
        PlayerPrefs.DeleteKey("skillI");
        PlayerPrefs.DeleteKey("skillJ");
        PlayerPrefs.DeleteKey("skillK");
        PlayerPrefs.DeleteKey("skillL");

        PlayerPrefs.DeleteKey("dungeon");
        PlayerPrefs.DeleteKey("survival");

        PlayerPrefs.DeleteKey("mapA");
        PlayerPrefs.DeleteKey("mapB");
        PlayerPrefs.DeleteKey("mapC");
        PlayerPrefs.DeleteKey("mapD");

        PlayerPrefs.DeleteKey("sword");
        PlayerPrefs.DeleteKey("knife");
        PlayerPrefs.DeleteKey("boxingGloves");
        PlayerPrefs.DeleteKey("pistol");
        PlayerPrefs.DeleteKey("akm");
        PlayerPrefs.DeleteKey("ordinaryStick");
        PlayerPrefs.DeleteKey("yoyo");

        PlayerPrefs.Save();
        Debug.Log("Player data cleared from PlayerPrefs.");
    }
}
