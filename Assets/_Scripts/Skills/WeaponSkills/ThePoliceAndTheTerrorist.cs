using UnityEngine;

public class ThePoliceAndTheTerrorist : Skill
{
    public ThePoliceAndTheTerrorist() : base(
    skillID: "skillI",
    skillName: "The police and the terrorist - Cảnh sát và tên khủng bố",
    power: 0,
    range: 10f, // xung quanh, đẩy lùi 1.5 - 3f
    validityPeriod: 1f, // thời gian hiệu lực
    isTrueDamage: true,
    cooldown: 15f,
    damageMultiplier: 2f,
    describe: "Kỹ năng đặc cấp của súng lục - Thực thi trật tự, thực thi công lý.",
    hitCounter:5)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
