using UnityEngine;

public class ImmortalCurse : Skill
{
    public float stun;
    public ImmortalCurse() : base(
    skillID: "skillE",
    skillName: "Immortal Curse - Lời nguyền bất tử",
    power: 0,
    range: 0,
    validityPeriod: 30f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 90f,
    damageMultiplier: 0,
    describe: "nhiệm vụ: Chỉ ta mới cứu được chính ta - Chiến đấu và cố gắng sống sót chống lại King of esclipse: The dead present",
    hitCounter: 0)
    {
        stun = 1f; // bất động 1s ( chỉnh survivability = 0)
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
