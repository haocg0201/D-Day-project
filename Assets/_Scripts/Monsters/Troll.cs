using System.Collections.Generic;
using UnityEngine;

public class Troll : Monster
{
    public Troll() : base(
    monsterName: "Troll",
    health: 5000,
    attackDamage: 1000,
    survivability: 2f,
    size: 4f,                            
    dialogues: new List<string>
    {
               "uglllyyy",
               " ?!",
               "shff shff"
    },
    mSkill: null
    )
    { }

    public override void SkillConsumption()
    {
        // tính sau
    }
}
