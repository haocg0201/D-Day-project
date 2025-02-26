using System.Collections.Generic;
using UnityEngine;

public class Orc : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize(monsterName: "Orc",
        health: 20000,
        attackDamage: 500,
        survivability: 1f,
        size: 2.0f,                         // 1 - 2.5f            
        dialogues: new List<string>
        {
                   "uglllyyy",
                   " ?!",
                   "shff shff"
        },1);
        transform.localScale = new Vector3(size, size, size);

    }

    public override void Update()
    {
        base.Update();
    }


    public override void SkillConsumption()
    {
        // t�nh sau
    }
}
