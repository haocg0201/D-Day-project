using UnityEngine;

public class Evolution : Skill
{
    public int bonusStatMultiplier;
    public Evolution() : base(
    skillID: "mB",
    skillName: "Evolution – Tiến hóa",
    power: 0, // tấn công gốc
    range: 1f,
    validityPeriod: 60f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 1f,
    damageMultiplier: 0,
    describe: "King of kings, Queen of queens (Slime) - Chúng ta có thể tiến hóa vô hạn.",
    hitCounter: 0)
    {
        bonusStatMultiplier = 2; // tăng gấp đôi chỉ số
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
