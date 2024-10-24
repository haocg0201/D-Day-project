using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class Player 
{
    public int level;
    public int gem;
    public int rune;
    public Dictionary<string, bool> skill;
    public int dungeon;
    public string survival;
    public Dictionary<string, string> campaign;
    public Dictionary<string, int> weapon;

    //public Player(int level, int gem, int rune, Dictionary<string, bool> skill, int dungeon, string survival, Dictionary<string, string> campaign, Dictionary<string, int> weapon)
    //{
    //    this.level = level;
    //    this.gem = gem;
    //    this.rune = rune;
    //    this.skill = skill;
    //    this.dungeon = dungeon;
    //    this.survival = survival;
    //    this.campaign = campaign;
    //    this.weapon = weapon;
    //}
}
