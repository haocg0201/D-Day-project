using UnityEngine;

public class SilkLinesOnTheLeaves : Skill
{
    public SilkLinesOnTheLeaves() : base(
    skillID: "skillG",
    skillName: "Silk lines on the leaves – Đường tơ trên kẽ lá",
    power: 0,
    range: 1f,
    validityPeriod: 2f, // thời gian hiệu lực , around
    isTrueDamage: false,
    cooldown: 15f,
    damageMultiplier: 1.5f,
    describe: " Kỹ năng đặc cấp của dao - Ta rất sắc đấy.",
    hitCounter: 7)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
