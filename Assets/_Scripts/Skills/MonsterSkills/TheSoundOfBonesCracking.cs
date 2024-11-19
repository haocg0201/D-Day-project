using UnityEngine;

public class TheSoundOfBonesCracking : Skill
{
    public int bonusHpMultiplier;
    public int bonusSurvivability;
    public TheSoundOfBonesCracking() : base(
    skillID: "mC",
    skillName: "The sound of bones cracking - Tiếng xương gãy",
    power: 0, // tấn công gốc
    range: 1f,
    validityPeriod: 60f, // thời gian hiệu lực
    isTrueDamage: false,
    cooldown: 0, // tự hủy khi hết thời gian hiệu lực
    damageMultiplier: 0,
    describe: "Chúa xương được sinh ra ngẫu nhiên, không có điều kiện hay thời gian - Ta đứng lên... ta sẽ bảo vệ đồng tộc của ta - Kzee, kzee - Crak, crak",
    hitCounter: 0)
    {
        bonusHpMultiplier = 3;  // tăng gấp 3 lần chỉ số Hp, hồi phục hoàn toàn hp đã mất
        bonusSurvivability = 2; // +2 survivability
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
