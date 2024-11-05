using System.Collections.Generic;
using UnityEngine;

public class Vampire : Monster
{
    public Vampire() : base(
    monsterName: "Vampire",        // Tên quái nhé
    health: 2500,                    // Máu quái nhé
    attackDamage: 500,               // Sát thương của quái nhé
    survivability: 1.5f,            // Chỉ số sinh tồn nhé
    size: 1.8f,                     // Kích thước của quái nhé - F.range(1 - 1.5)
    dialogues: new List<string>     // Các câu thoại đây nhé hh ;) 1.5 - 2.2f
    {
            "!!",
            " Blood",
            " Geggg"
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
