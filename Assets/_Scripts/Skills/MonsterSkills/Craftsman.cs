using UnityEngine;

public class Craftsman : Skill
{
    public bool isEngineSound;
    public Craftsman() : base(
        skillID: "mD",
        skillName: "Craftsman – Ta là thợ thủ công",
        power: 0, // tấn công gốc
        range: 1f,
        validityPeriod: 0,  // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 0,
        damageMultiplier: 0,
        describe: "Golden Goblem - ... (silent)",
        hitCounter: 0)
    {
        isEngineSound = true;
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
