using UnityEngine;

public class TheFinalWill : Skill
{
    public TheFinalWill() : base(
        skillID: "skillA",
        skillName: "The final will - Ý chí cuối cùng",
        power: 0,
        range: 1f,
        validityPeriod: 10f, // thời gian hiệu lực
        isTrueDamage: false,
        cooldown: 30f,
        damageMultiplier: 0,
        describe: "Kỹ năng thiên phú: Đứng trước bờ vực của sống còn, chỉ còn lại 1 suy nghĩ duy nhất là phải sống sót",
        hitCounter: 0)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
