using System.Collections.Generic;
using UnityEngine;

public class Goblem : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize(monsterName: "Goblem",        // Tên quái nhé
            health: 1500,                    // Máu quái nhé
            attackDamage: 10,               // Sát thương của quái nhé
            survivability: 0.5f,            // Chỉ số sinh tồn nhé
            size: 2.5f,                     // Kích thước của quái nhé
            dialogues: new List<string>     // Các câu thoại đây nhé hh ;) 2- 2.5f
            {
                "Con người",
                " ?!",
                "kesss kiess"
            },
            0
        );
        transform.localScale = new Vector3(size, size, size);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void SkillConsumption()
    {
        // tính sau
    }
}
