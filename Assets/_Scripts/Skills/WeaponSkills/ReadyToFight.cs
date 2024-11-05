using UnityEngine;

public class ReadyToFight : Skill
{
    public ReadyToFight() : base(
    skillID: "skillJ",
    skillName: "Ready to fight – Sẵn sàng chiến đấu",
    power: 0,
    range: 15f, // xung quanh, đẩy lùi 1.5 - 3f
    validityPeriod: 1f, // thời gian hiệu lực
    isTrueDamage: true,
    cooldown: 15f,
    damageMultiplier: 2.5f, // 5f cuối đường đạn
    describe: "Kỹ năng đặc cấp của súng lục - Thực thi trật tự, thực thi công lý.",
    hitCounter: 1)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
