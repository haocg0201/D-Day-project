
using UnityEngine;

public class PlayerPrefsManager
{
    // lưu dữ liệu của người chơi vào PlayerPrefs
    public static void SavePlayerDataToPlayerPrefs(PlayerData playerData, string playerId)
    {
        // inf
        PlayerPrefs.SetString("playerId", playerId);
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
        PlayerPrefs.SetInt("skillG", playerData.skill.skillJ ? 1 : 0);
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
        //Debug.Log("Player data saved to PlayerPrefs.");
    }

        public static void SavePlayerDataToPlayerPrefsWithoutPlayerId(PlayerData playerData)
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
        PlayerPrefs.SetInt("skillG", playerData.skill.skillJ ? 1 : 0);
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
        //Debug.Log("Player data saved to PlayerPrefs.");
    }

    public static string GetPlayerIdFromPlayerPrefs()
    {
        return PlayerPrefs.GetString("playerId", "");
    }

    public static PlayerData LoadPlayerDataFromPlayerPrefs()
    {
        PlayerData data = new()
        {
            username = PlayerPrefs.GetString("username"),
            password = PlayerPrefs.GetString("password"),
            scode = PlayerPrefs.GetInt("scode"),

            stat = new PlayerStat
            {
                level = PlayerPrefs.GetInt("level"),
                gem = PlayerPrefs.GetInt("gem"),
                rune = PlayerPrefs.GetInt("rune")
            },

            skill = new PlayerSkill
            {
                skillA = PlayerPrefs.GetInt("skillA", 0) == 1,
                skillB = PlayerPrefs.GetInt("skillB", 0) == 1,
                skillC = PlayerPrefs.GetInt("skillC", 0) == 1,
                skillD = PlayerPrefs.GetInt("skillD", 0) == 1,
                skillE = PlayerPrefs.GetInt("skillE", 0) == 1,
                skillF = PlayerPrefs.GetInt("skillF", 0) == 1,
                skillG = PlayerPrefs.GetInt("skillG", 0) == 1,
                skillH = PlayerPrefs.GetInt("skillH", 0) == 1,
                skillI = PlayerPrefs.GetInt("skillI", 0) == 1,
                skillJ = PlayerPrefs.GetInt("skillJ", 0) == 1,
                skillK = PlayerPrefs.GetInt("skillK", 0) == 1,
                skillL = PlayerPrefs.GetInt("skillL", 0) == 1
            },


            dungeon = PlayerPrefs.GetInt("dungeon"),
            survival = PlayerPrefs.GetString("survival"),

            campaign = new PlayerCampaignAchievement
            {
                mapA = PlayerPrefs.GetString("mapA"),
                mapB = PlayerPrefs.GetString("mapB"),
                mapC = PlayerPrefs.GetString("mapC"),
                mapD = PlayerPrefs.GetString("mapD")
            },

            weapon = new PlayerWeapon
            {
                sword = PlayerPrefs.GetInt("sword"),
                knife = PlayerPrefs.GetInt("knife"),
                boxingGloves = PlayerPrefs.GetInt("boxingGloves"),
                pistol = PlayerPrefs.GetInt("pistol"),
                akm = PlayerPrefs.GetInt("akm"),
                ordinaryStick = PlayerPrefs.GetInt("ordinaryStick"),
                yoyo = PlayerPrefs.GetInt("yoyo")
            }
        };

        return data;
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
        //Debug.Log("Player data cleared from PlayerPrefs.");
    }
}
