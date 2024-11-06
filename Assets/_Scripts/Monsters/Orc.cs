using System.Collections.Generic;
using UnityEngine;

public class Orc : Monster
{
    public Orc() : base(
        monsterName: "Orc",
        health: 2000,
        attackDamage: 100,
        survivability: 1f,
        size: 2.0f,                         // 1 - 2.5f            
        dialogues: new List<string>
        {
                   "uglllyyy",
                   " ?!",
                   "shff shff"
        },
        mSkill: null
        )
    {}

    public override void SkillConsumption()
    {
        // tính sau
    }
}
