using UnityEngine;

public class CrimsonHarvest : Skill
{
    public bool isProvocative;
    public float takePercentDamage;
    public bool isInAHurry;
    
    public CrimsonHarvest() : base(
        skillID: "mH",
        skillName: "Crimson Harvest - Thu hoạch mùa màng đỏ(đẫm máu)",
        power: 0, // tấn công gốc của chiêu
        range: 2f, // chém xung quanh
        validityPeriod: 5f,  // thời gian hiệu lực
        isTrueDamage: true,
        cooldown: 3f, // sau khi người chơi mà talk khiêu khích tiếp thì cho nó chém tiếp )))) ai chơi chắc cay lắm vì chiêu này ko có The final wil thì không né đc )))
        damageMultiplier: 0,
        describe: "Từng có 1 vị Seraphim tổng lãnh thiên thần không biết tên đã tiêu diệt hắn trong 1 cuộc chiến nhân thần diễn ra vào 30000 năm trước khi loài người xuất hiện nên hắn rất hận tộc Angle, học giả thiên tai là món quà chứa đựng nỗi căm phẫn và uất hận của hắn dành cho tộc nhân này.",
        hitCounter: 5) // 1s 1 đòn
    {
        isProvocative = false; // Điều kiện kích hoạt: nếu người chơi nói "Còn gì khó hơn không?" ** , đây là 1 dạng tấn công đặc biệt(sau mỗi lần sử dụng sẽ bị Say huyết , rơi vào trạng thái stun trong 3s
        // sau khi isProvocative = true thì phát động Crimson Harvest
        // **Là một học giả vĩ đại, hắn tự cho mình là toàn năng nhất, hắn là tội đồ của sự kiêu ngạo vậy nên hắn không vừa mắt những kẻ tỏ ra kiêu ngạo.
        isInAHurry = true; // lao tới phía mục tiêu ( điều hướng và tăng ssvability là được bro, sau khi tới điểm đế, set ssvability về như cũ
        takePercentDamage = 0.05f; // hp mục tiêu * takePercentDamage
        
        
    }

    public override void UseSkill()
    {
        base.UseSkill(); // Gọi phương thức UseSkill của lớp cha nhé
        // chưa nghĩ ra @@
    }
}
