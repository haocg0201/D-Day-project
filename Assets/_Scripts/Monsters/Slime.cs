using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public Slime() : base(
    monsterName: "Slime",           
    health: 500,                    
    attackDamage: 200,               
    survivability: 1f,            
    size: 0.3f,                     
    dialogues: new List<string>     
    {
            ":))",
            " ?!",
            " @"
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
