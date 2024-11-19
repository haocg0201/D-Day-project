using UnityEngine;

public class MartialArts : Skill
{
    public MartialArts() : base(
    skillID: "skillH",
    skillName: "Martial arts - Thượng võ",
    power: 0,
    range: 1.5f, // xung quanh, đẩy lùi 1.5 - 3f
    validityPeriod: 2f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 30f,
    damageMultiplier:2f,
    describe: "Kỹ năng đặc cấp của găng boxing - Võ thuật bất tận, nỗ lực không ngường nghỉ",
    hitCounter: 5)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
