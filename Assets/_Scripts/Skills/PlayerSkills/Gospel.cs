using UnityEngine;

public class Gospel : Skill
{
    public float bonusSurvivability;
    public Gospel() : base(
    skillID: "skillD",
    skillName: "Gospel - Phúc Âm",
    power: 0,
    range: 0,
    validityPeriod: 30f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 90f,
    damageMultiplier: 0,
    describe: "Nhiệm vụ Windy",
    hitCounter: 0)
    {
        bonusSurvivability = 1f;
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
