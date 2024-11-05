using UnityEngine;

public class CalamityScholar : Skill
{
    public float takePercentDamage;
    public bool isAngelHere;
    public CalamityScholar() : base(
        skillID: "mG",
        skillName: "Calamity Scholar - Học giả thiên tai",
        power: 0, // tấn công gốc của chiêu
        range: 50f, // nổ 50m xung quanh vùng rơi (tạo bằng event+)
        validityPeriod: 5f,  // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 30f,
        damageMultiplier: 0,
        describe: "Từng có 1 vị Seraphim tổng lãnh thiên thần không biết tên đã tiêu diệt hắn trong 1 cuộc chiến nhân thần diễn ra vào 30000 năm trước khi loài người xuất hiện nên hắn rất hận tộc Angle, \"học giả thiên tai\" là món quà chứa đựng nỗi căm phẫn và uất hận của hắn dành cho tộc nhân này.",
        hitCounter: 5)
    {
        takePercentDamage = 0.05f; // hp mục tiêu * takePercentDamage
        isAngelHere = false; // có kỹ năng của tộc thiên thần xuất hiện => true (true kỹ năng mới được kích hoạt)
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
