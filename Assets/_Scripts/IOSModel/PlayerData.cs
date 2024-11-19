using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PlayerData 
{
    public string username;
    public string password;
    public int scode;
    public PlayerStat stat;
    public PlayerSkill skill;
    public int dungeon;
    public string survival;
    public PlayerCampaignAchievement campaign;
    public PlayerWeapon weapon;
}
