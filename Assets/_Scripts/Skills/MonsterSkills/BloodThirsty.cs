using UnityEngine;

public class BloodThirsty : Skill
{
    public float bonusSurvivability;
    public float bloodThirsty;
    public BloodThirsty() : base(
        skillID: "mA",
        skillName: "Bloodthirsty – Khát máu",
        power: 0, // tấn công gốc
        range: 1f,     
        validityPeriod: 5f, // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 0,
        damageMultiplier: 0,
        describe: "Vua của 1 ổ Vampire - Máu, ta cần nhiều máu hơn nữa.",
        hitCounter: 0)
    {
        bonusSurvivability = 0.3f;
        bloodThirsty = 0.5f;
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
