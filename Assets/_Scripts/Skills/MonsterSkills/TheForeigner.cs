using UnityEngine;

public class TheForeigner : Skill
{
    public int bonusStatMultiplier;
    public TheForeigner() : base(
        skillID: "mE",
        skillName: "The foreigner – Kẻ ngoại lai",
        power: 0, // tấn công gốc
        range: 1f,
        validityPeriod: 0,  // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 0,
        damageMultiplier: 0,
        describe: "Dracuwolf - Ta không biết mình là ai - Chẳng ai chịu chơi với ta cả",
        hitCounter: 0)
    {
        bonusStatMultiplier = 2; // nhân đôi stat
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
