using UnityEngine;

public class TheKngOfEclipses : Skill
{
    public bool isTakeDamage;
    public bool isAngelHere;
    public TheKngOfEclipses() : base(
        skillID: "mF",
        skillName: "The king of eclipses – Vị vua của nhật thực",
        power: 0, // tấn công gốc
        range: 1f,
        validityPeriod: 120f,  // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 180f,
        damageMultiplier: 0,
        describe: "King of esclipse: The dead present - Khi còn sống ta là một nhà hiền triết lỗi lạc nhất.",
        hitCounter: 0)
    {
        isTakeDamage = false; // có nhận sát thương không ?
        isAngelHere = false; // có tộc thiên thần => sát thương 100% bỏ qua giáp và hiệu ứng (isTakeDamage= true && TakeDamage(dmg bỏ qua tất cả hiệu ứng))
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
