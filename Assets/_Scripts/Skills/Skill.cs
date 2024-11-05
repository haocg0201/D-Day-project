using UnityEngine;

public class Skill
{
    public string skillID;        // ID để phân biệt kỹ năng nhé
    public string skillName;   // Tên của kỹ năng
    public int power;          // Sức mạnh, thiệt hại của kỹ năng :v tôi chưa nghĩ ra
    public float range;        // Phạm vi chiêu nhé
    public float validityPeriod; // Thời gian hiệu lực chiêu
    public bool isTrueDamage;  // Sát thương chuẩn bỏ qua giáp 
    public float cooldown;     // Thời gian hồi chiêu của kỹ năng :(( cái này cài sao nhỉ
    public float damageMultiplier; // hệ số tăng phúc % sát thương (dựa tên sát thương gốc)
    public string describe;
    public int hitCounter;

    // Constructor để khởi tạo kỹ năng
    public Skill(string skillID, string skillName, int power, float range, float validityPeriod, bool isTrueDamage, float cooldown, float damageMultiplier, string describe, int hitCounter)
    {
        this.skillID = skillID;
        this.skillName = skillName;
        this.power = power;
        this.range = range;
        this.validityPeriod = validityPeriod;
        this.isTrueDamage = isTrueDamage;
        this.cooldown = cooldown;
        this.damageMultiplier = damageMultiplier;
        this.describe = describe;
        this.hitCounter = hitCounter;
    }

    public virtual void UseSkill()
    {
        Debug.Log($"Vừa sử dụng skill: {skillName} với power: {power} và cooldown: {cooldown}s."); // để tạm đây đã nhé mn
    }

    // Phương thức để kiểm tra nếu kỹ năng có sẵn để sử dụng (có thể thêm vào hệ thống hồi chiêu sau nhé)
    public bool IsSkillAvailable(float currentCooldown)
    {
        return currentCooldown <= 0;
    }
}

