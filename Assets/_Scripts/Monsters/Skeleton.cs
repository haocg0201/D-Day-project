using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public Skeleton() : base(
        monsterName: "Skeleton",        // Tên quái nhé
        health: 500,                    // Máu quái nhé
        attackDamage: 200,               // Sát thương của quái nhé
        survivability: 1.5f,            // Chỉ số sinh tồn nhé
        size: 1.5f,                     // Kích thước của quái nhé - F.range(1 - 1.5)
        dialogues: new List<string>     // Các câu thoại đây nhé hh ;)
        {
            "Hm Ghuuu ..!",
            " ?!",
            "Crak crakk"
        },
        mSkill:  null
    )
    {
    }

    public override void SkillConsumption()
    {
        // tính sau
    }
}

