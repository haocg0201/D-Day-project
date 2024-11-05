using System.Collections.Generic;
using UnityEngine;

public class Werewolf : Monster
{
    public Werewolf() : base(
    monsterName: "Werewolf",        
    health: 2000,                    
    attackDamage: 500,              
    survivability: 1.5f,           
    size: 2.0f,                         // 1 - 2.5f            
    dialogues: new List<string>     
    {
           "auwww wuuu",
           " ?!",
           "waiiiuu"
    },
    mSkill: null
)
    {
    }

    public override void SkillConsumption()
    {
        // tính sau
    }
}
