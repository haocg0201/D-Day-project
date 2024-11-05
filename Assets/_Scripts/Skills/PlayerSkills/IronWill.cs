using UnityEngine;

public class IronWill : Skill
{
    public IronWill() : base(
    skillID: "skillC",
    skillName: "Iron will - Ý chí sắt đá",
    power: 0,
    range: 3f,
    validityPeriod: 15f, // thời gian hiệu lực
    isTrueDamage: true,
    cooldown: 60f,
    damageMultiplier: 0,
    describe: "Nhiệm vụ Athena",
    hitCounter: 0)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
