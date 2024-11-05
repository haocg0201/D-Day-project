using UnityEngine;

public class GlossyPass : Skill
{ 
    public GlossyPass() : base(
    skillID: "skillF",
    skillName: "Glossy pass - Quá khứ trong sự hào nhoáng",
    power: 0,
    range: 50f,
    validityPeriod: 5f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 30f,
    damageMultiplier: 5f,
    describe: "Kỹ năng đặc cấp của kiếm cùn: Ta đã từng là một thanh kiếm mạnh nhất.",
    hitCounter: 1)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
