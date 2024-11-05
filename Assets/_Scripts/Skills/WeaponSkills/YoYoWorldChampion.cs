using UnityEngine;

public class YoYoWorldChampion : Skill
{
    public YoYoWorldChampion() : base(
    skillID: "skillK",
    skillName: "Hajime Miura 3A Yo-yo world champion - ...",
    power: 0,
    range: 3f, // phía trước, đẩy lùi 
    validityPeriod: 10f, // thời gian hiệu lực
    isTrueDamage: true,
    cooldown: 30f,
    damageMultiplier: 1f,
    describe: "Kỹ năng đặc cấp của Yoyo - Sự kiên trì và nỗ lực sẽ đưa ta đi mọi nơi ta muốn.",
    hitCounter: 5)
    {
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
