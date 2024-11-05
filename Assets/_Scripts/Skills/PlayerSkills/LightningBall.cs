using UnityEngine;

public class LightningBall : Skill
{
    public int countBall;
    public LightningBall() : base(
    skillID: "skillB",
    skillName: "Lightning ball - Cầu sét",
    power: 333,
    range: 3f,
    validityPeriod: 15,
    isTrueDamage: true,
    cooldown: 60f,
    damageMultiplier: 0,
    describe: "Nhiệm vụ Ahri",
    hitCounter: 0)
    {
        countBall = 3;
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
