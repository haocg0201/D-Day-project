using System.Collections.Generic;
using UnityEngine;

public class Goblem : Monster
{
    public Goblem() : base(
        monsterName: "Goblem",        // Tên quái nhé
        health: 1500,                    // Máu quái nhé
        attackDamage: 100,               // Sát thương của quái nhé
        survivability: 0.5f,            // Chỉ số sinh tồn nhé
        size: 2.5f,                     // Kích thước của quái nhé
        dialogues: new List<string>     // Các câu thoại đây nhé hh ;) 2- 2.5f
        {
            "Con người",
            " ?!",
            "kesss kiess"
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
