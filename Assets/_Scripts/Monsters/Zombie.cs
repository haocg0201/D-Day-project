using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{
    public Zombie() : base(
        monsterName: "Zombie",
        health: 1000,
        attackDamage: 500,
        survivability: 1f,
        size: 1.6f,                         // 1 - 2f            
        dialogues: new List<string>
        {
                   "uhhh owhh",
                   " ?!",
                   "Gawww wwwoo"
        },
        mSkill: null
        )
    { }

    public override void SkillConsumption()
    {
        // tính sau
    }
}
