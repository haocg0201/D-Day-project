using UnityEngine;

public class TheProtectionOfGaia : Skill
{
    public TheProtectionOfGaia() : base(
        skillID: "skillK",
        skillName: "The protection of Gaia – Sự bảo hộ của mẹ thiên nhiên",
        power: 0,
        range: 1f, // xung quanh, đẩy lùi 1f
        validityPeriod: 5f, // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 15f,
        damageMultiplier: 1f, 
        describe: "Kỹ năng đặc cấp của cây gậy tầm thường - Xào xạc, xào xạc.",
        hitCounter: 0)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
